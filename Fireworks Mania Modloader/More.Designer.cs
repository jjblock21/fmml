
namespace Fireworks_Mania_Modloader
{
    partial class More
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
            if (disposing && (components != null))
            {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(More));
            this.label1 = new System.Windows.Forms.Label();
            this.copyButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.changeLogsLink = new System.Windows.Forms.LinkLabel();
            this.discordLink = new System.Windows.Forms.LinkLabel();
            this.backLink = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(296, 59);
            this.label1.TabIndex = 0;
            this.label1.Text = "Are you experiencing errors or having a problem with the mod?\nSend your Player lo" +
    "g to our discord together with a description of the error.";
            // 
            // copyButton
            // 
            this.copyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.copyButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.copyButton.FlatAppearance.BorderSize = 2;
            this.copyButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.copyButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.copyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.copyButton.ForeColor = System.Drawing.Color.White;
            this.copyButton.Location = new System.Drawing.Point(318, 29);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(104, 23);
            this.copyButton.TabIndex = 1;
            this.copyButton.Text = "Copy PlayerLog";
            this.copyButton.UseVisualStyleBackColor = true;
            this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
            this.copyButton.MouseEnter += new System.EventHandler(this.copyButton_MouseEnter);
            this.copyButton.MouseLeave += new System.EventHandler(this.copyButton_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.panel1.Controls.Add(this.changeLogsLink);
            this.panel1.Controls.Add(this.discordLink);
            this.panel1.Controls.Add(this.backLink);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 210);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(434, 26);
            this.panel1.TabIndex = 14;
            // 
            // changeLogsLink
            // 
            this.changeLogsLink.ActiveLinkColor = System.Drawing.Color.Silver;
            this.changeLogsLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.changeLogsLink.AutoSize = true;
            this.changeLogsLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.changeLogsLink.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.changeLogsLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.changeLogsLink.LinkColor = System.Drawing.Color.White;
            this.changeLogsLink.Location = new System.Drawing.Point(3, 7);
            this.changeLogsLink.Name = "changeLogsLink";
            this.changeLogsLink.Size = new System.Drawing.Size(66, 13);
            this.changeLogsLink.TabIndex = 3;
            this.changeLogsLink.TabStop = true;
            this.changeLogsLink.Text = " Changelogs";
            this.changeLogsLink.VisitedLinkColor = System.Drawing.Color.White;
            this.changeLogsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.changeLogsLink_LinkClicked);
            this.changeLogsLink.MouseEnter += new System.EventHandler(this.changeLogsLink_MouseEnter);
            this.changeLogsLink.MouseLeave += new System.EventHandler(this.changeLogsLink_MouseLeave);
            // 
            // discordLink
            // 
            this.discordLink.ActiveLinkColor = System.Drawing.Color.Silver;
            this.discordLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.discordLink.AutoSize = true;
            this.discordLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.discordLink.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.discordLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.discordLink.LinkColor = System.Drawing.Color.White;
            this.discordLink.Location = new System.Drawing.Point(348, 7);
            this.discordLink.Name = "discordLink";
            this.discordLink.Size = new System.Drawing.Size(43, 13);
            this.discordLink.TabIndex = 2;
            this.discordLink.TabStop = true;
            this.discordLink.Text = "Discord";
            this.discordLink.VisitedLinkColor = System.Drawing.Color.White;
            this.discordLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.discordLink_LinkClicked);
            this.discordLink.MouseEnter += new System.EventHandler(this.discordLink_MouseEnter);
            this.discordLink.MouseLeave += new System.EventHandler(this.discordLink_MouseLeave);
            // 
            // backLink
            // 
            this.backLink.ActiveLinkColor = System.Drawing.Color.Silver;
            this.backLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.backLink.AutoSize = true;
            this.backLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.backLink.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.backLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.backLink.LinkColor = System.Drawing.Color.White;
            this.backLink.Location = new System.Drawing.Point(399, 7);
            this.backLink.Name = "backLink";
            this.backLink.Size = new System.Drawing.Size(32, 13);
            this.backLink.TabIndex = 0;
            this.backLink.TabStop = true;
            this.backLink.Text = "Back";
            this.backLink.VisitedLinkColor = System.Drawing.Color.White;
            this.backLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.backLink_LinkClicked);
            this.backLink.MouseEnter += new System.EventHandler(this.backLink_MouseEnter);
            this.backLink.MouseLeave += new System.EventHandler(this.backLink_MouseLeave);
            // 
            // More
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.ClientSize = new System.Drawing.Size(434, 236);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.copyButton);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "More";
            this.ShowIcon = false;
            this.Text = "More";
            this.Load += new System.EventHandler(this.More_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button copyButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel backLink;
        private System.Windows.Forms.LinkLabel discordLink;
        private System.Windows.Forms.LinkLabel changeLogsLink;
    }
}