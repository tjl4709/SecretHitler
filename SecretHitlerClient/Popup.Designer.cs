
namespace SecretHitlerClient
{
    partial class Popup
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ImageFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.Caption = new System.Windows.Forms.Label();
            this.Title = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ImageFlowPanel
            // 
            this.ImageFlowPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ImageFlowPanel.AutoSize = true;
            this.ImageFlowPanel.Location = new System.Drawing.Point(10, 27);
            this.ImageFlowPanel.Name = "ImageFlowPanel";
            this.ImageFlowPanel.Size = new System.Drawing.Size(137, 50);
            this.ImageFlowPanel.TabIndex = 0;
            // 
            // Caption
            // 
            this.Caption.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Caption.AutoSize = true;
            this.Caption.Location = new System.Drawing.Point(55, 85);
            this.Caption.Name = "Caption";
            this.Caption.Size = new System.Drawing.Size(43, 13);
            this.Caption.TabIndex = 1;
            this.Caption.Text = "Caption";
            this.Caption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Title
            // 
            this.Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Title.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Title.Location = new System.Drawing.Point(-1, -1);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(159, 21);
            this.Title.TabIndex = 2;
            this.Title.Text = " Title";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.AutoSize = true;
            this.CloseButton.BackColor = System.Drawing.Color.Firebrick;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.ForeColor = System.Drawing.SystemColors.Control;
            this.CloseButton.Location = new System.Drawing.Point(137, 3);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(15, 13);
            this.CloseButton.TabIndex = 3;
            this.CloseButton.Text = "X";
            // 
            // Popup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.Caption);
            this.Controls.Add(this.ImageFlowPanel);
            this.Name = "Popup";
            this.Size = new System.Drawing.Size(157, 108);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel ImageFlowPanel;
        private System.Windows.Forms.Label Caption;
        private System.Windows.Forms.Label Title;
        public System.Windows.Forms.Label CloseButton;
    }
}
