
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
            this.gradient1 = new JControls.SimpleGradientPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.moddingGuideButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.fmmlLink = new System.Windows.Forms.LinkLabel();
            this.smiLink = new System.Windows.Forms.LinkLabel();
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
            this.label1.Size = new System.Drawing.Size(280, 59);
            this.label1.TabIndex = 0;
            this.label1.Text = "Experiencing errors or having problems with mods?\r\nWhen you report the issue it m" +
    "ight help to send the \r\nPlayerlog along with your question.\r\nJust click the butt" +
    "on and paste the file into discord!";
            // 
            // copyButton
            // 
            this.copyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.copyButton.BackColor = System.Drawing.Color.Gainsboro;
            this.copyButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.copyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.copyButton.ForeColor = System.Drawing.Color.Black;
            this.copyButton.Location = new System.Drawing.Point(299, 29);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(120, 23);
            this.copyButton.TabIndex = 1;
            this.copyButton.Text = "Copy PlayerLog";
            this.copyButton.UseVisualStyleBackColor = false;
            this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
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
            // gradient1
            // 
            this.gradient1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gradient1.Angle = 90F;
            this.gradient1.DoubleTitled = false;
            this.gradient1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.gradient1.Location = new System.Drawing.Point(0, 192);
            this.gradient1.Name = "gradient1";
            this.gradient1.Size = new System.Drawing.Size(434, 22);
            this.gradient1.StartColor = System.Drawing.Color.Transparent;
            this.gradient1.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(13, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(280, 41);
            this.label2.TabIndex = 16;
            this.label2.Text = "Do you want to make mods yourself?\r\nOn Laumania\'s github you can find instruction" +
    "s\r\non how make your own mod.\r\n";
            // 
            // moddingGuideButton
            // 
            this.moddingGuideButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moddingGuideButton.BackColor = System.Drawing.Color.Gainsboro;
            this.moddingGuideButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.moddingGuideButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.moddingGuideButton.ForeColor = System.Drawing.Color.Black;
            this.moddingGuideButton.Location = new System.Drawing.Point(299, 83);
            this.moddingGuideButton.Name = "moddingGuideButton";
            this.moddingGuideButton.Size = new System.Drawing.Size(120, 23);
            this.moddingGuideButton.TabIndex = 17;
            this.moddingGuideButton.Text = "Modding Guide";
            this.moddingGuideButton.UseVisualStyleBackColor = false;
            this.moddingGuideButton.Click += new System.EventHandler(this.moddingGuideButton_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(13, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(406, 23);
            this.label3.TabIndex = 18;
            this.label3.Text = "Don\'t trust this mod? Check the code on github.\r\n\r\n";
            // 
            // fmmlLink
            // 
            this.fmmlLink.ActiveLinkColor = System.Drawing.Color.RoyalBlue;
            this.fmmlLink.AutoSize = true;
            this.fmmlLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.fmmlLink.LinkColor = System.Drawing.Color.CornflowerBlue;
            this.fmmlLink.Location = new System.Drawing.Point(13, 152);
            this.fmmlLink.Name = "fmmlLink";
            this.fmmlLink.Size = new System.Drawing.Size(86, 13);
            this.fmmlLink.TabIndex = 19;
            this.fmmlLink.TabStop = true;
            this.fmmlLink.Text = "FMML on Github";
            this.fmmlLink.VisitedLinkColor = System.Drawing.Color.CornflowerBlue;
            this.fmmlLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.fmmlLink_LinkClicked);
            // 
            // smiLink
            // 
            this.smiLink.ActiveLinkColor = System.Drawing.Color.RoyalBlue;
            this.smiLink.AutoSize = true;
            this.smiLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.smiLink.LinkColor = System.Drawing.Color.CornflowerBlue;
            this.smiLink.Location = new System.Drawing.Point(105, 152);
            this.smiLink.Name = "smiLink";
            this.smiLink.Size = new System.Drawing.Size(97, 13);
            this.smiLink.TabIndex = 20;
            this.smiLink.TabStop = true;
            this.smiLink.Text = "SharpMonoInjector";
            this.smiLink.VisitedLinkColor = System.Drawing.Color.CornflowerBlue;
            this.smiLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.smiLink_LinkClicked);
            // 
            // More
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.ClientSize = new System.Drawing.Size(434, 236);
            this.Controls.Add(this.smiLink);
            this.Controls.Add(this.fmmlLink);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.moddingGuideButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gradient1);
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
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button copyButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel backLink;
        private System.Windows.Forms.LinkLabel discordLink;
        private System.Windows.Forms.LinkLabel changeLogsLink;
        private JControls.SimpleGradientPanel gradient1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button moddingGuideButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel fmmlLink;
        private System.Windows.Forms.LinkLabel smiLink;
    }
}