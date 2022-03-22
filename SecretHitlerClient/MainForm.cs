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

namespace SecretHitlerClient
{
    public partial class MainForm : Form
    {
        ClientInfo m_ci;
        List<string> m_players;
        bool m_vip;

        public MainForm()
        {
            m_vip = false;
            m_players = new List<string>(10);
            InitializeComponent();
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_ci != null) m_ci.Close();
        }

        private void LoginToPlayerList()
        {
            if (InvokeRequired) {
                Invoke(new Action(LoginToPlayerList));
                return;
            }
            LoginPanel.Hide();
            StartButton.Visible = m_vip;
            PlayerListPanel.Show();
        }
        private void ShowVIP()
        {
            if (InvokeRequired) {
                Invoke(new MethodInvoker(ShowVIP));
                return;
            }
            Text += ": VIP";
            StartButton.Visible = true;
        }
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
    }
}
