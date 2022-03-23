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
        private const int MAX_IMG_SZ = 200, MAX_CHAR_LN = 55;

        public Popup(string caption, string title)
        {
            Visible = false;
            InitializeComponent();
            Caption.Text = caption;
            Title.Text = ' ' + title;
        }
        public Popup(string caption, string title, Bitmap[] imgs) : this(caption, title)
        {
            PictureBox pb;
            double whr;
            for (int i = 0; i < imgs.Length; i++) {
                pb = new PictureBox();
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Image = imgs[i];
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
        }

        public void AdjustSize()
        {
            int height = 20;
            Point ifpLoc = new Point(10, height + 3), capLoc = new Point();
            if (Title.Text.Length == 0) Title.Visible = false;
            if (ImageFlowPanel.Controls.Count > 0) {
                ImageFlowPanel.Location = ifpLoc;
                Width = ImageFlowPanel.Width + 23;
                height = ImageFlowPanel.Bottom;
            } else ImageFlowPanel.Visible = false;
            if (Caption.Text.Length > 0) {
                int i = 0, j, k;
                Caption.Text = Caption.Text.Replace('\n', ' ');
                while (i < Caption.Text.Length) {
                    j = i;
                    while ((k = Caption.Text.IndexOf(' ', j+1)) - i < MAX_CHAR_LN && k != -1)
                        j = k;
                    if (k == -1) break;
                    if (k - i > MAX_CHAR_LN && j != 0)
                        Caption.Text = Caption.Text.Remove(j, 1).Insert(j, "\n");
                    i = j;
                }
                Caption.Location = capLoc = new Point((Width - Caption.Width) / 2, height + 5);
                height = Caption.Bottom;
                Width = Math.Max(Width, Caption.Width + 10);
            } else Caption.Visible = false;
            Height = height + 10;
            ImageFlowPanel.Location = ifpLoc;
            Caption.Location = capLoc;
            BackColor = SystemColors.Control;
            BringToFront();
        }
        public new void Show() { AdjustSize(); base.Show(); }
    }
}
