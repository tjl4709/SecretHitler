
namespace SecretHitlerClient
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.LoginPanel = new System.Windows.Forms.Panel();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.SubmitButton = new System.Windows.Forms.Button();
            this.UsernameEdit = new System.Windows.Forms.TextBox();
            this.IPPortEdit = new System.Windows.Forms.TextBox();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.IpPortLabel = new System.Windows.Forms.Label();
            this.PlayerListPanel = new System.Windows.Forms.Panel();
            this.PlayerLabel = new System.Windows.Forms.Label();
            this.PlayerListBox = new System.Windows.Forms.ListBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.GamePanel = new System.Windows.Forms.Panel();
            this.LiberalBoard = new System.Windows.Forms.PictureBox();
            this.FascistBoard = new System.Windows.Forms.PictureBox();
            this.RoleDisplay = new System.Windows.Forms.Label();
            this.UsernameDisplay = new System.Windows.Forms.Label();
            this.LoginPanel.SuspendLayout();
            this.PlayerListPanel.SuspendLayout();
            this.GamePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LiberalBoard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FascistBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // LoginPanel
            // 
            this.LoginPanel.Controls.Add(this.ErrorLabel);
            this.LoginPanel.Controls.Add(this.SubmitButton);
            this.LoginPanel.Controls.Add(this.UsernameEdit);
            this.LoginPanel.Controls.Add(this.IPPortEdit);
            this.LoginPanel.Controls.Add(this.UsernameLabel);
            this.LoginPanel.Controls.Add(this.IpPortLabel);
            this.LoginPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoginPanel.Location = new System.Drawing.Point(0, 0);
            this.LoginPanel.Name = "LoginPanel";
            this.LoginPanel.Size = new System.Drawing.Size(800, 450);
            this.LoginPanel.TabIndex = 0;
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ErrorLabel.BackColor = System.Drawing.Color.LightSalmon;
            this.ErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.ErrorLabel.Location = new System.Drawing.Point(294, 154);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(215, 29);
            this.ErrorLabel.TabIndex = 7;
            this.ErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ErrorLabel.Visible = false;
            // 
            // SubmitButton
            // 
            this.SubmitButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SubmitButton.Location = new System.Drawing.Point(364, 241);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(75, 23);
            this.SubmitButton.TabIndex = 6;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = true;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // UsernameEdit
            // 
            this.UsernameEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.UsernameEdit.Location = new System.Drawing.Point(353, 215);
            this.UsernameEdit.Name = "UsernameEdit";
            this.UsernameEdit.Size = new System.Drawing.Size(156, 20);
            this.UsernameEdit.TabIndex = 5;
            this.UsernameEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UsernameEdit_KeyPress);
            // 
            // IPPortEdit
            // 
            this.IPPortEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.IPPortEdit.Location = new System.Drawing.Point(353, 193);
            this.IPPortEdit.Name = "IPPortEdit";
            this.IPPortEdit.Size = new System.Drawing.Size(156, 20);
            this.IPPortEdit.TabIndex = 4;
            this.IPPortEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IPPortEdit_KeyPress);
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(291, 218);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(58, 13);
            this.UsernameLabel.TabIndex = 2;
            this.UsernameLabel.Text = "Username:";
            // 
            // IpPortLabel
            // 
            this.IpPortLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.IpPortLabel.AutoSize = true;
            this.IpPortLabel.Location = new System.Drawing.Point(291, 196);
            this.IpPortLabel.Name = "IpPortLabel";
            this.IpPortLabel.Size = new System.Drawing.Size(63, 13);
            this.IpPortLabel.TabIndex = 1;
            this.IpPortLabel.Text = "IP and Port:";
            // 
            // PlayerListPanel
            // 
            this.PlayerListPanel.Controls.Add(this.PlayerLabel);
            this.PlayerListPanel.Controls.Add(this.PlayerListBox);
            this.PlayerListPanel.Controls.Add(this.StartButton);
            this.PlayerListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlayerListPanel.Location = new System.Drawing.Point(0, 0);
            this.PlayerListPanel.Margin = new System.Windows.Forms.Padding(2);
            this.PlayerListPanel.Name = "PlayerListPanel";
            this.PlayerListPanel.Size = new System.Drawing.Size(800, 450);
            this.PlayerListPanel.TabIndex = 1;
            this.PlayerListPanel.Visible = false;
            // 
            // PlayerLabel
            // 
            this.PlayerLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PlayerLabel.AutoSize = true;
            this.PlayerLabel.Location = new System.Drawing.Point(324, 105);
            this.PlayerLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PlayerLabel.Name = "PlayerLabel";
            this.PlayerLabel.Size = new System.Drawing.Size(44, 13);
            this.PlayerLabel.TabIndex = 2;
            this.PlayerLabel.Text = "Players:";
            // 
            // PlayerListBox
            // 
            this.PlayerListBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PlayerListBox.FormattingEnabled = true;
            this.PlayerListBox.Location = new System.Drawing.Point(326, 119);
            this.PlayerListBox.Margin = new System.Windows.Forms.Padding(2);
            this.PlayerListBox.Name = "PlayerListBox";
            this.PlayerListBox.Size = new System.Drawing.Size(154, 134);
            this.PlayerListBox.TabIndex = 1;
            // 
            // StartButton
            // 
            this.StartButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.StartButton.Location = new System.Drawing.Point(375, 260);
            this.StartButton.Margin = new System.Windows.Forms.Padding(2);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(56, 19);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // GamePanel
            // 
            this.GamePanel.Controls.Add(this.LiberalBoard);
            this.GamePanel.Controls.Add(this.FascistBoard);
            this.GamePanel.Controls.Add(this.RoleDisplay);
            this.GamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GamePanel.Location = new System.Drawing.Point(0, 0);
            this.GamePanel.Margin = new System.Windows.Forms.Padding(2);
            this.GamePanel.Name = "GamePanel";
            this.GamePanel.Size = new System.Drawing.Size(800, 450);
            this.GamePanel.TabIndex = 3;
            this.GamePanel.Visible = false;
            // 
            // LiberalBoard
            // 
            this.LiberalBoard.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LiberalBoard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.LiberalBoard.Image = global::SecretHitlerClient.Properties.Resources.lb;
            this.LiberalBoard.Location = new System.Drawing.Point(9, 225);
            this.LiberalBoard.Margin = new System.Windows.Forms.Padding(2);
            this.LiberalBoard.Name = "LiberalBoard";
            this.LiberalBoard.Size = new System.Drawing.Size(540, 215);
            this.LiberalBoard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LiberalBoard.TabIndex = 3;
            this.LiberalBoard.TabStop = false;
            // 
            // FascistBoard
            // 
            this.FascistBoard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.FascistBoard.Location = new System.Drawing.Point(9, 10);
            this.FascistBoard.Margin = new System.Windows.Forms.Padding(2);
            this.FascistBoard.Name = "FascistBoard";
            this.FascistBoard.Size = new System.Drawing.Size(540, 215);
            this.FascistBoard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.FascistBoard.TabIndex = 2;
            this.FascistBoard.TabStop = false;
            // 
            // RoleDisplay
            // 
            this.RoleDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RoleDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoleDisplay.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.RoleDisplay.Location = new System.Drawing.Point(666, 31);
            this.RoleDisplay.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RoleDisplay.Name = "RoleDisplay";
            this.RoleDisplay.Size = new System.Drawing.Size(125, 24);
            this.RoleDisplay.TabIndex = 1;
            this.RoleDisplay.Text = "role";
            this.RoleDisplay.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // UsernameDisplay
            // 
            this.UsernameDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UsernameDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameDisplay.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.UsernameDisplay.Location = new System.Drawing.Point(666, 7);
            this.UsernameDisplay.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.UsernameDisplay.Name = "UsernameDisplay";
            this.UsernameDisplay.Size = new System.Drawing.Size(125, 24);
            this.UsernameDisplay.TabIndex = 0;
            this.UsernameDisplay.Text = "playername";
            this.UsernameDisplay.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Salmon;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.UsernameDisplay);
            this.Controls.Add(this.GamePanel);
            this.Controls.Add(this.PlayerListPanel);
            this.Controls.Add(this.LoginPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Secret Hitler";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.LoginPanel.ResumeLayout(false);
            this.LoginPanel.PerformLayout();
            this.PlayerListPanel.ResumeLayout(false);
            this.PlayerListPanel.PerformLayout();
            this.GamePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LiberalBoard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FascistBoard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel LoginPanel;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.Label IpPortLabel;
        private System.Windows.Forms.TextBox UsernameEdit;
        private System.Windows.Forms.TextBox IPPortEdit;
        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.Panel PlayerListPanel;
        private System.Windows.Forms.Label PlayerLabel;
        private System.Windows.Forms.ListBox PlayerListBox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Panel GamePanel;
        private System.Windows.Forms.Label UsernameDisplay;
        private System.Windows.Forms.Label RoleDisplay;
        private System.Windows.Forms.PictureBox LiberalBoard;
        private System.Windows.Forms.PictureBox FascistBoard;
    }
}

