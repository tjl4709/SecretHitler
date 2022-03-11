using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RedCorona.Net;
using SecretHitlerUtilities;

namespace SecretHitlerServer
{
    class Program
    {
        static Server server;
        static int vip_id;

        static void Main(string[] args)
        {
            vip_id = -1;
            server = new Server(0);
            server.ClientReady += ClientReady;
            server.DefaultEncryptionType = EncryptionType.ServerRSAClientKey;
            Console.WriteLine("Listening on port " + server.Port);
            Console.WriteLine("Enter \"Exit\" to stop server.");
            string com;
            do {
                com = Console.ReadLine().Trim().ToLower();
                if (com == "port")
                    Console.WriteLine("Listening on port " + server.Port);
            } while (com != "exit");
        }

        static bool ClientReady(Server s, ClientInfo ci)
        {
            ci.OnReadBytes += ReadBytes;
            ci.OnClose += ConnectionClosed;
            ci.Data = "player" + ci.ID;
            if (vip_id == -1) {
                vip_id = ci.ID;
                ci.Send(new byte[] { (byte)Commands.General });
            }
            return true;
        }

        static void ReadBytes(ClientInfo ci, byte[] data, int len)
        {
            if (data.Length == 0) {
                SendErrMsg(ci, "Missing command code");
                return;
            }
            Console.WriteLine($"Received data from {ci.Data}: [{string.Join(", ", data)}]");
            switch ((Commands)data[0]) {
                case Commands.General:
                    break;
                default:
                    break;
            }
        }

        static void ConnectionClosed(ClientInfo ci)
        {
            if (ci.ID == vip_id) {
                //set vip_id to next player or -1 if no other players connected
            }
            Console.WriteLine(ci.Data + " left the server.");
        }

        static void SendErrMsg(ClientInfo ci, string msg)
        {
            byte[] err = Array.ConvertAll(((char)Commands.Error + msg).ToCharArray(), Convert.ToByte);
            ci.Send(err);
        }
    }
}
