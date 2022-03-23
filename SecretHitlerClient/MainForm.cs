using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        List<string> m_players;
        bool m_vip;
        Role m_role;
        Popup m_popup;

        public MainForm(string[] args = null)
        {
            m_vip = false;
            m_players = new List<string>(10);
            InitializeComponent();
            UsernameDisplay.Text = "";
            MainForm_Resize(this, EventArgs.Empty);
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
            for (int cmdStart = 0; cmdStart < data.Length; cmdStart += data[cmdStart] + 1) {
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
                                if (!m_players.Remove(player))
                                    m_players.Add(player);
                            UpdatePlayerListBox();
                        }
                        break;
                    }
                    case Command.Start: {
                        m_role = (Role)cmd[1];
                        OpenRolePopup();
                        PlayerListToGame();
                        break;
                    }
                    case Command.VIP:
                        m_vip = true;
                        ShowVIP();
                        break;
                    default:
                        ci.Send(Parser.ErrMsg("Unrecognized command: " + cmd[0].ToString("X")));
                        break;
                }
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            FascistBoard.Height += GamePanel.Height / 2 - FascistBoard.Bottom;
            FascistBoard.Width = (int)Math.Round((double)FascistBoard.Height * LiberalBoard.Image.Width / LiberalBoard.Image.Height);
            LiberalBoard.Height = FascistBoard.Height;
            LiberalBoard.Width = FascistBoard.Width;
            LiberalBoard.Location = new Point(FascistBoard.Left, FascistBoard.Bottom);
            if (m_popup != null)
                m_popup.Location = new Point((GamePanel.Width - m_popup.Width) / 2, (GamePanel.Height - m_popup.Height) / 2);
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_ci != null) m_ci.Close();
        }

        //popups
        private void OpenRolePopup()
        {
            string title = "Party Membership and Secret Role";
            if (m_role == Role.Liberal)
                OpenPopup("You are a Liberal. Enact 5 Liberal policies or execute Hitler.",
                    title, new Bitmap[] { Resources.lpm, Resources.lsr });
            else
                OpenPopup("You are a Fascist. Enact 6 Fascist policies or elect Hitler as" +
                    " Chancellor after enacting 3 Fascist policies", title, new Bitmap[]
                    { Resources.fpm, m_role==Role.Fascist ? Resources.fsr : Resources.hsr});
        }
        private void OpenPopup(string caption, string title, Bitmap[] imgs)
        {
            if (InvokeRequired) {
                Invoke(new Action(() => OpenPopup(caption, title, imgs)));
                return;
            }
            while (m_popup != null) Thread.Sleep(500);
            m_popup = new Popup(caption, title, imgs);
            m_popup.CloseButton.Click += ClosePopup;
            m_popup.AdjustSize();
            m_popup.Location = new Point((GamePanel.Width - m_popup.Width) / 2, (GamePanel.Height - m_popup.Height) / 2);
            Controls.Add(m_popup);
            m_popup.Show();
        }
        private void ClosePopup(object sender, EventArgs e)
        {
            if (m_popup.InvokeRequired) {
                Invoke(new Action(() => Controls.Remove(m_popup)));
                m_popup.Invoke(new MethodInvoker(m_popup.Dispose));
            } else {
                Controls.Remove(m_popup);
                m_popup.Dispose();
            }
            m_popup = null;
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
        }
        private void PlayerListToGame()
        {
            if (InvokeRequired) {
                Invoke(new Action(PlayerListToGame));
                return;
            }
            PlayerListPanel.Hide();
            RoleDisplay.Text = m_role.ToString();
            if (m_players.Count < 7)
                FascistBoard.Image = Properties.Resources.fb5;
            else if (m_players.Count < 9)
                FascistBoard.Image = Properties.Resources.fb7;
            else
                FascistBoard.Image = Properties.Resources.fb9;
            GamePanel.Show();
        }
        //update UI elements
        private void ShowVIP()
        {
            if (InvokeRequired) {
                Invoke(new MethodInvoker(ShowVIP));
                return;
            }
            Text += ": VIP";
            StartButton.Visible = true;
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
                        m_ci = new ClientInfo(sock, null, ReadBytes, ClientDirection.Both, true, EncryptionType.ServerRSAClientKey);
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
            StartButton.Enabled = m_players.Count >= 5;
            PlayerListBox.Items.Clear();
            PlayerListBox.Items.AddRange(m_players.ToArray());
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            if (StartButton.Text == "Start") {
                m_ci.Send(new byte[] { 1, (byte)Command.Start });
                StartButton.Text = "Starting...";
            }
        }
    }
}
