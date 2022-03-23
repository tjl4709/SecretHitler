
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
            this.RoleDisplay = new System.Windows.Forms.Label();
            this.UsernameDisplay = new System.Windows.Forms.Label();
            this.LiberalBoard = new System.Windows.Forms.PictureBox();
            this.FascistBoard = new System.Windows.Forms.PictureBox();
            this.PlayerTable = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.LoginPanel.SuspendLayout();
            this.PlayerListPanel.SuspendLayout();
            this.GamePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LiberalBoard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FascistBoard)).BeginInit();
            this.PlayerTable.SuspendLayout();
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
            this.GamePanel.Controls.Add(this.PlayerTable);
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
            // PlayerTable
            // 
            this.PlayerTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayerTable.ColumnCount = 2;
            this.PlayerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PlayerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PlayerTable.Controls.Add(this.label7, 1, 5);
            this.PlayerTable.Controls.Add(this.label1, 1, 0);
            this.PlayerTable.Controls.Add(this.label2, 1, 1);
            this.PlayerTable.Controls.Add(this.label3, 1, 2);
            this.PlayerTable.Controls.Add(this.label4, 1, 3);
            this.PlayerTable.Controls.Add(this.label5, 1, 4);
            this.PlayerTable.Controls.Add(this.label8, 1, 7);
            this.PlayerTable.Controls.Add(this.label6, 1, 6);
            this.PlayerTable.Controls.Add(this.label9, 1, 8);
            this.PlayerTable.Location = new System.Drawing.Point(554, 77);
            this.PlayerTable.Name = "PlayerTable";
            this.PlayerTable.RowCount = 9;
            this.PlayerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.PlayerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.PlayerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.PlayerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.PlayerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.PlayerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.PlayerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.PlayerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.PlayerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.PlayerTable.Size = new System.Drawing.Size(237, 268);
            this.PlayerTable.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(121, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(121, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 29);
            this.label3.TabIndex = 2;
            this.label3.Text = "label3";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(121, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 29);
            this.label4.TabIndex = 3;
            this.label4.Text = "label4";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(121, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 29);
            this.label5.TabIndex = 4;
            this.label5.Text = "label5";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(121, 174);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 29);
            this.label6.TabIndex = 5;
            this.label6.Text = "label6";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(121, 145);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 29);
            this.label7.TabIndex = 6;
            this.label7.Text = "label7";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(121, 203);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 29);
            this.label8.TabIndex = 7;
            this.label8.Text = "label8";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(121, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 29);
            this.label1.TabIndex = 8;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(121, 232);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(113, 36);
            this.label9.TabIndex = 9;
            this.label9.Text = "label9";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.PlayerTable.ResumeLayout(false);
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
        private System.Windows.Forms.TableLayoutPanel PlayerTable;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
    }
}

