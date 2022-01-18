
using System.Windows.Forms;

namespace Fireworks_Mania_Modloader
{
    partial class Window
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window));
            this.status = new System.Windows.Forms.Label();
            this.menuPanel = new System.Windows.Forms.Panel();
            this.statusLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.moreLink = new System.Windows.Forms.LinkLabel();
            this.simpleGradientPanel1 = new JControls.SimpleGradientPanel();
            this.injectButton = new System.Windows.Forms.Button();
            this.ejectButton = new System.Windows.Forms.Button();
            this.simpleTransparentGradientPanel1 = new JControls.SimpleTransparentGradientPanel();
            this.launchGameButton = new System.Windows.Forms.Button();
            this.picture = new System.Windows.Forms.PictureBox();
            this.menuPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            this.SuspendLayout();
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.status.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.status.Location = new System.Drawing.Point(10, 169);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(187, 13);
            this.status.TabIndex = 4;
            this.status.Text = "Fireworks Mania is not currently active\r\n";
            // 
            // menuPanel
            // 
            this.menuPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.menuPanel.Controls.Add(this.statusLabel);
            this.menuPanel.Controls.Add(this.versionLabel);
            this.menuPanel.Controls.Add(this.moreLink);
            this.menuPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuPanel.Location = new System.Drawing.Point(0, 265);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(314, 26);
            this.menuPanel.TabIndex = 10;
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.statusLabel.BackColor = System.Drawing.Color.Transparent;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.statusLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.statusLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.statusLabel.Location = new System.Drawing.Point(77, 7);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(173, 13);
            this.statusLabel.TabIndex = 2;
            this.statusLabel.Text = "Uh, something went wrong\r\n\r\n\r\n";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // versionLabel
            // 
            this.versionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.versionLabel.AutoSize = true;
            this.versionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.versionLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.versionLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.versionLabel.Location = new System.Drawing.Point(3, 7);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(47, 13);
            this.versionLabel.TabIndex = 1;
            this.versionLabel.Text = "[version]";
            // 
            // moreLink
            // 
            this.moreLink.ActiveLinkColor = System.Drawing.Color.Silver;
            this.moreLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.moreLink.AutoSize = true;
            this.moreLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.moreLink.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.moreLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.moreLink.LinkColor = System.Drawing.Color.White;
            this.moreLink.Location = new System.Drawing.Point(278, 7);
            this.moreLink.Name = "moreLink";
            this.moreLink.Size = new System.Drawing.Size(31, 13);
            this.moreLink.TabIndex = 0;
            this.moreLink.TabStop = true;
            this.moreLink.Text = "More";
            this.moreLink.VisitedLinkColor = System.Drawing.Color.White;
            this.moreLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.discordLink_LinkClicked);
            this.moreLink.MouseEnter += new System.EventHandler(this.discordLink_MouseEnter);
            this.moreLink.MouseLeave += new System.EventHandler(this.discordLink_MouseLeave);
            // 
            // simpleGradientPanel1
            // 
            this.simpleGradientPanel1.Angle = 90F;
            this.simpleGradientPanel1.DoubleTitled = false;
            this.simpleGradientPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.simpleGradientPanel1.Location = new System.Drawing.Point(0, 251);
            this.simpleGradientPanel1.Name = "simpleGradientPanel1";
            this.simpleGradientPanel1.Size = new System.Drawing.Size(314, 17);
            this.simpleGradientPanel1.StartColor = System.Drawing.Color.Transparent;
            this.simpleGradientPanel1.TabIndex = 17;
            // 
            // injectButton
            // 
            this.injectButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.injectButton.BackColor = System.Drawing.Color.Gainsboro;
            this.injectButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.injectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.injectButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.injectButton.Location = new System.Drawing.Point(53, 205);
            this.injectButton.Name = "injectButton";
            this.injectButton.Size = new System.Drawing.Size(96, 23);
            this.injectButton.TabIndex = 15;
            this.injectButton.Text = "Load Mod";
            this.injectButton.UseVisualStyleBackColor = false;
            this.injectButton.Click += new System.EventHandler(this.injectButton_Click);
            // 
            // ejectButton
            // 
            this.ejectButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ejectButton.BackColor = System.Drawing.Color.Gainsboro;
            this.ejectButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.ejectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ejectButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ejectButton.Location = new System.Drawing.Point(155, 205);
            this.ejectButton.Name = "ejectButton";
            this.ejectButton.Size = new System.Drawing.Size(94, 23);
            this.ejectButton.TabIndex = 16;
            this.ejectButton.Text = "Unload Mod";
            this.ejectButton.UseVisualStyleBackColor = false;
            this.ejectButton.Click += new System.EventHandler(this.ejectButton_Click);
            // 
            // simpleTransparentGradientPanel1
            // 
            this.simpleTransparentGradientPanel1.Angle = 90F;
            this.simpleTransparentGradientPanel1.DoubleTitled = false;
            this.simpleTransparentGradientPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.simpleTransparentGradientPanel1.Location = new System.Drawing.Point(0, 64);
            this.simpleTransparentGradientPanel1.Name = "simpleTransparentGradientPanel1";
            this.simpleTransparentGradientPanel1.Size = new System.Drawing.Size(314, 91);
            this.simpleTransparentGradientPanel1.StartColor = System.Drawing.Color.Transparent;
            this.simpleTransparentGradientPanel1.TabIndex = 1;
            // 
            // launchGameButton
            // 
            this.launchGameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.launchGameButton.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.launchGameButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.launchGameButton.FlatAppearance.BorderSize = 2;
            this.launchGameButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.launchGameButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.launchGameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.launchGameButton.ForeColor = System.Drawing.Color.White;
            this.launchGameButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.launchGameButton.Location = new System.Drawing.Point(207, 164);
            this.launchGameButton.Name = "launchGameButton";
            this.launchGameButton.Size = new System.Drawing.Size(95, 23);
            this.launchGameButton.TabIndex = 14;
            this.launchGameButton.Text = "Start Game";
            this.launchGameButton.UseCompatibleTextRendering = true;
            this.launchGameButton.UseVisualStyleBackColor = true;
            this.launchGameButton.Click += new System.EventHandler(this.launchGameButton_Click);
            this.launchGameButton.MouseEnter += new System.EventHandler(this.refreshButton_MouseEnter);
            this.launchGameButton.MouseLeave += new System.EventHandler(this.refreshButton_MouseLeave);
            // 
            // picture
            // 
            this.picture.Dock = System.Windows.Forms.DockStyle.Top;
            this.picture.Image = global::Fireworks_Mania_Modloader.Properties.Resources.img1;
            this.picture.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.picture.Location = new System.Drawing.Point(0, 0);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(314, 155);
            this.picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture.TabIndex = 12;
            this.picture.TabStop = false;
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.ClientSize = new System.Drawing.Size(314, 291);
            this.Controls.Add(this.simpleTransparentGradientPanel1);
            this.Controls.Add(this.ejectButton);
            this.Controls.Add(this.injectButton);
            this.Controls.Add(this.launchGameButton);
            this.Controls.Add(this.status);
            this.Controls.Add(this.simpleGradientPanel1);
            this.Controls.Add(this.picture);
            this.Controls.Add(this.menuPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Window";
            this.Text = "Fireworks Mania Modloader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Exit);
            this.Load += new System.EventHandler(this.Start);
            this.menuPanel.ResumeLayout(false);
            this.menuPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label status;
        private System.Windows.Forms.Panel menuPanel;
        private System.Windows.Forms.LinkLabel moreLink;
        private PictureBox picture;
        private JControls.SimpleGradientPanel simpleGradientPanel1;
        private Button injectButton;
        private Button ejectButton;
        private JControls.SimpleTransparentGradientPanel simpleTransparentGradientPanel1;
        private Label versionLabel;
        private Label statusLabel;
        private Button launchGameButton;
    }
}

