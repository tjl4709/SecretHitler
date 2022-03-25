using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecretHitlerClient
{
    public partial class Popup : UserControl
    {
        public override string Text { get => Caption.Text; set => Caption.Text = value; }
        public EventHandler ImageClick { set { for (int i = 0; i < ImageFlowPanel.Controls.Count; i++) ImageFlowPanel.Controls[i].Click += value; } }
        private const int MAX_IMG_SZ = 200, MAX_CHAR_LN = 60;
        public readonly Control InsertCtrl;
        private readonly Button m_insertBtn;

        public Popup(string caption, string title)
        {
            InsertCtrl = m_insertBtn = null;
            Visible = false;
            InitializeComponent();
            Caption.Text = caption;
            Title.Text = ' ' + title;
        }
        public Popup(string caption, string title, Bitmap[] imgs, bool closeable = true) : this(caption, title)
        {
            PictureBox pb;
            double whr;
            for (int i = 0; i < imgs.Length; i++) {
                pb = new PictureBox {
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = imgs[i]
                };
                whr = (double)imgs[i].Width / imgs[i].Height;
                if (whr >= 1) {
                    pb.Width = MAX_IMG_SZ;
                    pb.Height = (int)Math.Round(MAX_IMG_SZ / whr);
                } else {
                    pb.Height = MAX_IMG_SZ;
                    pb.Width = (int)Math.Round(MAX_IMG_SZ * whr);
                }
                ImageFlowPanel.Controls.Add(pb);
            }
            CloseButton.Visible = closeable;
        }
        public Popup(string caption, string title, List<Bitmap> imgs, Button button) : this(caption, title, imgs.ToArray(), false)
        {
            if (button != null)
                Controls.Add(m_insertBtn = button);
        }
        public Popup(string caption, string title, Control control, Button button = null) : this(caption, title)
        {
            Controls.Add(InsertCtrl = control);
            if (button != null)
                Controls.Add(m_insertBtn = button);
            CloseButton.Visible = false;
        }

        public void AdjustSize()
        {
            int height = 20;
            if (Title.Text.Length == 0) Title.Visible = false;
            //image flow panel
            if (ImageFlowPanel.Controls.Count > 0) {
                ImageFlowPanel.Location = new Point(10, height + 3);
                Width = ImageFlowPanel.Width + 23;
                height = ImageFlowPanel.Bottom;
            } else ImageFlowPanel.Visible = false;
            //caption
            if (Caption.Text.Length > 0) {
                int i = 0, j, k;
                if (!Caption.Text.Contains('\n'))
                    while (i < Caption.Text.Length) {
                        j = i;
                        while ((k = Caption.Text.IndexOf(' ', j+1)) - i < MAX_CHAR_LN && k != -1)
                            j = k;
                        if (k == -1) break;
                        if (k - i > MAX_CHAR_LN && j != 0)
                            Caption.Text = Caption.Text.Remove(j, 1).Insert(j, "\n");
                        i = j;
                    }
                Width = Math.Max(Width, Caption.Width + 10);
                Caption.Location = new Point((Width - Caption.Width) / 2, height + 5);
                height = Caption.Bottom;
            } else Caption.Visible = false;
            //inserted control
            if (InsertCtrl != null) {
                InsertCtrl.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                Width = Math.Max(Width, InsertCtrl.Width + 10);
                InsertCtrl.Location = new Point((Width - InsertCtrl.Width) / 2, height + 10);
                height = InsertCtrl.Bottom;
            }
            //inserted button
            if (m_insertBtn != null) {
                m_insertBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                m_insertBtn.Location = new Point(Width - m_insertBtn.Width - 5, height + 5);
                height = m_insertBtn.Bottom - 5;
            }
            Height = height + 10;
            BackColor = SystemColors.Control;
            BringToFront();
        }
        public new void Show()
        {
            AdjustSize();
            if (InvokeRequired)
                Invoke(new MethodInvoker(base.Show));
            else base.Show();
        }
    }
}
