using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecretHitlerClient
{
    public partial class PortDialog : Form
    {
        public Socket Socket;

        public PortDialog()
        {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            InitializeComponent();
        }

        private void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SubmitBtn_Click(sender, e);
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            string ipString = PortEdit.Text.Substring(0, PortEdit.Text.IndexOf(':')).Replace(" ", ""),
                portString = PortEdit.Text.Substring(PortEdit.Text.IndexOf(':') + 1).Replace(" ", "");
            if (IPAddress.TryParse(ipString, out IPAddress ip) && int.TryParse(portString, out int port)) {
                Text = "Connecting...";
                try {
                    Socket.Connect(ip, port);
                    Hide();
                } catch (Exception) {
                    Text = "Connection Failed: Try Again";
                }
            }
        }

        public static Socket ShowPortDialog()
        {
            PortDialog pd = new PortDialog();
            pd.ShowDialog();
            if (!pd.Socket.Connected) return null;
            Socket sock = pd.Socket;
            pd.Close();
            return sock;
        }
    }
}
