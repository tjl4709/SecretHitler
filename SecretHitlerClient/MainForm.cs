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

        public MainForm()
        {
            m_players = new List<string>(10);
            InitializeComponent();
        }

        private void ReadBytes(ClientInfo ci, byte[] data, int len)
        {
            if (data.Length == 0) {
                ci.Send(Parser.ErrMsg("Missing command code"));
                return;
            }
            switch ((Command)data[0]) {
                case Command.Name: {
                    if (data.Length == 2) {
                        if (data[1] == 0) {
                            ErrorLabel.Invoke(new Action(() => ErrorLabel.ForeColor = Color.Red));
                            ErrorLabel.Invoke(new Action(() => ErrorLabel.Text = "Username already taken."));
                        } else
                            LoginPanel.Invoke(new MethodInvoker(LoginPanel.Hide));
                    } else
                        m_players.AddRange(Parser.ToString(data).Split(','));
                    break;
                }
                case Command.VIP:
                    Invoke(new Action(() => Text += ": VIP"));
                    break;
                default:
                    ci.Send(Parser.ErrMsg("Unrecognized command: " + data[0].ToString("X")));
                    break;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_ci != null) m_ci.Close();
        }

        //login panel
        private void UsernameEdit_KeyPress(object sender, KeyPressEventArgs e)
        { if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '\b') e.Handled = true; }
        private void PortEdit_KeyPress(object sender, KeyPressEventArgs e)
        { if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b') e.Handled = true; }
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (m_ci == null) {
                ErrorLabel.Text = "";
                ErrorLabel.ForeColor = Color.Red;
                if (PortEdit.Text.Length == 0)
                    ErrorLabel.Text = "Must enter port.";
                else if (!int.TryParse(PortEdit.Text, out int port) || port < 0 || port > 65535)
                    ErrorLabel.Text = "Port must be a number in the range 0-65535.";
                else if (UsernameEdit.Text.Length < 2 || UsernameEdit.Text.Length > 15)
                    ErrorLabel.Text = "Username must be 2-15 characters long.";
                else {
                    ErrorLabel.ForeColor = Color.Black;
                    ErrorLabel.Text = "Connecting...";
                    ErrorLabel.Visible = true;
                    try {
                        Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        sock.Connect(IPEdit.IPAddress, port);
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
