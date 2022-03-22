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
        static int vip_id, m_voteCnt, m_proCnt, m_electTrack;
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
            if (vip_id == -1) {
                vip_id = ci.ID;
                ci.Send(new byte[] { 1, (byte)Command.VIP });
            }
            return true;
        }

        static void ReadBytes(ClientInfo ci, byte[] data, int len)
        {
            byte[] cmd;
            for (int cmdStart = 0; cmdStart < data.Length; cmdStart += data[cmdStart] + 1) {
                cmd = new byte[data[cmdStart]];
                Array.ConstrainedCopy(data, cmdStart + 1, cmd, 0, data[cmdStart]);
                //Console.WriteLine($"Received data from {ci.Data}: [{string.Join(", ", cmd)}]");
                switch ((Command)cmd[0]) {
                    case Command.Name: {
                        string name = Parser.ToString(cmd);
                        for (int i = 0; i < name.Length; i++)
                            if (!char.IsLetterOrDigit(name[i]))
                                name.Remove(i);
                        if (m_clients.ContainsKey(name)) {
                            ci.Send(new byte[] { 2, (byte)Command.Name, 0 });
                        } else {
                            ci.Send(new byte[] { 2, (byte)Command.Name, 1 });
                            ci.Data = name;
                            m_clients.Add(name, ci);
                            if (m_players.Count > 1)
                                ci.Send(Parser.ToBytes(Command.Name, string.Join(",", m_players)));
                            if (m_game == null && m_players.Count < 10) {
                                m_players.Add(name);
                                m_server.Broadcast(Parser.ToBytes(Command.Name, name));
                            }
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
                                m_server.Broadcast(Parser.ToBytes(Command.PosAssign, m_game.NextPrez));
                            } else
                                ci.Send(Parser.ErrMsg("Insufficient amount of players. At least 5 are required"));
                        } else
                            ci.Send(Parser.ErrMsg("Only the VIP can start the game"));
                        break;
                    }
                    case Command.PosAssign: {
                        if (m_game != null) {
                            m_nextChanc = Parser.ToString(cmd);
                            if (m_nextChanc != m_game.NextPrez && m_nextChanc != m_game.Chancellor && (m_game.NumAlive <= 5 || m_nextChanc != m_game.President)) {
                                m_voteCnt = m_proCnt = 0;
                                m_server.Broadcast(Parser.ToBytes(Command.Vote, m_nextChanc));
                            } else ci.Send(Parser.ErrMsg("Ineligable Chancellor nomination"));
                        } else ci.Send(Parser.ErrMsg("Game not started"));
                        break;
                    }
                    case Command.Vote: {
                        if (m_game != null) {
                            if (cmd.Length == 2) {
                                m_voteCnt++;
                                if (cmd[1] == 1) m_proCnt++;
                                if (m_voteCnt == m_players.Count) {
                                    m_server.Broadcast(new byte[] { 2, (byte)Command.VoteCnt, (byte)m_proCnt });
                                    m_game.NextPrezResult(m_proCnt > m_voteCnt / 2);
                                    if (m_proCnt > m_voteCnt / 2) {
                                        m_game.Chancellor = m_nextChanc;
                                        if (m_game.Winner != Role.None)
                                            m_server.Broadcast(new byte[] { 2, (byte)Command.Winner, (byte)m_game.Winner });
                                        else {
                                            if (m_policies == null || m_policies.Count != 3)
                                                m_policies = m_game.Draw();
                                            m_clients[m_game.President].Send(new byte[] { 4, (byte)Command.Policy, (byte)m_policies[0], (byte)m_policies[1], (byte)m_policies[2] });
                                        }
                                    } else {
                                        m_electTrack++;
                                        if (m_electTrack >= 3) {
                                            m_electTrack = 0;
                                            Role policy = m_game.Draw(1)[0];
                                            m_server.Broadcast(new byte[] { 2, (byte)Command.Policy, (byte)policy });
                                            m_game.Play(policy);
                                            if (m_game.Winner != Role.None) {
                                                m_server.Broadcast(new byte[] { 2, (byte)Command.Winner, (byte)m_game.Winner });
                                                break;
                                            }
                                            m_game.President = m_game.Chancellor = "";
                                        }
                                        m_server.Broadcast(Parser.ToBytes(Command.PosAssign, m_game.NextPrez));
                                    }
                                }
                            } else ci.Send(Parser.ErrMsg("Improper format: Vote command must only contain boolean value"));
                        } else ci.Send(Parser.ErrMsg("Game not started"));
                        break;
                    }
                    case Command.Policy: {
                        if (m_game != null) {
                            if (m_policies.Remove((Role)cmd[1])) {
                                if (m_policies.Count == 2) {
                                    m_game.Discard((Role)cmd[1]);
                                    m_clients[m_game.Chancellor].Send(new byte[] { 3, (byte)Command.Policy, (byte)m_policies[0], (byte)m_policies[1] });
                                } else {
                                    m_server.Broadcast(cmd);
                                    m_game.Discard(m_policies[0]);
                                    FascistPowers pow = m_game.Play((Role)cmd[1]);
                                    if (m_game.Winner != Role.None) {
                                        m_server.Broadcast(new byte[] { 2, (byte)Command.Winner, (byte)m_game.Winner });
                                    } else if (pow != FascistPowers.None) {
                                        if (pow == FascistPowers.InvestigateLoyalty) {
                                            m_policies = m_game.Draw();
                                            ci.Send(new byte[] { 5, (byte)Command.FascPow, (byte)pow,
                                            (byte)m_policies[0], (byte)m_policies[1], (byte)m_policies[2] });
                                        } else
                                            m_clients[m_game.President].Send(new byte[] { 2, (byte)Command.FascPow, (byte)pow });
                                    } else
                                        m_server.Broadcast(Parser.ToBytes(Command.PosAssign, m_game.NextPrez));
                                }
                            } else ci.Send(Parser.ErrMsg("Invalid policy choice"));
                        } else ci.Send(Parser.ErrMsg("Game not started"));
                        break;
                    }
                    case Command.FascPow: {
                        if (m_game != null) {
                            if ((string)ci.Data == m_game.President) {
                                string player = "";
                                if ((FascistPowers)cmd[1] != FascistPowers.PolicyPeek)
                                    player = Parser.ToString(cmd, 2);
                                switch ((FascistPowers)cmd[1]) {
                                    case FascistPowers.InvestigateLoyalty:
                                        if (player != m_game.InvestigatedPlayer) {
                                            m_game.InvestigatedPlayer = player;
                                            m_server.Broadcast(Parser.FascPowToBytes(FascistPowers.InvestigateLoyalty, player));
                                            ci.Send(new byte[] { 3, (byte)Command.FascPow, (byte)FascistPowers.InvestigateLoyalty, (byte)m_game.GetRole(player) });
                                        } else ci.Send(Parser.ErrMsg("Player already investigated"));
                                        break;
                                    case FascistPowers.SpecialElection:
                                        if (player != m_game.President) {
                                            m_game.NextPrez = player;
                                        } else ci.Send(Parser.ErrMsg("Cannot choose current president"));
                                        break;
                                    case FascistPowers.Execution:
                                        m_game.Kill(player);
                                        if (m_game.Winner != Role.None) {
                                            m_server.Broadcast(Parser.FascPowToBytes(FascistPowers.Execution, player));
                                            m_server.Broadcast(new byte[] { 2, (byte)Command.Winner, (byte)m_game.Winner });
                                            return;
                                        }
                                        break;
                                    default:
                                        ci.Send(Parser.ErrMsg("Unrecognized presidential power"));
                                        break;
                                }
                                m_server.Broadcast(Parser.ToBytes(Command.PosAssign, m_game.NextPrez));
                            } else ci.Send(Parser.ErrMsg("Only the president may use presidential powers"));
                        } else ci.Send(Parser.ErrMsg("Game not started"));
                        break;
                    }
                    case Command.General:
                        Console.WriteLine("Received message: " + Parser.ToString(cmd));
                        break;
                    case Command.Error:
                        Console.WriteLine("Received error: " + Parser.ToString(cmd));
                        break;
                    default:
                        ci.Send(Parser.ErrMsg("Unrecognized command: " + cmd[0].ToString("X")));
                        break;
                }
            }
        }

        static void SendRoles()
        {
            for (int i = 0; i < m_players.Count; i++)
                if (m_game.GetRole(m_players[i]) == Role.Liberal || m_players.Count >= 7 && m_game.GetRole(m_players[i]) == Role.Hitler) {
                    m_clients[m_players[i]].Send(new byte[] { 2, (byte)Command.Start, (byte)m_game.GetRole(m_players[i]) });
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
                    m_clients[m_players[i]].Send(Parser.ToBytes(Command.Start, msg));
                }
        }

        static void ConnectionClosed(ClientInfo ci)
        {
            if (ci.Data != null)
                m_clients.Remove((string)ci.Data);
            if (ci.ID == vip_id) {
                if (m_clients.Count > 0) {
                    vip_id = int.MaxValue;
                    ClientInfo newVIP = null;
                    foreach (ClientInfo client in m_clients.Values)
                        if (client.ID < vip_id) {
                            vip_id = client.ID;
                            newVIP = client;
                        }
                    newVIP?.Send(new byte[] { 1, (byte)Command.VIP });
                } else vip_id = -1;
            }
            if (m_players.Remove((string)ci.Data))
                m_server.Broadcast(Parser.ToBytes(Command.Name, (string)ci.Data));
            if (m_game != null) {
                if (m_game.Contains((string)ci.Data)) 
                    if (m_game.Kill((string)ci.Data)) {
                        m_server.Broadcast(Parser.FascPowToBytes(FascistPowers.Execution, (string)ci.Data));
                        m_server.Broadcast(Parser.ToBytes(Command.General, ci.Data + " has been disconnected and was hitler"));
                        m_server.Broadcast(new byte[] { 2, (byte)Command.Winner, (byte)m_game.Winner });
                    } else m_server.Broadcast(Parser.ToBytes(Command.General, ci.Data + " has been disconnected"));
            } else {
                foreach (string key in m_clients.Keys)
                    if (!m_players.Contains(key)) {
                        m_players.Add(key);
                        if (m_players.Count == 10)
                            break;
                    }
            }
            Console.WriteLine(ci.Data + " left the server.");
        }
    }
}
