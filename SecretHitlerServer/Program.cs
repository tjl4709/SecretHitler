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
        static Server m_server;
        static int vip_id, m_nextPrezIdx, m_voteCnt, m_proCnt, m_electTrack;
        static Dictionary<string, ClientInfo> m_clients;
        static List<string> m_players;
        static Game m_game;
        static string m_nextChanc;
        static List<Role> m_policies;

        static void Main(string[] args)
        {
            vip_id = -1;
            m_clients = new Dictionary<string, ClientInfo>(10);
            m_players = new List<string>(10);
            m_server = new Server(0);
            m_server.ClientReady += ClientReady;
            m_server.DefaultEncryptionType = EncryptionType.ServerRSAClientKey;
            Console.WriteLine("Listening on port " + m_server.Port);
            Console.WriteLine("Enter \"Exit\" to stop server.");
            string com;
            do {
                com = Console.ReadLine().Trim().ToLower();
                if (com == "port")
                    Console.WriteLine("Listening on port " + m_server.Port);
            } while (com != "exit");
        }

        static bool ClientReady(Server s, ClientInfo ci)
        {
            ci.OnReadBytes += ReadBytes;
            ci.OnClose += ConnectionClosed;
            ci.Data = "player" + ci.ID;
            m_clients.Add((string)ci.Data, ci);
            if (vip_id == -1) {
                vip_id = ci.ID;
                ci.Send(new byte[] { (byte)Command.VIP });
            }
            if (m_players.Count < 10)
                m_players.Add((string)ci.Data);
            return true;
        }

        static void ReadBytes(ClientInfo ci, byte[] data, int len)
        {
            if (data.Length == 0) {
                ci.Send(CmdMsgToBytes(Command.Error, "Missing command code"));
                return;
            }
            Console.WriteLine($"Received data from {ci.Data}: [{string.Join(", ", data)}]");
            switch ((Command)data[0]) {
                case Command.General: {
                    if (m_game == null) {
                        m_clients.Remove((string)ci.Data);
                        ci.Data = ParseBytesToString(data);
                        m_clients.Add((string)ci.Data, ci);
                    }
                    break;
                }
                case Command.Start: {
                    if (ci.ID == vip_id) {
                        if (m_players.Count >= 5) {
                            m_game = new Game(m_players);
                            m_electTrack = 0;
                            SendRoles();
                            Random rand = new Random();
                            m_nextPrezIdx = rand.Next(m_players.Count);
                            m_server.Broadcast(CmdMsgToBytes(Command.PosAssign, m_players[m_nextPrezIdx]));
                        } else
                            ci.Send(CmdMsgToBytes(Command.Error, "Insufficient amount of players. At least 5 are required"));
                    } else
                        ci.Send(CmdMsgToBytes(Command.Error, "Only the VIP can start the game"));
                    break;
                }
                case Command.PosAssign: {
                    if (m_game != null) {
                        m_nextChanc = ParseBytesToString(data);
                        if (m_nextChanc != m_players[m_nextPrezIdx] && m_nextChanc != m_game.Chancellor && (m_game.NumAlive <= 5 || m_nextChanc != m_game.President)) {
                            m_voteCnt = m_proCnt = 0;
                            m_server.Broadcast(CmdMsgToBytes(Command.Vote, m_nextChanc));
                        } else ci.Send(CmdMsgToBytes(Command.Error, "Ineligable Chancellor nomination"));
                    } else ci.Send(CmdMsgToBytes(Command.Error, "Game not started"));
                    break;
                }
                case Command.Vote: {
                    if (m_game != null) {
                        m_voteCnt++;
                        if (data[0] != 1) m_proCnt++;
                        if (m_voteCnt == m_players.Count) {
                            m_server.Broadcast(new byte[] { (byte)Command.VoteCnt, (byte)m_proCnt });
                            if (m_proCnt > m_voteCnt / 2) {
                                m_game.President = m_players[m_nextPrezIdx];
                                m_game.Chancellor = m_nextChanc;
                                if (m_game.Winner != Role.None)
                                    m_server.Broadcast(new byte[] { (byte)Command.Winner, (byte)m_game.Winner });
                                else {
                                    m_policies = m_game.Draw();
                                    m_clients[m_game.President].Send(new byte[] { (byte)Command.Policy, (byte)m_policies[0], (byte)m_policies[1], (byte)m_policies[2] });
                                }
                            } else {
                                m_electTrack++;
                                if (m_electTrack >= 3) {
                                    m_electTrack = 0;
                                    Role policy = m_game.Draw(1)[0];
                                    m_server.Broadcast(new byte[] { (byte)Command.Policy, (byte)policy });
                                    m_game.Play(policy);
                                    if (m_game.Winner != Role.None)
                                        m_server.Broadcast(new byte[] { (byte)Command.Winner, (byte)m_game.Winner });
                                }
                                m_nextPrezIdx = (m_nextPrezIdx + 1) % m_players.Count;
                                m_server.Broadcast(CmdMsgToBytes(Command.PosAssign, m_players[m_nextPrezIdx]));
                            }
                        }
                    } else ci.Send(CmdMsgToBytes(Command.Error, "Game not started"));
                    break;
                }
                default:
                    ci.Send(CmdMsgToBytes(Command.Error, "Unrecognized command: " + data[0].ToString("X")));
                    break;
            }
        }

        static void SendRoles()
        {
            for (int i = 0; i < m_players.Count; i++)
                if (m_game.GetRole(m_players[i]) == Role.Liberal || m_players.Count >= 7 && m_game.GetRole(m_players[i]) == Role.Hitler) {
                    m_clients[m_players[i]].Send(new byte[] { (byte)Command.Start, (byte)m_game.GetRole(m_players[i]) });
                } else {
                    string msg = "" + (char)m_game.GetRole(m_players[i]);
                    int j;
                    for (j = 0; j < m_players.Count; j++)
                        if (i != j && m_game.GetRole(m_players[j]) == Role.Fascist)
                            msg += m_players[j] + (char)Role.Fascist;
                    if (m_game.GetRole(m_players[i]) != Role.Hitler) {
                        for (j = 0; j < m_players.Count; j++)
                            if (i != j && m_game.GetRole(m_players[j]) == Role.Hitler) {
                                msg += m_players[j];
                                break;
                            }
                        if (m_players.Count >= 7) msg += (char)Role.Hitler;
                        else msg += (char)Role.Fascist;
                    }
                    m_clients[m_players[i]].Send(CmdMsgToBytes(Command.Start, (char)m_game.GetRole(m_players[i]) + msg));
                }
        }

        static void ConnectionClosed(ClientInfo ci)
        {
            if (ci.ID == vip_id) {
                if (m_clients.Count > 0) {
                    vip_id = int.MaxValue;
                    ClientInfo newVIP = null;
                    foreach (ClientInfo client in m_clients.Values)
                        if (client.ID < vip_id) {
                            vip_id = client.ID;
                            newVIP = client;
                        }
                    newVIP?.Send(new byte[] { (byte)Command.VIP });
                } else vip_id = -1;
            }
            if (m_game != null && m_game.Contains((string)ci.Data)) {
                string msg = ci.Data + " has been disconnected";
                if (m_game.Kill((string)ci.Data)) {
                    m_server.Broadcast(CmdMsgToBytes(Command.General, ci.Data + " has been disconnected and was hitler"));
                    m_server.Broadcast(new byte[] { (byte)Command.Winner, (byte)m_game.Winner });
                } else m_server.Broadcast(CmdMsgToBytes(Command.General, ci.Data + " has been disconnected"));
            }
            Console.WriteLine(ci.Data + " left the server.");
        }

        static byte[] CmdMsgToBytes(Command cmd, string msg)
        { return Array.ConvertAll(((char)cmd + msg).ToCharArray(), Convert.ToByte); }
        static string ParseBytesToString(byte[] bytes, int numCmd = 1)
        { return new string(Array.ConvertAll(bytes, Convert.ToChar)).Substring(numCmd); }
    }
}
