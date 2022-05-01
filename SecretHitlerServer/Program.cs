using System;
using System.Collections.Generic;
using SecretHitlerUtilities;
using System.IO;
using System.Reflection;
using System.Threading;

namespace SecretHitlerServer
{
    class Program
    {
        const int MAX_LOG_LVL = 1, MAX_PORT=65535;

        static Server m_server;
        static int vip_id, m_electTrack;
        static Dictionary<string, Client> m_clients;
        static Dictionary<string, bool> m_participating;
        static List<string> m_players;
        static Game m_game;
        static string m_nextChanc;
        static List<Role> m_policies;
        static string m_logFile = null;
        static Queue<string> m_logQ;
        static Thread m_logThread;
        static bool m_anonVote;

        static void Main(string[] args)
        {
            int port = ParseCommandArgs(args);
            vip_id = -1;
            m_anonVote = false;
            m_clients = new Dictionary<string, Client>(10);
            m_participating = new Dictionary<string, bool>(10);
            m_players = new List<string>(10);
            try {
                m_server = new Server(port);
            } catch {
                Log("Given port was already in use.");
                m_server = new Server(0);
            }
            m_server.ConnectComplete += ClientReady;
            Console.WriteLine("Listening on port " + m_server.Port);
            Console.WriteLine("Enter \"Exit\" to stop server.");
            string com;
            do {
                com = Console.ReadLine().Trim().ToLower();
                if (com == "port")
                    Console.WriteLine("Listening on port " + m_server.Port);
            } while (com != "exit");
        }
        static int ParseCommandArgs(string[] args)
        {
            int port = 0, log = 1;
            #region //parse logging
            int idxInf = Array.IndexOf(args, "-l"),           lidxInf = Array.LastIndexOf(args, "-l"),
                idxFrml = Array.IndexOf(args, "--log-level"), lidxFrml = Array.LastIndexOf(args, "--log-level");
            if (idxInf != -1 && idxFrml != -1 || idxInf != lidxInf || idxFrml != lidxFrml)
                Console.WriteLine("Logging level specified multiple times, using first most verbose valid occurence");
            if (idxInf != -1 || idxFrml != -1)
                if (!TryParseLog(args, idxFrml, out log) && !TryParseLog(args, lidxFrml, out log) &&
                    !TryParseLog(args, idxInf, out log) &&  !TryParseLog(args, lidxInf, out log)) {
                    Console.WriteLine("Logging level specified without valid argument: logging enabled");
                    log = 1;
                }
            #endregion
            if (log > 0) /*set up logging*/ {
                m_logFile = Assembly.GetEntryAssembly().Location;
                m_logFile = m_logFile.Substring(0, m_logFile.LastIndexOf('\\') + 1) + "logs";
                if (!Directory.Exists(m_logFile)) Directory.CreateDirectory(m_logFile);
                m_logFile += "\\SHSL_" + DateTime.Now.ToString("s").Replace('T', '_').Replace(':', '-') + ".log";
                m_logQ = new Queue<string>();
                m_logThread = new Thread(LogQHandler);
                m_logThread.IsBackground = true;
                m_logThread.Start();
                AppDomain.CurrentDomain.UnhandledException += (s, a) => {
#if DEBUG
                    Log("Unhandled Exception:: " + (a.ExceptionObject as Exception)?.ToString());
#else
                    Log("Unhandled Exception: " + (a.ExceptionObject as Exception)?.Message);
#endif
                };
                AppDomain.CurrentDomain.ProcessExit += (s, a) => { while (m_logQ.Count > 0) Thread.Sleep(100);};
            }
            #region //parse port
            idxInf = Array.IndexOf(args, "-p");      lidxInf = Array.LastIndexOf(args, "-p");
            idxFrml = Array.IndexOf(args, "--port"); lidxFrml = Array.LastIndexOf(args, "--port");
            if (idxInf != -1 && idxFrml != -1 || idxInf != lidxInf || idxFrml != lidxFrml)
                Console.WriteLine("Port specified multiple times, using first most verbose valid occurence");
            if (idxInf != -1 || idxFrml != -1)
                if (!TryParsePort(args, idxFrml, out port) && !TryParsePort(args, lidxFrml, out port) &&
                    !TryParsePort(args, idxInf, out port) &&  !TryParsePort(args, lidxInf, out port)) {
                    Console.WriteLine("Port specified without valid argument: finding open port");
                    port = 0;
                }
            return port;
            #endregion
        }
        static bool TryParseLog(string[] args, int index, out int log)
        { return TryParseIntArg(args, index, 0, MAX_LOG_LVL, out log); }
        static bool TryParsePort(string[] args, int index, out int port)
        { return TryParseIntArg(args, index, 0, MAX_PORT, out port); }
        static bool TryParseIntArg(string[] args, int index, int minBound, int maxBound, out int output)
        {
            output = -1;
            if (index >= 0) {
                if (index < args.Length - 1 && !args[index + 1].Contains("-")) {
                    if (!(int.TryParse(args[index + 1], out output) && output >= minBound && output <= maxBound)) {
                        output = -1;
                        Console.WriteLine($"Error: {args[index + 1]} is not a valid number");
                    }
                } else Console.WriteLine($"Error: {args[index]} missing arguments");
            }
            return output != -1;
        }

        static void ReadBytes(Client ci, byte[] data, int len)
        {
            byte[] cmd;
            for (int cmdStart = 0; cmdStart < len; cmdStart += data[cmdStart] + 1) {
                cmd = new byte[data[cmdStart]];
                Array.ConstrainedCopy(data, cmdStart + 1, cmd, 0, data[cmdStart]);
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
                            ci.Name = name;
                            Log($"{ci.Name}({ci.ID}) has connected to the server");
                            CheckVIP(ci);
                            m_clients.Add(name, ci);
                            m_participating.Add(name, true);
                            if (m_players.Count > 0)
                                ci.Send(Parser.ToBytes(Command.Name, string.Join(",", m_players)));
                            if (m_players.Count < 10) {
                                m_players.Add(name);
                                Log(name + " has joined the game");
                                m_server.Broadcast(Parser.ToBytes(Command.Name, name));
                            }
                            if (m_game != null)
                                ci.Send(Parser.UpdateToBytes(m_game.NumLiberalPolicies,
                                    m_game.NumFascistPolicies, m_game.President, m_game.Chancellor));
                        }
                        break;
                    }
                    case Command.Start: {
                        if (ci.ID == vip_id) {
                            if (m_players.Count >= 5) {
                                Log("\nThe game has begun!");
                                m_game = new Game(m_players);
                                m_policies = null;
                                m_electTrack = 0;
                                SendRoles();
                                NotifyNextPrez();
                            } else
                                ci.Send(Parser.ErrMsg("Insufficient amount of players. At least 5 are required"));
                        } else
                            ci.Send(Parser.ErrMsg("Only the VIP can start the game"));
                        break;
                    }
                    case Command.PosAssign: {
                        if (m_game != null) {
                            m_nextChanc = Parser.ToString(cmd);
                            Log(m_nextChanc + " has been nominated as Chancellor.");
                            if (m_nextChanc != m_game.NextPrez && m_nextChanc != m_game.Chancellor && (m_game.NumAlive <= 5 || m_nextChanc != m_game.President)) {
                                m_game.StartVoting();
                                m_server.Broadcast(Parser.ToBytes(Command.Vote, m_nextChanc));
                            } else ci.Send(Parser.ErrMsg("Ineligable Chancellor nomination"));
                        } else ci.Send(Parser.ErrMsg("Game not started"));
                        break;
                    }
                    case Command.Vote: {
                        if (m_game != null) {
                            if (cmd.Length == 2) {
                                m_game.Votes[ci.Name] = cmd[1] == 1;
                                CheckVoting();
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
                                    m_electTrack = 0;
                                    Log("A " + (Role)cmd[1] + " ploicy was enacted.");
                                    m_server.Broadcast(new byte[] { 2, (byte)Command.Policy, cmd[1] });
                                    m_game.Discard(m_policies[0]);
                                    FascistPowers pow = m_game.Play((Role)cmd[1]);
                                    if (!SendWinner("The " + m_game.Winner + "s have enacted enough policies and won!")) {
                                        if (pow != FascistPowers.None) {
                                            Log("The President has received the " + pow + " power.");
                                            if (pow == FascistPowers.PolicyPeek) {
                                                m_policies = m_game.Draw();
                                                m_clients[m_game.President].Send(new byte[] { 5, (byte)Command.FascPow, (byte)pow,
                                                (byte)m_policies[0], (byte)m_policies[1], (byte)m_policies[2] });
                                                NotifyNextPrez();
                                            } else
                                                m_clients[m_game.President].Send(new byte[] { 2, (byte)Command.FascPow, (byte)pow });
                                        } else NotifyNextPrez();
                                    }
                                }
                            } else ci.Send(Parser.ErrMsg("Invalid policy choice"));
                        } else ci.Send(Parser.ErrMsg("Game not started"));
                        break;
                    }
                    case Command.Winner: {
                        if (m_game != null) {
                            if (vip_id == ci.ID) {
                                m_server.Broadcast(new byte[] { 2, (byte)Command.Winner, (byte)Role.None });
                                Log("Game ended");
                                m_game = null;
                            } else
                                m_server.Broadcast(Parser.ErrMsg("Only VIP can end the game"));
                        } else
                            m_server.Broadcast(Parser.ErrMsg("Game not started"));
                        break;
                    }
                    case Command.Settings:
                        ParseSetting(ci, cmd); break;
                    case Command.FascPow:
                        ParseFascistPower(ci, cmd); break;
                    case Command.General:
                        Log("Received message: " + Parser.ToString(cmd)); break;
                    case Command.Error:
                        Log("Received error: " + Parser.ToString(cmd)); break;
                    default:
                        ci.Send(Parser.ErrMsg("Unrecognized command: 0x" + cmd[0].ToString("X")));
                        break;
                }
            }
        }
        static void ParseFascistPower(Client ci, byte[] cmd)
        {
            if (m_game != null) {
                if ((FascistPowers)cmd[1] == FascistPowers.Veto) {
                    string msg;
                    if (ci.Name == m_game.Chancellor) {
                        Log((msg = "The Chancellor has requested a veto") + '.');
                        m_server.Broadcast(Parser.ToBytes(Command.General, msg));
                        m_clients[m_game.President].Send(new byte[] { 2, (byte)Command.FascPow, (byte)FascistPowers.Veto });
                    } else if (ci.Name == m_game.President) {
                        if (cmd[2] == 1) {
                            msg = "The President has accepted the veto";
                            m_game.Discard(m_policies);
                            m_policies.Clear();
                        } else msg = "The President has denied the veto";
                        Log(msg + '.');
                        m_server.Broadcast(Parser.ToBytes(Command.General, msg));
                        m_server.Broadcast(new byte[] { 3, (byte)Command.FascPow, (byte)FascistPowers.Veto, cmd[2] });
                        if (cmd[2] == 1) IncElectTrack();
                    }
                } else if (ci.Name == m_game.President) {
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
                                Log(player + " has been selected as the next President.");
                            } else ci.Send(Parser.ErrMsg("Cannot choose current president"));
                            break;
                        case FascistPowers.Execution:
                            m_game.Kill(player);
                            Log(player + " has been executed.");
                            m_server.Broadcast(Parser.FascPowToBytes(FascistPowers.Execution, player));
                            if (SendWinner("The Liberals have executed Hitler and won!")) return;
                            break;
                        default:
                            ci.Send(Parser.ErrMsg("Unrecognized presidential power"));
                            break;
                    }
                    NotifyNextPrez();
                } else ci.Send(Parser.ErrMsg("Only the president may use presidential powers"));
            } else ci.Send(Parser.ErrMsg("Game not started"));
        }
        static void ParseSetting(Client ci, byte[] cmd)
        {
            if (cmd.Length < 2) {
                ci.Send(Parser.ErrMsg("Missing setting information"));
                return;
            }
            switch((Setting)cmd[1]) {
                case Setting.Participation: {
                    if (cmd.Length == 3) {
                        m_participating[ci.Name] = cmd[2] == 1;
                        if (m_participating[ci.Name]) {
                            if (m_players.Count < 10 && !m_players.Contains(ci.Name)) {
                                m_players.Add(ci.Name);
                                Log(ci.Name + " has joined the game");
                                m_server.Broadcast(Parser.ToBytes(Command.Name, ci.Name));
                                CheckVIP(ci);
                            }
                        } else if (m_players.Contains(ci.Name)) {
                            m_players.Remove(ci.Name);
                            m_server.Broadcast(Parser.ToBytes(Command.Disconnect, ci.Name));
                            Log(ci.Name + " has left the game");
                            if (ci.ID == vip_id) FindVIP();
                            TryJoining();
                        }
                    } else ci.Send(Parser.ErrMsg("Missing paticipation information"));
                    break;
                }
                case Setting.AnonymousVoting: {
                    if (cmd.Length == 3) {
                        m_anonVote = cmd[2] == 1;
                        Log("Anonymous voting has been turned o" + (m_anonVote ? "n" : "ff"));
                    } else ci.Send(Parser.ErrMsg("Missing anonymous voting information"));
                    break;
                }
                default:
                    ci.Send(Parser.ErrMsg("Unrecognized setting: 0x" + cmd[1].ToString("X")));
                    break;
            }
        }

        static void NotifyNextPrez()
        {
            m_nextChanc = "";
            if (m_game != null) {
                Log(m_game.NextPrez + " is the next President.");
                m_server.Broadcast(Parser.ToBytes(Command.PosAssign, m_game.NextPrez));
            }
        }
        static void SendRoles()
        {
            foreach (string key in m_clients.Keys)
                if (m_players.Contains(key))
                    if (m_game.GetRole(key) == Role.Liberal || m_players.Count >= 7 && m_game.GetRole(key) == Role.Hitler) {
                        m_clients[key].Send(new byte[] { 2, (byte)Command.Start, (byte)m_game.GetRole(key) });
                    } else {
                        string msg = "" + (char)m_game.GetRole(key);
                        int j;
                        for (j = 0; j < m_players.Count; j++)
                            if (m_game.GetRole(m_players[j]) == Role.Fascist && key != m_players[j])
                                msg += m_players[j] + (char)Role.Fascist;
                        if (m_game.GetRole(key) != Role.Hitler) {
                            for (j = 0; j < m_players.Count; j++)
                                if (m_game.GetRole(m_players[j]) == Role.Hitler && key != m_players[j]) {
                                    msg += m_players[j];
                                    break;
                                }
                            if (m_players.Count >= 7) msg += (char)Role.Hitler;
                            else msg += (char)Role.Fascist;
                        }
                        m_clients[key].Send(Parser.ToBytes(Command.Start, msg));
                    }
                else m_clients[key].Send(new byte[] { 2, (byte)Command.Start, (byte)Role.Audience });
        }
        static void CheckVoting()
        {
            if (m_game?.VotingComplete == true) {
                if (m_anonVote)
                    m_server.Broadcast(new byte[] { 2, (byte)Command.VoteCnt, (byte)m_game.ProVoters.Count });
                else
                    m_server.Broadcast(Parser.ToBytes(Command.VoteCnt, string.Join(",", m_game.ProVoters)));
                bool passed = m_game.VotePassed;
                m_game.NextPrezResult(passed);
                Log($"The vote {(passed ? "passed" : "failed")} with {string.Join(", ", m_game.ProVoters)} voting ja");
                if (passed) {
                    m_game.Chancellor = m_nextChanc;
                    if (!SendWinner("Hitler was elected Chancellor after 3 Fascist policies and the Fascists have won!")) {
                        if (m_policies == null || m_policies.Count < 3)
                            m_policies = m_game.Draw();
                        m_clients[m_game.President].Send(new byte[] { 4, (byte)Command.Policy, (byte)m_policies[0], (byte)m_policies[1], (byte)m_policies[2] });
                    }
                } else {
                    IncElectTrack();
                }
            }
        }
        static void IncElectTrack()
        {
            if (++m_electTrack >= 3) {
                m_electTrack = 0;
                Role policy= m_game.Draw(1)[0];
                if (m_policies != null && m_policies.Count > 0) {
                    m_policies.Add(policy);
                    policy = m_policies[0];
                    m_policies.RemoveAt(0);
                }
                m_server.Broadcast(new byte[] { 2, (byte)Command.Policy, (byte)policy });
                Log("The people were fed up with the inactive govt and enacted a " + policy + "policy.");
                m_game.Play(policy);
                if (m_game.Winner != Role.None) {
                    m_server.Broadcast(new byte[] { 2, (byte)Command.Winner, (byte)m_game.Winner });
                    Log("The " + m_game.Winner + "s enacted enough policies and won!");
                    return;
                }
                m_game.President = m_game.Chancellor = "";
            }
            NotifyNextPrez();
        }
        static bool SendWinner(string msg)
        {
            if (m_game.Winner == Role.None)
                return false;
            m_server.Broadcast(new byte[] { 2, (byte)Command.Winner, (byte)m_game.Winner });
            Log(msg);
            m_game = null;
            return true;
        }

        static void TryJoining()
        {
            foreach (string key in m_clients.Keys)
                if (m_players.Count >= 10)
                    break;
                else if (!m_players.Contains(key) && m_participating[key]) {
                    m_players.Add(key);
                    m_server.Broadcast(Parser.ToBytes(Command.Name, key));
                    Log(key + " has joined the game.");
                    CheckVIP(m_clients[key]);
                }
        }
        static void CheckVIP(Client ci)
        {
            if (vip_id == -1) {
                vip_id = ci.ID;
                ci.Send(new byte[] { 2, (byte)Command.VIP,  (byte)(m_anonVote ? 1 : 0) });
                Log(ci.Name + " is the VIP");
            }
        }
        static void FindVIP()
        {
            vip_id = -1;
            if (m_clients.Count > 0) {
                int id = int.MaxValue;
                Client newVIP = null;
                foreach (Client client in m_clients.Values)
                    if (m_participating[client.Name] && client.ID < id) {
                        id = client.ID;
                        newVIP = client;
                    }
                if (newVIP != null && vip_id == -1) {
                    vip_id = id;
                    newVIP.Send(new byte[] { 2, (byte)Command.VIP,  (byte)(m_anonVote ? 1 : 0) });
                    Log($"{newVIP.Name}({newVIP.ID}) is the new VIP.");
                }
            }
        }

        static void Log(string msg)
        {
            Console.WriteLine(msg);
            m_logQ?.Enqueue(msg);
        }
        static void LogQHandler()
        {
            while (Thread.CurrentThread.IsAlive) {
                if (m_logQ.Count > 0) {
                    StreamWriter sw = new StreamWriter(File.Open(m_logFile, FileMode.Append));
                    while (m_logQ.Count > 0) sw.WriteLine(m_logQ.Dequeue());
                    sw.Close();
                }
                Thread.Sleep(1000);
            }
        }

        static bool ClientReady(Client ci)
        {
            ci.ReadBytes += ReadBytes;
            ci.ConnectClosed += ConnectionClosed;
            return true;
        }
        static void ConnectionClosed(Client ci)
        {
            m_clients.Remove(ci.Name);
            m_participating.Remove(ci.Name);
            if (ci.ID == vip_id) FindVIP();
            Log(ci.Name + " left the server.");
            if (m_players.Remove(ci.Name)) {
                m_server.Broadcast(Parser.ToBytes(Command.Disconnect, ci.Name));
                if (m_game != null && m_game.Contains(ci.Name)) {
                    bool inVote = !m_game.VotingComplete;
                    if (m_game?.Kill(ci.Name) == true) {
                        m_server.Broadcast(Parser.ToBytes(Command.General, ci.Name + " has been disconnected and was hitler"));
                        SendWinner("Hitler has left so the Liberals win.");
                    } else {
                        m_server.Broadcast(Parser.ToBytes(Command.General, ci.Name + " has been disconnected"));
                        if (inVote) { //during voting
                            if (ci.Name == m_game?.NextPrez || ci.Name == m_nextChanc) {
                                //skip vote and legislative session
                                m_game.NextPrezResult(false);
                                NotifyNextPrez();
                            } else CheckVoting();
                        } else if (m_nextChanc == m_game.Chancellor) { //during legislative sesion
                            if (ci.Name == m_game.President || ci.Name == m_game.Chancellor) {
                                //skip legislative session
                                m_game.Discard(m_policies);
                                m_policies.Clear();
                                NotifyNextPrez();
                            }
                        } else if (ci.Name == m_game.NextPrez || ci.Name == m_nextChanc) { //between legislative session and voting
                            //skip vote and legislative session
                            m_game.NextPrezResult(false);
                            NotifyNextPrez();
                        }
                    }
                }
                TryJoining();
            }
        }
    }
}
