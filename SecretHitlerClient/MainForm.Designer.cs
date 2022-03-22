
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
            this.LoginPanel.SuspendLayout();
            this.PlayerListPanel.SuspendLayout();
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
            this.LoginPanel.Margin = new System.Windows.Forms.Padding(4);
            this.LoginPanel.Name = "LoginPanel";
            this.LoginPanel.Size = new System.Drawing.Size(1067, 554);
            this.LoginPanel.TabIndex = 0;
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ErrorLabel.BackColor = System.Drawing.Color.LightSalmon;
            this.ErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.ErrorLabel.Location = new System.Drawing.Point(392, 189);
            this.ErrorLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(287, 36);
            this.ErrorLabel.TabIndex = 7;
            this.ErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ErrorLabel.Visible = false;
            // 
            // SubmitButton
            // 
            this.SubmitButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SubmitButton.Location = new System.Drawing.Point(485, 297);
            this.SubmitButton.Margin = new System.Windows.Forms.Padding(4);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(100, 28);
            this.SubmitButton.TabIndex = 6;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = true;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // UsernameEdit
            // 
            this.UsernameEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.UsernameEdit.Location = new System.Drawing.Point(471, 265);
            this.UsernameEdit.Margin = new System.Windows.Forms.Padding(4);
            this.UsernameEdit.Name = "UsernameEdit";
            this.UsernameEdit.Size = new System.Drawing.Size(206, 20);
            this.UsernameEdit.TabIndex = 5;
            this.UsernameEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UsernameEdit_KeyPress);
            // 
            // IPPortEdit
            // 
            this.IPPortEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.IPPortEdit.Location = new System.Drawing.Point(471, 238);
            this.IPPortEdit.Margin = new System.Windows.Forms.Padding(4);
            this.IPPortEdit.Name = "IPPortEdit";
            this.IPPortEdit.Size = new System.Drawing.Size(207, 20);
            this.IPPortEdit.TabIndex = 4;
            this.IPPortEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IPPortEdit_KeyPress);
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(388, 268);
            this.UsernameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(58, 13);
            this.UsernameLabel.TabIndex = 2;
            this.UsernameLabel.Text = "Username:";
            // 
            // IpPortLabel
            // 
            this.IpPortLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.IpPortLabel.AutoSize = true;
            this.IpPortLabel.Location = new System.Drawing.Point(388, 241);
            this.IpPortLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
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
            this.PlayerListPanel.Name = "PlayerListPanel";
            this.PlayerListPanel.Size = new System.Drawing.Size(1067, 554);
            this.PlayerListPanel.TabIndex = 1;
            this.PlayerListPanel.Visible = false;
            // 
            // PlayerLabel
            // 
            this.PlayerLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PlayerLabel.AutoSize = true;
            this.PlayerLabel.Location = new System.Drawing.Point(432, 129);
            this.PlayerLabel.Name = "PlayerLabel";
            this.PlayerLabel.Size = new System.Drawing.Size(44, 13);
            this.PlayerLabel.TabIndex = 2;
            this.PlayerLabel.Text = "Players:";
            // 
            // PlayerListBox
            // 
            this.PlayerListBox.FormattingEnabled = true;
            this.PlayerListBox.ItemHeight = 16;
            this.PlayerListBox.Location = new System.Drawing.Point(435, 146);
            this.PlayerListBox.Name = "PlayerListBox";
            this.PlayerListBox.Size = new System.Drawing.Size(204, 164);
            this.PlayerListBox.TabIndex = 1;
            // 
            // StartButton
            // 
            this.StartButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.StartButton.Location = new System.Drawing.Point(500, 320);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Salmon;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.PlayerListPanel);
            this.Controls.Add(this.LoginPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Secret Hitler";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.LoginPanel.ResumeLayout(false);
            this.LoginPanel.PerformLayout();
            this.PlayerListPanel.ResumeLayout(false);
            this.PlayerListPanel.PerformLayout();
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
    }
}

