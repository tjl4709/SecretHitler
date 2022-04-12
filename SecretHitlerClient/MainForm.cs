using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RedCorona.Net;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using SecretHitlerUtilities;
using SecretHitlerClient.Properties;

namespace SecretHitlerClient
{
    public partial class MainForm : Form
    {
        ClientInfo m_ci;
        List<string> m_players, m_connected;
        bool m_vip, m_gameover, m_audience, m_participation;
        Role m_role;
        readonly Thread m_popHand;
        readonly Queue<Popup> m_popups;
        Popup m_currPop;
        readonly PictureBox[] m_libPols, m_fascPols;
        string m_pres, m_chanc, m_lastPres, m_lastChanc, m_investigated;
        int m_electTrack, m_nLibPol, m_nFascPol;
        Bitmap[] m_vetoed;
        string[] m_fascists;

        public MainForm(string[] args = null)
        {
            m_vip = m_gameover = m_audience = false;
            m_participation = true;
            m_electTrack = 0;
            m_connected = new List<string>(10);
            m_popups = new Queue<Popup>();
            m_popHand = new Thread(OpenPopups);
            //add policy picture boxes
            m_libPols = new PictureBox[5];
            for (int i = 0; i < m_libPols.Length; i++) {
                m_libPols[i] = new PictureBox() {
                    Image = Resources.lpol,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Visible = false,
                    BackColor = i < 4 ? Color.FromArgb(97,200,217) : Color.FromArgb(0,78,110)
                };
                m_libPols[i].BringToFront();
            }
            m_fascPols = new PictureBox[6];
            for (int i = 0; i < m_fascPols.Length; i++) {
                m_fascPols[i] = new PictureBox() {
                    Image = Resources.fpol,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Visible = false,
                    BackColor = i < 3 ? Color.FromArgb(230,100,67) : Color.FromArgb(153,2,0)
                };
                m_fascPols[i].BringToFront();
            }
            InitializeComponent();
            GamePanel.Controls.AddRange(m_libPols);
            GamePanel.Controls.AddRange(m_fascPols);
            UsernameDisplay.Text = "";
            //parse args
            if (args != null && args.Length > 0) {
                int i;
                for (i = 0; i < args[0].Length; i++)
                    if (!char.IsDigit(args[0][i]) && args[0][i] != '.' && args[0][i] != ':')
                        args[0] = args[0].Remove(i--, 1);
                IPPortEdit.Text = args[0];
                if (args.Length > 1) {
                    for (i = 0; i < args[1].Length; i++)
                        if (!char.IsLetterOrDigit(args[1][i]))
                            args[1] = args[1].Remove(i--, 1);
                    UsernameEdit.Text = args[1];
                    SubmitButton_Click(this, EventArgs.Empty);
                }
            }
        }

        private void ReadBytes(ClientInfo ci, byte[] data, int len)
        {
            byte[] cmd;
            for (int cmdStart = 0; cmdStart < len; cmdStart += data[cmdStart] + 1) {
                cmd = new byte[data[cmdStart]];
                Array.ConstrainedCopy(data, cmdStart + 1, cmd, 0, data[cmdStart]);
                switch ((Command)cmd[0]) {
                    case Command.Name: {
                        if (cmd.Length == 2) {
                            if (cmd[1] == 0) {
                                ErrorLabel.Invoke(new Action(() => ErrorLabel.ForeColor = Color.Red));
                                ErrorLabel.Invoke(new Action(() => ErrorLabel.Text = "Username already taken."));
                            } else LoginToPlayerList();
                        } else {
                            foreach (string player in Parser.ToString(cmd).Split(','))
                                if (!m_connected.Contains(player))
                                    m_connected.Add(player);
                            UpdatePlayerListBox();
                        }
                        break;
                    }
                    case Command.Start: {
                        m_gameover = false;
                        m_electTrack = m_nLibPol = m_nFascPol = 0;
                        m_audience = (m_role = (Role)cmd[1]) == Role.Audience;
                        m_pres = m_chanc = m_lastPres = m_lastChanc = "";
                        OpenRolePopup();
                        if (!m_popHand.IsAlive) m_popHand.Start();
                        if (cmd.Length > 2) {
                            m_fascists = Parser.ToString(cmd, 2).Split(new char[] { (char)Role.Fascist }, StringSplitOptions.RemoveEmptyEntries);
                            if ((Role)cmd.Last() == Role.Hitler)
                                m_fascists[m_fascists.Length - 1] = m_fascists[m_fascists.Length - 1].Substring(0, m_fascists[m_fascists.Length - 1].Length - 1) + " (Hitler)";
                            Array.Sort(m_fascists);
                            m_popups.Enqueue(new Popup(string.Join("\n", m_fascists), "Fellow Fascists"));
                            for (int i = 0; i < m_fascists.Length; i++)
                                if (m_fascists[i][m_fascists[i].Length - 1] == ')')
                                    m_fascists[i] = m_fascists[i].Substring(0, m_fascists[i].Length - 9);
                        } else m_fascists = null;
                        PlayerListToGame();
                        break;
                    }
                    case Command.PosAssign: {
                        m_lastPres = m_pres;
                        m_pres = Parser.ToString(cmd);
                        UpdatePlayerTable();
                        if (m_pres == UsernameDisplay.Text) {
                            List<string> nominees = new List<string>(m_players);
                            if (m_players.Count > 5) nominees.Remove(m_lastPres);
                            nominees.Remove(m_chanc);
                            OpenPlayerSelectPopup(nominees, "Nominate", NominateButton_Click,
                                "Choose a player to nominate as Chancellor:", "Chancellor Nomination");
                            PosDisplay.Image = Resources.pres;
                        } else PosDisplay.Image = null;
                        break;
                    }
                    case Command.Vote: {
                        m_lastChanc = m_chanc;
                        m_chanc = Parser.ToString(cmd);
                        if (m_chanc == UsernameDisplay.Text)
                            PosDisplay.Image = Resources.chanc;
                        UpdatePlayerTable();
                        if (!m_audience)
                            OpenVotePopup();
                        break;
                    }
                    case Command.VoteCnt: {
                        string[] pros = cmd.Length > 1 ? Parser.ToString(cmd).Split(',') : new string[]{ };
                        List<string> antis = new List<string>(m_players);
                        antis.Add(UsernameDisplay.Text);
                        for (int i = 0; i < pros.Length; i++) antis.Remove(pros[i]);
                        string passfail;
                        if (pros.Length > (m_players.Count + 1) / 2) {
                            passfail = "pass";
                        } else {
                            m_electTrack++;
                            m_pres = m_lastPres;
                            m_chanc = m_lastChanc;
                            passfail = "fail";
                        }
                        UpdateStatusMsg($"The vote {passfail}ed with {pros.Length}/{m_players.Count+1}.", Color.Black);
                        OpenVoteCntPopup(pros, antis.ToArray());
                        MainForm_Resize(this, EventArgs.Empty);
                        break;
                    }
                    case Command.Policy: {
                        if (cmd.Length == 4) {
                            if (m_pres == UsernameDisplay.Text) {
                                OpenPolicyPopup(cmd);
                            } else ci.Send(Parser.ErrMsg("I'm not the President"));
                        } else if (cmd.Length == 3) {
                            if (m_chanc == UsernameDisplay.Text) {
                                OpenPolicyPopup(cmd);
                            } else ci.Send(Parser.ErrMsg("I'm not the Chancellor"));
                        } else EnactPolicy((Role)cmd[1] == Role.Liberal);
                        break;
                    }
                    case Command.FascPow: {
                        bool isPres = m_pres == UsernameDisplay.Text;
                        if (cmd.Length == 2 && isPres) {
                            List<string> players = new List<string>(m_players);
                            switch ((FascistPowers)cmd[1]) {
                                case FascistPowers.InvestigateLoyalty:
                                    players.Remove(m_investigated);
                                    OpenPlayerSelectPopup(players, "Investigate", InvestigateBtn_Click,
                                        "Choose a player to investigate:", "Investigate Loyalty");
                                    break;
                                case FascistPowers.SpecialElection:
                                    OpenPlayerSelectPopup(players, "Elect", ElectButton_Click,
                                        "Choose a player to elect as the next President:", "Special Election");
                                    break;
                                case FascistPowers.Execution:
                                    players.Add(UsernameDisplay.Text);
                                    OpenPlayerSelectPopup(players, "Execute", ExecuteButton_Click, "Choose a player to execute:", "Execution");
                                    break;
                                case FascistPowers.Veto:
                                    OpenVetoPopup();
                                    break;
                            }
                        } else if (cmd.Length > 2 && (FascistPowers)cmd[1] == FascistPowers.Execution) {
                            string player = Parser.ToString(cmd, 2);
                            if (player == UsernameDisplay.Text) {
                                RoleDisplay.Text = "Audience";
                                PictureBox pb = new PictureBox() {
                                    Height = 200,
                                    SizeMode = PictureBoxSizeMode.Zoom,
                                    BackgroundImageLayout = ImageLayout.Zoom,
                                    Image = Resources.blood,
                                    BackgroundImage = m_role == Role.Liberal ? Resources.lsr : m_role == Role.Fascist ? Resources.fsr : Resources.hsr
                                };
                                m_audience = true;
                                pb.Width = (int)Math.Round((double)pb.Height * pb.BackgroundImage.Width / pb.BackgroundImage.Height);
                                m_popups.Enqueue(new Popup("You can stay and watch or leave the game.", "You have been executed", pb, true));
                            } else {
                                m_players.Remove(player);
                                UpdatePlayerTable();
                            }
                            UpdateStatusMsg(m_pres + " executed " + player, Color.Black);
                        } else if ((FascistPowers)cmd[1] == FascistPowers.InvestigateLoyalty) {
                            if (isPres && cmd.Length == 3) {
                                string cap = m_investigated + " is a ", title = "Investigate Loyalty";
                                if ((Role)cmd[2] == Role.Liberal) {
                                    m_popups.Enqueue(new Popup(cap + Role.Liberal, title, new Bitmap[] { Resources.lpm }));
                                } else
                                    m_popups.Enqueue(new Popup(cap + Role.Fascist, title, new Bitmap[] { Resources.fpm }));
                            } else {
                                m_investigated = Parser.ToString(cmd, 2);
                                UpdateStatusMsg(m_pres + " is investigating " + m_investigated, Color.Black);
                            }
                        } else if (isPres && (FascistPowers)cmd[1] == FascistPowers.PolicyPeek) {
                            Bitmap[] imgs = new Bitmap[3];
                            for (int i = 2; i < cmd.Length; i++)
                                imgs[i-2] = (Role)cmd[i] == Role.Liberal ? Resources.lpol : Resources.fpol;
                            m_popups.Enqueue(new Popup("These are the next 3 policies to be drawn.", "Policy Peek", imgs));
                        } else if ((FascistPowers)cmd[1] == FascistPowers.Veto)
                            if (cmd[2] == 1) {
                                m_electTrack++;
                                MainForm_Resize(this, EventArgs.Empty);
                            } else if (m_chanc == UsernameDisplay.Text)
                                OpenPolicyPopup(cmd);
                        break;
                    }
                    case Command.Winner: {
                        string caption;
                        if (m_role == Role.Audience)
                            caption = "";
                        else if (((Role)cmd[1] == Role.Liberal) == (m_role == Role.Liberal))
                            caption = "You won!";
                        else caption = "You lost.";
                        m_gameover = true;
                        m_popups.Clear();
                        if (m_currPop != null) ClosePopup(this, EventArgs.Empty);
                        m_popups.Enqueue(new Popup(caption, $"The {(Role)cmd[1]}s won", new Bitmap[]
                            { (Role)cmd[1] == Role.Liberal ? Resources.lpm : Resources.fpm }));
                        break;
                    }
                    case Command.VIP:
                        ShowVIP(true);
                        break;
                    case Command.Update: {
                        m_role = Role.Audience;
                        OpenRolePopup();
                        if (!m_popHand.IsAlive) m_popHand.Start();
                        string[] presChanc = Parser.ToString(cmd, 3).Split(',');
                        m_pres = presChanc[0];
                        m_chanc = presChanc[1];
                        PlayerListToGame();
                        EnactPolicies(cmd[1] - '0', cmd[2] - '0');
                        break;
                    }
                    case Command.Disconnect: {
                        string player = Parser.ToString(cmd);
                        if (m_players != null) {
                            m_players.Remove(player);
                            UpdatePlayerTable();
                        }
                        if (m_currPop != null && ClearPlayer(m_currPop.Controls, player)) {
                            m_currPop.AdjustSize();
                            MainForm_Resize(this, EventArgs.Empty);
                        }
                        m_connected.Remove(player);
                        UpdatePlayerListBox();
                        break;
                    }
                    case Command.General:
                        UpdateStatusMsg(Parser.ToString(cmd), Color.Black);
                        break;
                    case Command.Error:
                        UpdateStatusMsg(Parser.ToString(cmd), Color.Red);
                        break;
                    default:
                        ci.Send(Parser.ErrMsg("Unrecognized command: 0x" + cmd[0].ToString("X")));
                        break;
                }
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (InvokeRequired) {
                Invoke(new Action(() => MainForm_Resize(sender, e)));
                return;
            }
            PosDisplay.Location = new Point(UsernameDisplay.Left - PosDisplay.Width, PosDisplay.Top);
            //resize boards
            FascistBoard.Height = GamePanel.Height / 2 - 10;
            FascistBoard.Width = (int)Math.Round((double)FascistBoard.Height * LiberalBoard.Image.Width / LiberalBoard.Image.Height);
            if (FascistBoard.Right > Math.Min(PosDisplay.Left, PlayerTable.Left) - FascistBoard.Left) {
                FascistBoard.Width = Math.Min(PosDisplay.Left, PlayerTable.Left) - 2 * FascistBoard.Left;
                FascistBoard.Height = (int)Math.Round((double)FascistBoard.Width * LiberalBoard.Image.Height / LiberalBoard.Image.Width);
            }
            FascistBoard.Location = new Point(FascistBoard.Left, GamePanel.Height / 2 - FascistBoard.Height);
            LiberalBoard.Height = FascistBoard.Height;
            LiberalBoard.Width = FascistBoard.Width;
            LiberalBoard.Location = new Point(FascistBoard.Left, FascistBoard.Bottom);
            FascistBoard.SendToBack();
            LiberalBoard.SendToBack();
            //recenter popup
            if (m_currPop != null)
                m_currPop.Location = new Point((GamePanel.Width - m_currPop.Width) / 2, (GamePanel.Height - m_currPop.Height) / 2);
            //resize and align policies
            double hFrac = 0.46;
            for (int i = 0; i < m_libPols.Length; i++) {
                m_libPols[i].Height = (int)Math.Round(hFrac * LiberalBoard.Height);
                m_libPols[i].Width = (int)Math.Round((double)m_libPols[i].Height * m_libPols[i].Image.Width / m_libPols[i].Image.Height);
                m_libPols[i].Location = new Point((int)Math.Round(LiberalBoard.Left + (0.175 + 0.134*i) * LiberalBoard.Width),
                    LiberalBoard.Top + (int)Math.Round((1 - hFrac) / 2 * LiberalBoard.Height));
            }
            for (int i = 0; i < m_fascPols.Length; i++) {
                m_fascPols[i].Height = (int)Math.Round(hFrac * FascistBoard.Height);
                m_fascPols[i].Width = (int)Math.Round((double)m_fascPols[i].Height * m_fascPols[i].Image.Width / m_fascPols[i].Image.Height);
                m_fascPols[i].Location = new Point((int)Math.Round(FascistBoard.Left + (0.108 + 0.1348*i) * FascistBoard.Width),
                    FascistBoard.Top + (int)Math.Round((1 - hFrac) / 2 * FascistBoard.Height));
            }
            //resize and align election tracker
            ElectionTrackerMarker.Width = ElectionTrackerMarker.Height = (int)Math.Round(0.101 * LiberalBoard.Height);
            ElectionTrackerMarker.Location = new Point(LiberalBoard.Left + (int)Math.Round((0.337 + 0.091 * m_electTrack)
                * LiberalBoard.Width), LiberalBoard.Top + (int)Math.Round(0.764 * LiberalBoard.Height));
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_ci != null) m_ci.Close();
            if (m_popHand.IsAlive) m_popHand.Abort();
        }

        //popups
        private void OpenRolePopup()
        {
            string title = "Party Membership and Secret Role";
            if (m_role == Role.Liberal)
                m_popups.Enqueue(new Popup("You are a Liberal. Enact 5 Liberal policies or execute Hitler.",
                    title, new Bitmap[] { Resources.lpm, Resources.lsr }));
            else if (m_role == Role.Audience) {
                m_audience = true;
                m_popups.Enqueue(new Popup("You are an Audience member. You may watch\nthe game progress, but cannot partake in it.", title));
            }  else
                m_popups.Enqueue(new Popup("You are a Fascist. Enact 6 Fascist policies or elect Hitler as" +
                    " Chancellor after enacting 3 Fascist policies", title, new Bitmap[]
                    { Resources.fpm, m_role==Role.Fascist ? Resources.fsr : Resources.hsr}));
        }
        private void OpenVotePopup()
        {
            Popup popup = new Popup("Vote for Chancellor election", $"{m_pres} nominated {m_chanc} for Chancellor",
                new Bitmap[] { Resources.nein, Resources.ja }, false);
            popup.ImageClick = VoteImage_Click;
            m_popups.Enqueue(popup);
        }
        private void OpenVoteCntPopup(string[] pros, string[] antis)
        {
            TableLayoutPanel table = new TableLayoutPanel {
                ColumnCount = 2,
                RowCount = Math.Max(pros.Length, antis.Length) + 1,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            table.Controls.Add(new Label() {
                Text = "Nein:  ",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                AutoSize = true
            }, 0, 0);
            table.Controls.Add(new Label() {
                Text = "Ja:  ",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                AutoSize = true
            }, 1, 0);
            for (int i = 0; i < antis.Length; i++)
                table.Controls.Add(new Label() {
                    Text = "    " + antis[i],
                    TextAlign = ContentAlignment.MiddleRight,
                    Dock = DockStyle.Fill,
                    AutoSize = true
                }, 0, i + 1);
            for (int i = 0; i < pros.Length; i++)
                table.Controls.Add(new Label() {
                    Text = "    " + pros[i],
                    TextAlign = ContentAlignment.MiddleRight,
                    Dock = DockStyle.Fill,
                    AutoSize = true
                }, 1, i + 1);
            m_popups.Enqueue(new Popup(StatusMsg.Text, "Election Results", table, true));
        }
        private void OpenPolicyPopup(byte[] cmd)
        {
            Popup popup;
            List<Bitmap> imgs = new List<Bitmap>(3);
            if ((Command)cmd[0] == Command.Policy)
                for (int i = 1; i < cmd.Length; i++) {
                    if ((Role)cmd[i] == Role.Liberal)
                        imgs.Add(Resources.lpol);
                    else imgs.Add(Resources.fpol);
                    imgs[i - 1].Tag = (Role)cmd[i];
                }
            else {
                imgs.Clear();
                imgs.AddRange(m_vetoed);
                m_vetoed = null;
            }
            if (cmd.Length == 3 && (Command)cmd[0] == Command.Policy) {
                Button vetoBtn = null;
                if (m_nFascPol >= 5) {
                    vetoBtn = new Button() {
                        Text = "Veto",
                        AutoSize = true,
                        AutoSizeMode = AutoSizeMode.GrowAndShrink,
                        Margin = new Padding(0)
                    };
                    vetoBtn.Click += VetoButton_Click;
                }
                popup = new Popup("Choose a policy to enact", "Legislative Session", imgs, vetoBtn);
            } else 
                popup = new Popup("Choose a policy to discard", "Legislative Session", imgs.ToArray(), false);
            popup.ImageClick = PolicyImage_Click;
            m_popups.Enqueue(popup);
        }
        private void OpenVetoPopup()
        {
            Panel panel = new Panel() {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            Button denyBtn = new Button() {
                Text = "Deny",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(0)
            }, accBtn = new Button() {
                Text = "Accept",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(0)
            };
            panel.Controls.Add(denyBtn);
            panel.Controls.Add(accBtn);
            accBtn.Location = new Point(50, 0);
            denyBtn.Click += VetoButton_Click;
            accBtn.Click += VetoButton_Click;
            m_popups.Enqueue(new Popup(m_chanc + " has requested a veto.", "Veto", panel));
        }
        private void OpenPlayerSelectPopup(List<string> players, string btnName, EventHandler btnClick, string caption, string title)
        {
            FlowLayoutPanel radios = new FlowLayoutPanel() {
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            for (int i = 0; i < players.Count; i++)
                radios.Controls.Add(new RadioButton() { Text = players[i] });
            Button button = null;
            if (btnClick != null) {
                button = new Button() {
                    Text = btnName,
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    Margin = new Padding(0)
                };
                button.Click += btnClick;
            }
            m_popups.Enqueue(new Popup(caption, title, radios, button));
        }

        //popup handling
        private bool ClearPlayer(Control.ControlCollection ctrls, string player)
        {
            bool removed = false;
            for (int i = 0; i < ctrls.Count; i++)
                if (ctrls[i].HasChildren) {
                    if (ClearPlayer(ctrls[i].Controls, player))
                        removed = true;
                } else if (ctrls[i].Text == player) {
                    removed = true;
                    if (ctrls[i].InvokeRequired)
                        ctrls[i].Invoke(new Action(() => ctrls.RemoveAt(i--)));
                    else
                        ctrls.RemoveAt(i--);
                }
            return removed;
        }
        private void OpenPopups()
        {
            while (true) {
                while (m_popups.Count == 0 || m_currPop != null) Thread.Sleep(500);
                m_currPop = m_popups.Dequeue();
                m_currPop.CloseButton.Click += ClosePopup;
                m_currPop.AdjustSize();
                m_currPop.Location = new Point((GamePanel.Width - m_currPop.Width) / 2, (GamePanel.Height - m_currPop.Height) / 2);
                Invoke(new Action(() => Controls.Add(m_currPop)));
                m_currPop.Show();
            }
        }
        private void ClosePopup(object sender, EventArgs e)
        {
            bool isWinMsg = m_currPop.TitleText.Contains("won");
            if (m_currPop.InvokeRequired) {
                Invoke(new Action(() => Controls.Remove(m_currPop)));
                m_currPop.Invoke(new MethodInvoker(m_currPop.Dispose));
            } else {
                Controls.Remove(m_currPop);
                m_currPop.Dispose();
            }
            m_currPop = null;
            if (m_gameover && isWinMsg)
                GameToPlayerList();
        }

        //panel transitions
        private void LoginToPlayerList()
        {
            if (InvokeRequired) {
                Invoke(new Action(LoginToPlayerList));
                return;
            }
            LoginPanel.Hide();
            StartButton.Visible = m_vip;
            PlayerListPanel.Show();
            UsernameDisplay.Text = UsernameEdit.Text;
            UsernameDisplay.Location = new Point(RoleDisplay.Right - UsernameDisplay.Width, UsernameDisplay.Top);
        }
        private void PlayerListToGame()
        {
            if (InvokeRequired) {
                Invoke(new MethodInvoker(PlayerListToGame));
                return;
            }
            PlayerListPanel.Hide();
            m_players = new List<string>(m_connected);
            RoleDisplay.Text = m_audience ? "Audience" : m_role.ToString();
            PosDisplay.Image = null;
            //hide policies
            for (int i = 0; i < m_libPols.Length; i++)
                m_libPols[i].Visible = false;
            for (int i = 0; i < m_fascPols.Length; i++)
                m_fascPols[i].Visible = false;
            //choose fascist board
            if (m_players.Count < 7) FascistBoard.Image = Resources.fb5;
            else if (m_players.Count < 9) FascistBoard.Image = Resources.fb7;
            else FascistBoard.Image = Resources.fb9;
            //setup PlayerTable
            m_players.Remove(UsernameDisplay.Text);
            UpdatePlayerTable();
            UpdateStatusMsg("", Color.Black);
            GamePanel.Show();
            MainForm_Resize(this, EventArgs.Empty);
        }
        private void GameToPlayerList()
        {
            if (InvokeRequired) {
                Invoke(new MethodInvoker(GameToPlayerList));
                return;
            }
            GamePanel.Hide();
            StartButton.Visible = m_vip;
            StartButton.Text = "Start";
            UpdatePlayerListBox();
            PlayerListPanel.Show();
        }
        //update UI elements
        private void ShowVIP(bool isVIP)
        {
            if (InvokeRequired) {
                Invoke(new Action(() => ShowVIP(isVIP)));
                return;
            }
            m_vip = isVIP;
            Text = "SecretHitler" + (m_vip ? ": VIP" : "");
            StartButton.Visible = m_vip;
        }

        //login panel
        private void IPPortEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            char[] chars = { /*CTRL+A*/(char)1, /*CTRL+C*/(char)3, /*CTRL+X*/(char)24, /*CTRL+Z*/(char)26, '.', ':', '\b' };
            if (!char.IsDigit(e.KeyChar) && !chars.Contains(e.KeyChar)) e.Handled = true;
            if (e.KeyChar == (char)127) {   //CTRL+BKSP
                int cursor = IPPortEdit.SelectionStart;
                IPPortEdit.Text = IPPortEdit.Text.Remove(cursor, IPPortEdit.SelectionLength);
                if (cursor > 0) {
                    int i = Math.Max(IPPortEdit.Text.LastIndexOf(':', cursor-1), IPPortEdit.Text.LastIndexOf('.', cursor-1));
                    if (i + 1 == cursor)
                        IPPortEdit.Text = IPPortEdit.Text.Remove(cursor - 1, 1);
                    else
                        IPPortEdit.Text = IPPortEdit.Text.Remove(i + 1, cursor - i - 1);
                    IPPortEdit.SelectionStart = i + 1;
                }
            } else if (e.KeyChar == (char)22) { //CTRL+V
                string paste = Clipboard.GetText();
                for (int i = 0; i < paste.Length; i++)
                    if (!char.IsDigit(paste[i]) && paste[i] != '.' && paste[i] != ':')
                        paste = paste.Remove(i--, 1);
                int cursor = IPPortEdit.SelectionStart;
                if (IPPortEdit.SelectionLength > 0)
                    IPPortEdit.Text = IPPortEdit.Text.Remove(cursor, IPPortEdit.SelectionLength);
                IPPortEdit.Text = IPPortEdit.Text.Insert(cursor, paste);
                IPPortEdit.SelectionStart = cursor + paste.Length;
            }
        }
        private void UsernameEdit_KeyPress(object sender, KeyPressEventArgs e)
        { if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '\b') e.Handled = true; }
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (m_ci == null) {
                ErrorLabel.Text = "";
                ErrorLabel.ForeColor = Color.Red;
                int colon;
                if (IPPortEdit.Text.Length == 0 || (colon = IPPortEdit.Text.IndexOf(':')) == -1)
                    ErrorLabel.Text = "Must enter IP and port in form #.#.#.#:#";
                else if (!IPAddress.TryParse(IPPortEdit.Text.Substring(0, colon), out IPAddress ip))
                    ErrorLabel.Text = "IP must be entered in the form #.#.#.#";
                else if (!int.TryParse(IPPortEdit.Text.Substring(colon + 1), out int port) || port < 0 || port > 65535)
                    ErrorLabel.Text = "Port must be a number in the range 0-65535.";
                else if (UsernameEdit.Text.Length < 2 || UsernameEdit.Text.Length > 15)
                    ErrorLabel.Text = "Username must be 2-15 characters long.";
                else {
                    ErrorLabel.ForeColor = Color.Black;
                    ErrorLabel.Text = "Connecting...";
                    ErrorLabel.Visible = true;
                    try {
                        Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        sock.Connect(ip, port);
                        m_ci = new ClientInfo(sock, null, ReadBytes, ClientDirection.Both, true);
                        while (!m_ci.EncryptionReady) Thread.Sleep(100);
                        m_ci.Send(Parser.ToBytes(Command.Name, UsernameEdit.Text));
                        ErrorLabel.Text = "Connected!";
                    } catch (Exception) {
                        Thread.Sleep(500);
                        ErrorLabel.ForeColor = Color.Red;
                        ErrorLabel.Text = "Connection Failed: Try Again.";
                    }
                }
            } else m_ci.Send(Parser.ToBytes(Command.Name, UsernameEdit.Text));
            ErrorLabel.Visible = true;
        }

        //playerlist panel
        private void UpdatePlayerListBox()
        {
            if (PlayerListBox.InvokeRequired) {
                PlayerListBox.Invoke(new MethodInvoker(UpdatePlayerListBox));
                return;
            }
            StartButton.Enabled = m_connected.Count >= 5;
            PlayerListBox.Items.Clear();
            PlayerListBox.Items.AddRange(m_connected.ToArray());
        }
        private void ParticipationBtn_Click(object sender, EventArgs e)
        {
            m_participation = !m_participation;
            m_ci.Send(new byte[]{3, (byte)Command.Settings, (byte)Setting.Participation, (byte)(m_participation ? 1 : 0)});
            ParticipationBtn.Text = (m_participation ? "Leave" : "Join") + " Game";
            if (m_vip && !m_participation) ShowVIP(false);
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            if (StartButton.Text == "Start") {
                m_ci.Send(new byte[] { 1, (byte)Command.Start });
                StartButton.Text = "Starting...";
            }
        }

        //game panel
        private void UpdatePlayerTable()
        {
            if (PlayerTable.InvokeRequired) {
                PlayerTable.Invoke(new MethodInvoker(UpdatePlayerTable));
                return;
            }
            int i;
            for (i = 0; i < m_players.Count; i++) {
                PlayerTable.GetControlFromPosition(1, i).Text = m_players[i];
                ((PictureBox)PlayerTable.GetControlFromPosition(0, i)).Image =
                    m_players[i] == m_pres ? Resources.pres : m_players[i] == m_chanc ? Resources.chanc : null;
                PlayerTable.GetControlFromPosition(1, i).ForeColor =
                    m_fascists != null && Array.IndexOf(m_fascists, m_players[i]) != -1 ? Color.Red : Color.Black;
            }
            for (; i < PlayerTable.RowCount; i++) {
                PlayerTable.GetControlFromPosition(1, i).Text = "";
                ((PictureBox)PlayerTable.GetControlFromPosition(0, i)).Image = null;
            }
        }

        private void UpdateStatusMsg(string text, Color txtCol)
        {
            if (StatusMsg.InvokeRequired) {
                StatusMsg.Invoke(new Action(() => StatusMsg.Text = text));
                StatusMsg.Invoke(new Action(() => StatusMsg.ForeColor = txtCol));
            } else {
                StatusMsg.Text = text;
                StatusMsg.ForeColor = txtCol;
            }
        }
        private void EnactPolicy(bool isLib)
        {
            if (InvokeRequired) {
                Invoke(new Action(() => EnactPolicy(isLib)));
                return;
            }
            if (isLib) {
                m_libPols[m_nLibPol].Visible = true;
                m_libPols[m_nLibPol++].BringToFront();
                LiberalBoard.SendToBack();
                UpdateStatusMsg("A Liberal policy has been enacted", Color.Black);
            } else {
                m_fascPols[m_nFascPol].Visible = true;
                m_fascPols[m_nFascPol++].BringToFront();
                FascistBoard.SendToBack();
                UpdateStatusMsg("A Fascist policy has been enacted", Color.Black);
            }
            m_electTrack = 0;
            MainForm_Resize(this, EventArgs.Empty);
        }
        private void EnactPolicies(int nLib, int nFasc)
        {
            for (; nLib > 0; nLib--) EnactPolicy(true);
            for (; nFasc > 0; nFasc--) EnactPolicy(false);
        }

        //game popup button_click handlers
        private void NominateButton_Click(object sender, EventArgs e)
        {
            if (m_currPop.InvokeRequired) {
                m_currPop.Invoke(new Action(() => NominateButton_Click(sender, e)));
                return;
            }
            string chanc = GetSelectedPlayer((FlowLayoutPanel)m_currPop.InsertCtrl);
            if (chanc == null) {
                m_currPop.Caption.ForeColor = Color.Red;
            } else {
                ClosePopup(sender, e);
                m_ci.Send(Parser.ToBytes(Command.PosAssign, chanc));
            }
        }
        private void InvestigateBtn_Click(object sender, EventArgs e)
        {
            if (m_currPop.InvokeRequired) {
                m_currPop.Invoke(new Action(() => InvestigateBtn_Click(sender, e)));
                return;
            }
            string player = GetSelectedPlayer((FlowLayoutPanel)m_currPop.InsertCtrl);
            if (player == null) {
                m_currPop.Caption.ForeColor = Color.Red;
            } else {
                ClosePopup(sender, e);
                m_investigated = player;
                m_ci.Send(Parser.FascPowToBytes(FascistPowers.InvestigateLoyalty, player));
            }
        }
        private void ElectButton_Click(object sender, EventArgs e)
        {
            if (m_currPop.InvokeRequired) {
                m_currPop.Invoke(new Action(() => ElectButton_Click(sender, e)));
                return;
            }
            string player = GetSelectedPlayer((FlowLayoutPanel)m_currPop.InsertCtrl);
            if (player == null) {
                m_currPop.Caption.ForeColor = Color.Red;
            } else {
                ClosePopup(sender, e);
                m_ci.Send(Parser.FascPowToBytes(FascistPowers.SpecialElection, player));
            }
        }
        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            if (m_currPop.InvokeRequired) {
                m_currPop.Invoke(new Action(() => ExecuteButton_Click(sender, e)));
                return;
            }
            string player = GetSelectedPlayer((FlowLayoutPanel)m_currPop.InsertCtrl);
            if (player == null) {
                m_currPop.Caption.ForeColor = Color.Red;
            } else {
                ClosePopup(sender, e);
                m_ci.Send(Parser.FascPowToBytes(FascistPowers.Execution, player));
            }
        }
        private string GetSelectedPlayer(FlowLayoutPanel radios)
        {
            string player = null;
            for (int i = 0; i < radios.Controls.Count; i++)
                if (radios.Controls[i] is RadioButton rb && rb.Checked) {
                    player = rb.Text;
                    break;
                }
            return player;
        }
        private void VetoButton_Click(object sender, EventArgs e)
        {
            if (m_chanc == UsernameDisplay.Text) {
                m_vetoed = new Bitmap[] { (Bitmap)((PictureBox)m_currPop.ImageFlowPanel.Controls[0]).Image,
                    (Bitmap)((PictureBox)m_currPop.ImageFlowPanel.Controls[1]).Image };
                m_ci.Send(new byte[] { 2, (byte)Command.FascPow, (byte)FascistPowers.Veto });
            } else if (m_pres == UsernameDisplay.Text && sender is Control ctrl && m_currPop.InsertCtrl.Controls.Contains(ctrl))
                m_ci.Send(new byte[] { 3, (byte)Command.FascPow, (byte)FascistPowers.Veto, (byte)(ctrl.Text == "Accept" ? 1 : 0) });
            ClosePopup(sender, e);
        }

        //game popup image_click handlers
        private void VoteImage_Click(object sender, EventArgs e)
        {
            if (sender is Control ctrl && m_currPop.ImageFlowPanel.Controls.Contains(ctrl)) {
                m_ci.Send(new byte[] { 2, (byte)Command.Vote, (byte)m_currPop.ImageFlowPanel.Controls.IndexOf(ctrl) });
                ClosePopup(sender, e);
            }
        }
        private void PolicyImage_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox pb) {
                if ((Role)pb.Image.Tag == Role.Fascist)
                    m_ci.Send(new byte[] { 2, (byte)Command.Policy, (byte)Role.Fascist });
                else if ((Role)pb.Image.Tag == Role.Liberal)
                    m_ci.Send(new byte[] { 2, (byte)Command.Policy, (byte)Role.Liberal });
                else return;
                ClosePopup(sender, e);
            }
        }
    }
}
