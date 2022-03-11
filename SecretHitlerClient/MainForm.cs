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

namespace SecretHitlerClient
{
    public partial class MainForm : Form
    {
        ClientInfo ci;

        public MainForm()
        {
            Socket sock = PortDialog.ShowPortDialog();
            if (sock == null) Environment.Exit(1);
            ci = new ClientInfo(sock, null, ReadBytes, ClientDirection.Both, true, EncryptionType.ServerRSAClientKey);
            InitializeComponent();
            for (byte i = 0; i < 30;) {
                Thread.Sleep(1000);
                ci.Send(new byte[] { i++, i++ });
            }
            ci.Close();
        }

        private void ReadBytes(ClientInfo ci, byte[] data, int len)
        {

        }
    }
}
