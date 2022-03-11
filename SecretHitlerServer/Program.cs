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
        static int id;

        static void Main(string[] args)
        {
            id = 0;
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
            ci.Data = "player" + ++id;
            return true;
        }

        static void ReadBytes(ClientInfo ci, byte[] data, int len)
        {
            Console.WriteLine($"Received message from {ci.Data}: [{string.Join(", ", data)}]");
        }

        static void ConnectionClosed(ClientInfo ci)
        {
            Console.WriteLine(ci.Data + " left the server.");
        }
    }
}
