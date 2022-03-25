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
            m_server = new Server(args.Length > 0 ? int.Parse(args[0]) : 0);
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
                            Console.WriteLine(name + " has connected to the server");
                            if (vip_id == -1) {
                                vip_id = ci.ID;
                                ci.Send(new byte[] { 1, (byte)Command.VIP });
                                Console.WriteLine(name + " is the VIP");
                            }
                            m_clients.Add(name, ci);
                            if (m_players.Count > 0)
                                ci.Send(Parser.ToBytes(Command.Name, string.Join(",", m_players)));
                            if (m_game == null && m_players.Count < 10) {
                                m_players.Add(name);
                                Console.WriteLine(name + " has joined the game");
                                m_server.Broadcast(Parser.ToBytes(Command.Name, name));
                            }
                        }
                        break;
                    }
                    case Command.Start: {
                        if (ci.ID == vip_id) {
                            if (m_players.Count >= 5) {
                                Console.WriteLine("The game has begun!");
                                m_game = new Game(m_players);
                                m_electTrack = 0;
                                SendRoles();
                                m_server.Broadcast(Parser.ToBytes(Command.PosAssign, m_game.NextPrez));
                                Console.WriteLine(m_game.NextPrez + " is the next President.");
                            } else
                                ci.Send(Parser.ErrMsg("Insufficient amount of players. At least 5 are required"));
                        } else
                            ci.Send(Parser.ErrMsg("Only the VIP can start the game"));
                        break;
                    }
                    case Command.PosAssign: {
                        if (m_game != null) {
                            m_nextChanc = Parser.ToString(cmd);
                            Console.WriteLine(m_nextChanc + " has been nominated as Chancellor.");
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
                                if (m_voteCnt == m_game.NumAlive) {
                                    m_server.Broadcast(new byte[] { 2, (byte)Command.VoteCnt, (byte)m_proCnt });
                                    m_game.NextPrezResult(m_proCnt > m_voteCnt / 2);
                                    Console.WriteLine("The vote " + (m_proCnt > m_voteCnt / 2 ? "passed" : "failed"));
                                    if (m_proCnt > m_voteCnt / 2) {
                                        m_electTrack = 0;
                                        m_game.Chancellor = m_nextChanc;
                                        if (m_game.Winner != Role.None) {
                                            m_server.Broadcast(new byte[] { 2, (byte)Command.Winner, (byte)m_game.Winner });
                                            Console.WriteLine("Hitler was elected Chancellor after 3 Fascist policies and the Fascists have won!");
                                        } else {
                                            if (m_policies == null || m_policies.Count != 3)
                                                m_policies = m_game.Draw();
                                            m_clients[m_game.President].Send(new byte[] { 4, (byte)Command.Policy, (byte)m_policies[0], (byte)m_policies[1], (byte)m_policies[2] });
                                        }
                                    } else {
                                        IncElectTrack();
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
                                    Console.WriteLine("A " + (Role)cmd[1] + " ploicy was enacted.");
                                    m_server.Broadcast(new byte[] { 2, (byte)Command.Policy, cmd[1] });
                                    m_game.Discard(m_policies[0]);
                                    FascistPowers pow = m_game.Play((Role)cmd[1]);
                                    if (m_game.Winner != Role.None) {
                                        m_server.Broadcast(new byte[] { 2, (byte)Command.Winner, (byte)m_game.Winner });
                                        Console.WriteLine("The " + m_game.Winner + "s have enacted enough policies and won!");
                                    } else if (pow != FascistPowers.None) {
                                        Console.WriteLine("The President has received the " + pow + " power.");
                                        if (pow == FascistPowers.PolicyPeek) {
                                            m_policies = m_game.Draw();
                                            m_clients[m_game.President].Send(new byte[] { 5, (byte)Command.FascPow, (byte)pow,
                                                (byte)m_policies[0], (byte)m_policies[1], (byte)m_policies[2] });
                                            m_server.Broadcast(Parser.ToBytes(Command.PosAssign, m_game.NextPrez));
                                            Console.WriteLine(m_game.NextPrez + " is the next President.");
                                        } else
                                            m_clients[m_game.President].Send(new byte[] { 2, (byte)Command.FascPow, (byte)pow });
                                    } else {
                                        m_server.Broadcast(Parser.ToBytes(Command.PosAssign, m_game.NextPrez));
                                        Console.WriteLine(m_game.NextPrez + " is the next President.");
                                    }
                                }
                            } else ci.Send(Parser.ErrMsg("Invalid policy choice"));
                        } else ci.Send(Parser.ErrMsg("Game not started"));
                        break;
                    }
                    case Command.FascPow: {
                        if (m_game != null) {
                            if ((FascistPowers)cmd[1] == FascistPowers.Veto) {
                                string msg;
                                if ((string)ci.Data == m_game.Chancellor) {
                                    Console.WriteLine((msg = "The Chancellor has requested a veto") + '.');
                                    m_server.Broadcast(Parser.ToBytes(Command.General, msg));
                                    m_clients[m_game.President].Send(new byte[] { 2, (byte)Command.FascPow, (byte)FascistPowers.Veto });
                                } else if ((string)ci.Data == m_game.President) {
                                    if (cmd[2] == 1) msg = "The President has accepted the veto";
                                    else msg = "The President has denied the veto";
                                    Console.WriteLine(msg + '.');
                                    m_server.Broadcast(Parser.ToBytes(Command.General, msg));
                                    m_server.Broadcast(new byte[] { 3, (byte)Command.FascPow, (byte)FascistPowers.Veto, cmd[2] });
                                    if (cmd[2] == 1) IncElectTrack();
                                }
                            } else if ((string)ci.Data == m_game.President) {
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
                                            Console.WriteLine(player + " has been selected as the next President.");
                                        } else ci.Send(Parser.ErrMsg("Cannot choose current president"));
                                        break;
                                    case FascistPowers.Execution:
                                        m_game.Kill(player);
                                        Console.WriteLine(player + " has been executed.");
                                        m_server.Broadcast(Parser.FascPowToBytes(FascistPowers.Execution, player));
                                        if (m_game.Winner != Role.None) {
                                            m_server.Broadcast(new byte[] { 2, (byte)Command.Winner, (byte)m_game.Winner });
                                            Console.WriteLine("The Liberals have executed Hitler and won!");
                                            return;
                                        }
                                        break;
                                    default:
                                        ci.Send(Parser.ErrMsg("Unrecognized presidential power"));
                                        break;
                                }
                                Console.WriteLine(m_game.NextPrez + " is the next President.");
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
                        ci.Send(Parser.ErrMsg("Unrecognized command: 0x" + cmd[0].ToString("X")));
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
        static void IncElectTrack()
        {
            m_electTrack++;
            if (m_electTrack >= 3) {
                m_electTrack = 0;
                Role policy = m_game.Draw(1)[0];
                m_server.Broadcast(new byte[] { 2, (byte)Command.Policy, (byte)policy });
                Console.WriteLine("The people were fed up with the inactive govt and enacted a " + policy + "policy.");
                m_game.Play(policy);
                if (m_game.Winner != Role.None) {
                    m_server.Broadcast(new byte[] { 2, (byte)Command.Winner, (byte)m_game.Winner });
                    Console.WriteLine("The " + m_game.Winner + "s enacted enough policies and won!");
                    return;
                }
                m_game.President = m_game.Chancellor = "";
            }
            Console.WriteLine(m_game.NextPrez + " is the next President.");
            m_server.Broadcast(Parser.ToBytes(Command.PosAssign, m_game.NextPrez));
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
                    if (newVIP != null) {
                        newVIP.Send(new byte[] { 1, (byte)Command.VIP });
                        Console.WriteLine(newVIP.Data + " is the new VIP.");
                    }
                } else vip_id = -1;
            }
            Console.WriteLine(ci.Data + " left the server.");
            if (m_players.Remove((string)ci.Data))
                m_server.Broadcast(Parser.ToBytes(Command.Name, (string)ci.Data));
            if (m_game != null) {
                if (m_game.Contains((string)ci.Data))
                    m_server.Broadcast(Parser.FascPowToBytes(FascistPowers.Execution, (string)ci.Data));
                    if (m_game.Kill((string)ci.Data)) {
                        m_server.Broadcast(Parser.ToBytes(Command.General, ci.Data + " has been disconnected and was hitler"));
                        m_server.Broadcast(new byte[] { 2, (byte)Command.Winner, (byte)m_game.Winner });
                        Console.WriteLine("Hitler has left so the Liberals win.");
                    } else m_server.Broadcast(Parser.ToBytes(Command.General, ci.Data + " has been disconnected"));
            } else {
                foreach (string key in m_clients.Keys)
                    if (!m_players.Contains(key)) {
                        m_players.Add(key);
                        m_server.Broadcast(Parser.ToBytes(Command.Name, key));
                        Console.WriteLine(key + " has joined the game.");
                        if (m_players.Count == 10)
                            break;
                    }
            }
        }
    }
}
