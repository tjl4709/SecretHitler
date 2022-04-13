using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace SecretHitlerUtilities
{
    public delegate bool ConnectionComplete(Client client);
    public delegate void ReceiveReadBytes(Client client, byte[] bytes, int len);
    public delegate void ConnectionClosed(Client client);

    public class Client
    {
        public event ReceiveReadBytes ReadBytes;
        public void OnReadBytes(byte[] data, int len) { ReadBytes?.Invoke(this, data, len); }
        public event ConnectionComplete ConnectComplete;
        public void OnConnectComplete() { ConnectComplete?.Invoke(this); }
        public event ConnectionClosed ConnectClosed;
        public void OnConnectClosed() { ConnectClosed?.Invoke(this); }

        protected Server m_server;

        protected Socket m_socket;
        public int Port { get { return ((IPEndPoint)m_socket.RemoteEndPoint).Port; } }
        public IPAddress IP { get { return ((IPEndPoint)m_socket.RemoteEndPoint).Address; } }
        public bool Connected { get { return m_socket != null && m_socket.Connected; } }

        private static int NEXT_ID = 0;
        public readonly int ID;

        public string Name;
        public object Data;

        private int BUF_SZ = 512;
        public int BufferSize { get { return BUF_SZ; }
            set {
                BUF_SZ = value;
                byte[] buf = new byte[BUF_SZ];
                Array.Copy(m_buffer, buf, Math.Min(m_buffer.Length, buf.Length));
                m_buffer = buf;
            } }
        private byte[] m_buffer;


        public Client(Socket socket, string name = "")
        {
            m_socket = socket;
            Name = name;
            ID = NEXT_ID++;
            m_buffer = new byte[BUF_SZ];
        }
        public Client(IPAddress ip, int port, string name = "")
        {
            m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            m_socket.BeginConnect(new IPEndPoint(ip, port), ConnectCallback, this);
            Name = name;
            ID = NEXT_ID++;
            m_buffer = new byte[BUF_SZ];
        }

        internal void SetServer(Server s) { if (m_server == null) m_server = s; }

        public void Send(byte[] data)
        {
            m_socket.BeginSend(data, 0, data.Length, SocketFlags.None, SendCallback, this);
        }
        public void BeginReceive()
        {
            m_socket.BeginReceive(m_buffer, 0, BUF_SZ, SocketFlags.None, ReceiveCallback, this);
        }
        public void Close()
        {
            if (m_server != null) m_server.ClientClosed(this);
            OnConnectClosed();
            try {
                m_socket.Shutdown(SocketShutdown.Both);
                m_socket.Close();
            } catch { }
        }

        protected static void ConnectCallback(IAsyncResult result)
        {
            try {
                Client client = (Client)result.AsyncState;
                client.m_socket.EndConnect(result);
#if DEBUG
                Console.WriteLine((string.IsNullOrEmpty(client.Name) ? "Client " + client.ID : client.Name) + " connection complete");
#endif
                client.OnConnectComplete();
            } catch { }
        }
        protected static void SendCallback(IAsyncResult result)
        {
            Client client = null;
            try {
                client = (Client)result.AsyncState;
                int len = client.m_socket.EndSend(result);
                #if DEBUG
                    Console.WriteLine($"Sent {len} to server");
                #endif
            } catch (SocketException) {
                client?.Close();
            } catch (ObjectDisposedException) {
                client?.Close();
            }
        }
        protected static void ReceiveCallback(IAsyncResult result)
        {
            Client client = null;
            try {
                client = (Client)result.AsyncState;
                int len = client.m_socket.EndReceive(result);
                if (len > 0) {
                    client.OnReadBytes(client.m_buffer, len);
                    client.BeginReceive();
                } else client.Close();
            } catch (SocketException) {
                client?.Close();
            } catch (ObjectDisposedException) {
                client?.Close();
            }
        }
    }

    public class Server
    {
        public event ReceiveReadBytes ReadBytes;
        public void OnReadBytes(Client client, byte[] data, int len) { ReadBytes?.Invoke(client, data, len); }
        public event ConnectionComplete ConnectComplete;
        public bool OnConnectComplete(Client client) { return ConnectComplete != null && ConnectComplete.Invoke(client); }

        protected Dictionary<int, Client> m_clients;
        public Client this[int id] { get { return m_clients.ContainsKey(id) ? m_clients[id] : null; } }

        protected Socket m_socket;
        public int Port { get { return ((IPEndPoint)m_socket.LocalEndPoint).Port; } }
        public IPAddress IP { get { return ((IPEndPoint)m_socket.LocalEndPoint).Address; } }
        public bool Connected { get { return m_socket != null && m_socket.Connected; } }


        public Server(int port, ConnectionComplete connComp = null)
        {
            ConnectComplete = connComp;
            m_clients = new Dictionary<int, Client>();
            m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
			m_socket.Bind(new IPEndPoint(IPAddress.Any, port));
			m_socket.Listen(100);
            m_socket.BeginAccept(AcceptCallback, this);
        }

        public bool AddClient(Client client)
        {
            if (m_clients.ContainsKey(client.ID))
                return false;
            m_clients.Add(client.ID, client);
            client.SetServer(this);
            client.BeginReceive();
            return true;
        }
        public void ClientClosed(Client client) { m_clients.Remove(client.ID); }

        public void Send(Client client, byte[] data) { client.Send(data); }
        public void Broadcast(byte[] data)
        { for (int i = 0; i < m_clients.Count; i++) m_clients.ElementAt(i).Value.Send(data); }

        public void Close()
        {
            List<Client> clients = new List<Client>(m_clients.Values);
            for (int i = 0; i < clients.Count; i++) clients[i].Close();
            m_socket.Shutdown(SocketShutdown.Both);
            m_socket.Close();
        }

        protected static void AcceptCallback(IAsyncResult result)
        {
            Server server = (Server)result.AsyncState;
            Client client = new Client(server.m_socket.EndAccept(result));
            server.m_socket.BeginAccept(AcceptCallback, server);
            if (server.ConnectComplete == null || server.OnConnectComplete(client))
                server.AddClient(client);
            else client.Close();
        }
    }
}
