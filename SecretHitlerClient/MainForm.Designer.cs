
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
            this.StatusMsg = new System.Windows.Forms.Label();
            this.ElectionTrackerMarker = new System.Windows.Forms.PictureBox();
            this.PosDisplay = new System.Windows.Forms.PictureBox();
            this.PlayerTable = new System.Windows.Forms.TableLayoutPanel();
            this.Player6Label = new System.Windows.Forms.Label();
            this.Player1Label = new System.Windows.Forms.Label();
            this.Player2Label = new System.Windows.Forms.Label();
            this.Player3Label = new System.Windows.Forms.Label();
            this.Player4Label = new System.Windows.Forms.Label();
            this.Player5Label = new System.Windows.Forms.Label();
            this.Player8Label = new System.Windows.Forms.Label();
            this.Player7Label = new System.Windows.Forms.Label();
            this.Player9Label = new System.Windows.Forms.Label();
            this.Player1Pos = new System.Windows.Forms.PictureBox();
            this.Player2Pos = new System.Windows.Forms.PictureBox();
            this.Player3Pos = new System.Windows.Forms.PictureBox();
            this.Player4Pos = new System.Windows.Forms.PictureBox();
            this.Player5Pos = new System.Windows.Forms.PictureBox();
            this.Player6Pos = new System.Windows.Forms.PictureBox();
            this.Player7Pos = new System.Windows.Forms.PictureBox();
            this.Player8Pos = new System.Windows.Forms.PictureBox();
            this.Player9Pos = new System.Windows.Forms.PictureBox();
            this.LiberalBoard = new System.Windows.Forms.PictureBox();
            this.FascistBoard = new System.Windows.Forms.PictureBox();
            this.RoleDisplay = new System.Windows.Forms.Label();
            this.UsernameDisplay = new System.Windows.Forms.Label();
            this.LoginPanel.SuspendLayout();
            this.PlayerListPanel.SuspendLayout();
            this.GamePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ElectionTrackerMarker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PosDisplay)).BeginInit();
            this.PlayerTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Player1Pos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player2Pos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player3Pos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player4Pos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player5Pos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player6Pos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player7Pos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player8Pos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player9Pos)).BeginInit();
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
            this.LoginPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LoginPanel.Name = "LoginPanel";
            this.LoginPanel.Size = new System.Drawing.Size(1067, 554);
            this.LoginPanel.TabIndex = 0;
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ErrorLabel.BackColor = System.Drawing.Color.LightSalmon;
            this.ErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.ErrorLabel.Location = new System.Drawing.Point(392, 190);
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
            this.SubmitButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.UsernameEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.UsernameEdit.Name = "UsernameEdit";
            this.UsernameEdit.Size = new System.Drawing.Size(207, 22);
            this.UsernameEdit.TabIndex = 5;
            this.UsernameEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UsernameEdit_KeyPress);
            // 
            // IPPortEdit
            // 
            this.IPPortEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.IPPortEdit.Location = new System.Drawing.Point(471, 238);
            this.IPPortEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.IPPortEdit.Name = "IPPortEdit";
            this.IPPortEdit.Size = new System.Drawing.Size(207, 22);
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
            this.UsernameLabel.Size = new System.Drawing.Size(77, 17);
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
            this.IpPortLabel.Size = new System.Drawing.Size(82, 17);
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
            this.PlayerListPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.PlayerLabel.Size = new System.Drawing.Size(59, 17);
            this.PlayerLabel.TabIndex = 2;
            this.PlayerLabel.Text = "Players:";
            // 
            // PlayerListBox
            // 
            this.PlayerListBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PlayerListBox.FormattingEnabled = true;
            this.PlayerListBox.ItemHeight = 16;
            this.PlayerListBox.Location = new System.Drawing.Point(435, 146);
            this.PlayerListBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PlayerListBox.Name = "PlayerListBox";
            this.PlayerListBox.Size = new System.Drawing.Size(204, 164);
            this.PlayerListBox.TabIndex = 1;
            // 
            // StartButton
            // 
            this.StartButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.StartButton.Location = new System.Drawing.Point(500, 320);
            this.StartButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // GamePanel
            // 
            this.GamePanel.Controls.Add(this.UsernameDisplay);
            this.GamePanel.Controls.Add(this.StatusMsg);
            this.GamePanel.Controls.Add(this.ElectionTrackerMarker);
            this.GamePanel.Controls.Add(this.PosDisplay);
            this.GamePanel.Controls.Add(this.PlayerTable);
            this.GamePanel.Controls.Add(this.LiberalBoard);
            this.GamePanel.Controls.Add(this.FascistBoard);
            this.GamePanel.Controls.Add(this.RoleDisplay);
            this.GamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GamePanel.Location = new System.Drawing.Point(0, 0);
            this.GamePanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GamePanel.Name = "GamePanel";
            this.GamePanel.Size = new System.Drawing.Size(1067, 554);
            this.GamePanel.TabIndex = 3;
            this.GamePanel.Visible = false;
            // 
            // StatusMsg
            // 
            this.StatusMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusMsg.BackColor = System.Drawing.Color.LightSalmon;
            this.StatusMsg.Location = new System.Drawing.Point(893, 466);
            this.StatusMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.StatusMsg.Name = "StatusMsg";
            this.StatusMsg.Size = new System.Drawing.Size(161, 75);
            this.StatusMsg.TabIndex = 13;
            this.StatusMsg.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ElectionTrackerMarker
            // 
            this.ElectionTrackerMarker.Image = global::SecretHitlerClient.Properties.Resources.et;
            this.ElectionTrackerMarker.Location = new System.Drawing.Point(255, 480);
            this.ElectionTrackerMarker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ElectionTrackerMarker.Name = "ElectionTrackerMarker";
            this.ElectionTrackerMarker.Size = new System.Drawing.Size(27, 25);
            this.ElectionTrackerMarker.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ElectionTrackerMarker.TabIndex = 12;
            this.ElectionTrackerMarker.TabStop = false;
            // 
            // PosDisplay
            // 
            this.PosDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PosDisplay.Location = new System.Drawing.Point(784, 10);
            this.PosDisplay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PosDisplay.Name = "PosDisplay";
            this.PosDisplay.Size = new System.Drawing.Size(115, 28);
            this.PosDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PosDisplay.TabIndex = 11;
            this.PosDisplay.TabStop = false;
            // 
            // PlayerTable
            // 
            this.PlayerTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayerTable.AutoSize = true;
            this.PlayerTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PlayerTable.ColumnCount = 2;
            this.PlayerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.PlayerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PlayerTable.Controls.Add(this.Player6Label, 1, 5);
            this.PlayerTable.Controls.Add(this.Player1Label, 1, 0);
            this.PlayerTable.Controls.Add(this.Player2Label, 1, 1);
            this.PlayerTable.Controls.Add(this.Player3Label, 1, 2);
            this.PlayerTable.Controls.Add(this.Player4Label, 1, 3);
            this.PlayerTable.Controls.Add(this.Player5Label, 1, 4);
            this.PlayerTable.Controls.Add(this.Player8Label, 1, 7);
            this.PlayerTable.Controls.Add(this.Player7Label, 1, 6);
            this.PlayerTable.Controls.Add(this.Player9Label, 1, 8);
            this.PlayerTable.Controls.Add(this.Player1Pos, 0, 0);
            this.PlayerTable.Controls.Add(this.Player2Pos, 0, 1);
            this.PlayerTable.Controls.Add(this.Player3Pos, 0, 2);
            this.PlayerTable.Controls.Add(this.Player4Pos, 0, 3);
            this.PlayerTable.Controls.Add(this.Player5Pos, 0, 4);
            this.PlayerTable.Controls.Add(this.Player6Pos, 0, 5);
            this.PlayerTable.Controls.Add(this.Player7Pos, 0, 6);
            this.PlayerTable.Controls.Add(this.Player8Pos, 0, 7);
            this.PlayerTable.Controls.Add(this.Player9Pos, 0, 8);
            this.PlayerTable.Location = new System.Drawing.Point(932, 112);
            this.PlayerTable.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PlayerTable.Name = "PlayerTable";
            this.PlayerTable.RowCount = 9;
            this.PlayerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.PlayerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.PlayerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.PlayerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.PlayerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.PlayerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.PlayerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.PlayerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.PlayerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.PlayerTable.Size = new System.Drawing.Size(123, 324);
            this.PlayerTable.TabIndex = 4;
            // 
            // Player6Label
            // 
            this.Player6Label.AutoSize = true;
            this.Player6Label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Player6Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player6Label.Location = new System.Drawing.Point(119, 180);
            this.Player6Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Player6Label.Name = "Player6Label";
            this.Player6Label.Size = new System.Drawing.Size(1, 36);
            this.Player6Label.TabIndex = 6;
            this.Player6Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Player1Label
            // 
            this.Player1Label.AutoSize = true;
            this.Player1Label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Player1Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player1Label.Location = new System.Drawing.Point(119, 0);
            this.Player1Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Player1Label.Name = "Player1Label";
            this.Player1Label.Size = new System.Drawing.Size(1, 36);
            this.Player1Label.TabIndex = 8;
            this.Player1Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Player2Label
            // 
            this.Player2Label.AutoSize = true;
            this.Player2Label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Player2Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player2Label.Location = new System.Drawing.Point(119, 36);
            this.Player2Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Player2Label.Name = "Player2Label";
            this.Player2Label.Size = new System.Drawing.Size(1, 36);
            this.Player2Label.TabIndex = 1;
            this.Player2Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Player3Label
            // 
            this.Player3Label.AutoSize = true;
            this.Player3Label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Player3Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player3Label.Location = new System.Drawing.Point(119, 72);
            this.Player3Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Player3Label.Name = "Player3Label";
            this.Player3Label.Size = new System.Drawing.Size(1, 36);
            this.Player3Label.TabIndex = 2;
            this.Player3Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Player4Label
            // 
            this.Player4Label.AutoSize = true;
            this.Player4Label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Player4Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player4Label.Location = new System.Drawing.Point(119, 108);
            this.Player4Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Player4Label.Name = "Player4Label";
            this.Player4Label.Size = new System.Drawing.Size(1, 36);
            this.Player4Label.TabIndex = 3;
            this.Player4Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Player5Label
            // 
            this.Player5Label.AutoSize = true;
            this.Player5Label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Player5Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player5Label.Location = new System.Drawing.Point(119, 144);
            this.Player5Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Player5Label.Name = "Player5Label";
            this.Player5Label.Size = new System.Drawing.Size(1, 36);
            this.Player5Label.TabIndex = 4;
            this.Player5Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Player8Label
            // 
            this.Player8Label.AutoSize = true;
            this.Player8Label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Player8Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player8Label.Location = new System.Drawing.Point(119, 252);
            this.Player8Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Player8Label.Name = "Player8Label";
            this.Player8Label.Size = new System.Drawing.Size(1, 36);
            this.Player8Label.TabIndex = 7;
            this.Player8Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Player7Label
            // 
            this.Player7Label.AutoSize = true;
            this.Player7Label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Player7Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player7Label.Location = new System.Drawing.Point(119, 216);
            this.Player7Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Player7Label.Name = "Player7Label";
            this.Player7Label.Size = new System.Drawing.Size(1, 36);
            this.Player7Label.TabIndex = 5;
            this.Player7Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Player9Label
            // 
            this.Player9Label.AutoSize = true;
            this.Player9Label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Player9Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player9Label.Location = new System.Drawing.Point(119, 288);
            this.Player9Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Player9Label.Name = "Player9Label";
            this.Player9Label.Size = new System.Drawing.Size(1, 36);
            this.Player9Label.TabIndex = 9;
            this.Player9Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Player1Pos
            // 
            this.Player1Pos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Player1Pos.Location = new System.Drawing.Point(4, 4);
            this.Player1Pos.Margin = new System.Windows.Forms.Padding(4, 4, 0, 4);
            this.Player1Pos.Name = "Player1Pos";
            this.Player1Pos.Size = new System.Drawing.Size(111, 28);
            this.Player1Pos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Player1Pos.TabIndex = 10;
            this.Player1Pos.TabStop = false;
            // 
            // Player2Pos
            // 
            this.Player2Pos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Player2Pos.Location = new System.Drawing.Point(4, 40);
            this.Player2Pos.Margin = new System.Windows.Forms.Padding(4, 4, 0, 4);
            this.Player2Pos.Name = "Player2Pos";
            this.Player2Pos.Size = new System.Drawing.Size(111, 28);
            this.Player2Pos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Player2Pos.TabIndex = 11;
            this.Player2Pos.TabStop = false;
            // 
            // Player3Pos
            // 
            this.Player3Pos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Player3Pos.Location = new System.Drawing.Point(4, 76);
            this.Player3Pos.Margin = new System.Windows.Forms.Padding(4, 4, 0, 4);
            this.Player3Pos.Name = "Player3Pos";
            this.Player3Pos.Size = new System.Drawing.Size(111, 28);
            this.Player3Pos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Player3Pos.TabIndex = 12;
            this.Player3Pos.TabStop = false;
            // 
            // Player4Pos
            // 
            this.Player4Pos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Player4Pos.Location = new System.Drawing.Point(4, 112);
            this.Player4Pos.Margin = new System.Windows.Forms.Padding(4, 4, 0, 4);
            this.Player4Pos.Name = "Player4Pos";
            this.Player4Pos.Size = new System.Drawing.Size(111, 28);
            this.Player4Pos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Player4Pos.TabIndex = 13;
            this.Player4Pos.TabStop = false;
            // 
            // Player5Pos
            // 
            this.Player5Pos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Player5Pos.Location = new System.Drawing.Point(4, 148);
            this.Player5Pos.Margin = new System.Windows.Forms.Padding(4, 4, 0, 4);
            this.Player5Pos.Name = "Player5Pos";
            this.Player5Pos.Size = new System.Drawing.Size(111, 28);
            this.Player5Pos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Player5Pos.TabIndex = 14;
            this.Player5Pos.TabStop = false;
            // 
            // Player6Pos
            // 
            this.Player6Pos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Player6Pos.Location = new System.Drawing.Point(4, 184);
            this.Player6Pos.Margin = new System.Windows.Forms.Padding(4, 4, 0, 4);
            this.Player6Pos.Name = "Player6Pos";
            this.Player6Pos.Size = new System.Drawing.Size(111, 28);
            this.Player6Pos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Player6Pos.TabIndex = 15;
            this.Player6Pos.TabStop = false;
            // 
            // Player7Pos
            // 
            this.Player7Pos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Player7Pos.Location = new System.Drawing.Point(4, 220);
            this.Player7Pos.Margin = new System.Windows.Forms.Padding(4, 4, 0, 4);
            this.Player7Pos.Name = "Player7Pos";
            this.Player7Pos.Size = new System.Drawing.Size(111, 28);
            this.Player7Pos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Player7Pos.TabIndex = 16;
            this.Player7Pos.TabStop = false;
            // 
            // Player8Pos
            // 
            this.Player8Pos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Player8Pos.Location = new System.Drawing.Point(4, 256);
            this.Player8Pos.Margin = new System.Windows.Forms.Padding(4, 4, 0, 4);
            this.Player8Pos.Name = "Player8Pos";
            this.Player8Pos.Size = new System.Drawing.Size(111, 28);
            this.Player8Pos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Player8Pos.TabIndex = 17;
            this.Player8Pos.TabStop = false;
            // 
            // Player9Pos
            // 
            this.Player9Pos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Player9Pos.Location = new System.Drawing.Point(4, 292);
            this.Player9Pos.Margin = new System.Windows.Forms.Padding(4, 4, 0, 4);
            this.Player9Pos.Name = "Player9Pos";
            this.Player9Pos.Size = new System.Drawing.Size(111, 28);
            this.Player9Pos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Player9Pos.TabIndex = 18;
            this.Player9Pos.TabStop = false;
            // 
            // LiberalBoard
            // 
            this.LiberalBoard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.LiberalBoard.Image = global::SecretHitlerClient.Properties.Resources.lb;
            this.LiberalBoard.Location = new System.Drawing.Point(12, 277);
            this.LiberalBoard.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LiberalBoard.Name = "LiberalBoard";
            this.LiberalBoard.Size = new System.Drawing.Size(720, 265);
            this.LiberalBoard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LiberalBoard.TabIndex = 3;
            this.LiberalBoard.TabStop = false;
            // 
            // FascistBoard
            // 
            this.FascistBoard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.FascistBoard.Location = new System.Drawing.Point(12, 12);
            this.FascistBoard.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FascistBoard.Name = "FascistBoard";
            this.FascistBoard.Size = new System.Drawing.Size(720, 265);
            this.FascistBoard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.FascistBoard.TabIndex = 2;
            this.FascistBoard.TabStop = false;
            // 
            // RoleDisplay
            // 
            this.RoleDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RoleDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoleDisplay.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.RoleDisplay.Location = new System.Drawing.Point(888, 38);
            this.RoleDisplay.Name = "RoleDisplay";
            this.RoleDisplay.Size = new System.Drawing.Size(167, 30);
            this.RoleDisplay.TabIndex = 1;
            this.RoleDisplay.Text = "role";
            this.RoleDisplay.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // UsernameDisplay
            // 
            this.UsernameDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UsernameDisplay.AutoSize = true;
            this.UsernameDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameDisplay.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.UsernameDisplay.Location = new System.Drawing.Point(906, 9);
            this.UsernameDisplay.Name = "UsernameDisplay";
            this.UsernameDisplay.Size = new System.Drawing.Size(149, 29);
            this.UsernameDisplay.TabIndex = 0;
            this.UsernameDisplay.Text = "playername";
            this.UsernameDisplay.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Salmon;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.GamePanel);
            this.Controls.Add(this.PlayerListPanel);
            this.Controls.Add(this.LoginPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "Secret Hitler";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.LoginPanel.ResumeLayout(false);
            this.LoginPanel.PerformLayout();
            this.PlayerListPanel.ResumeLayout(false);
            this.PlayerListPanel.PerformLayout();
            this.GamePanel.ResumeLayout(false);
            this.GamePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ElectionTrackerMarker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PosDisplay)).EndInit();
            this.PlayerTable.ResumeLayout(false);
            this.PlayerTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Player1Pos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player2Pos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player3Pos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player4Pos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player5Pos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player6Pos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player7Pos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player8Pos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player9Pos)).EndInit();
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
        private System.Windows.Forms.TableLayoutPanel PlayerTable;
        private System.Windows.Forms.Label Player6Label;
        private System.Windows.Forms.Label Player1Label;
        private System.Windows.Forms.Label Player2Label;
        private System.Windows.Forms.Label Player3Label;
        private System.Windows.Forms.Label Player4Label;
        private System.Windows.Forms.Label Player5Label;
        private System.Windows.Forms.Label Player8Label;
        private System.Windows.Forms.Label Player7Label;
        private System.Windows.Forms.Label Player9Label;
        private System.Windows.Forms.PictureBox Player1Pos;
        private System.Windows.Forms.PictureBox Player2Pos;
        private System.Windows.Forms.PictureBox Player3Pos;
        private System.Windows.Forms.PictureBox Player4Pos;
        private System.Windows.Forms.PictureBox Player5Pos;
        private System.Windows.Forms.PictureBox Player6Pos;
        private System.Windows.Forms.PictureBox Player7Pos;
        private System.Windows.Forms.PictureBox Player8Pos;
        private System.Windows.Forms.PictureBox Player9Pos;
        private System.Windows.Forms.PictureBox PosDisplay;
        private System.Windows.Forms.PictureBox ElectionTrackerMarker;
        private System.Windows.Forms.Label StatusMsg;
    }
}

