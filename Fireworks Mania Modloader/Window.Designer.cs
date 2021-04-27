
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.discordLink = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.simpleGradientPanel1 = new JControls.SimpleGradientPanel();
            this.refreshButton = new System.Windows.Forms.Button();
            this.injectButton = new System.Windows.Forms.Button();
            this.ejectButton = new System.Windows.Forms.Button();
            this.simpleTransparentGradientPanel1 = new JControls.SimpleTransparentGradientPanel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // status
            // 
            resources.ApplyResources(this.status, "status");
            this.status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.status.Name = "status";
            this.status.Click += new System.EventHandler(this.status_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.discordLink);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Name = "label1";
            // 
            // discordLink
            // 
            this.discordLink.ActiveLinkColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.discordLink, "discordLink");
            this.discordLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.discordLink.LinkColor = System.Drawing.Color.White;
            this.discordLink.Name = "discordLink";
            this.discordLink.TabStop = true;
            this.discordLink.VisitedLinkColor = System.Drawing.Color.White;
            this.discordLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.discordLink_LinkClicked);
            this.discordLink.MouseEnter += new System.EventHandler(this.discordLink_MouseEnter);
            this.discordLink.MouseLeave += new System.EventHandler(this.discordLink_MouseLeave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Fireworks_Mania_Modloader.Properties.Resources.img;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // simpleGradientPanel1
            // 
            this.simpleGradientPanel1.Angle = 90F;
            this.simpleGradientPanel1.DoubleTitled = false;
            this.simpleGradientPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            resources.ApplyResources(this.simpleGradientPanel1, "simpleGradientPanel1");
            this.simpleGradientPanel1.Name = "simpleGradientPanel1";
            this.simpleGradientPanel1.StartColor = System.Drawing.Color.Transparent;
            this.simpleGradientPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.simpleGradientPanel1_Paint);
            // 
            // refreshButton
            // 
            this.refreshButton.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.refreshButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.refreshButton.FlatAppearance.BorderSize = 2;
            this.refreshButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.refreshButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.refreshButton, "refreshButton");
            this.refreshButton.ForeColor = System.Drawing.Color.White;
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.UseCompatibleTextRendering = true;
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            this.refreshButton.MouseEnter += new System.EventHandler(this.refreshButton_MouseEnter);
            this.refreshButton.MouseLeave += new System.EventHandler(this.refreshButton_MouseLeave);
            // 
            // injectButton
            // 
            this.injectButton.BackColor = System.Drawing.Color.Gainsboro;
            this.injectButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.injectButton, "injectButton");
            this.injectButton.Name = "injectButton";
            this.injectButton.UseVisualStyleBackColor = false;
            this.injectButton.Click += new System.EventHandler(this.injectButton_Click);
            // 
            // ejectButton
            // 
            this.ejectButton.BackColor = System.Drawing.Color.Gainsboro;
            this.ejectButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.ejectButton, "ejectButton");
            this.ejectButton.Name = "ejectButton";
            this.ejectButton.UseVisualStyleBackColor = false;
            this.ejectButton.Click += new System.EventHandler(this.ejectButton_Click);
            // 
            // simpleTransparentGradientPanel1
            // 
            this.simpleTransparentGradientPanel1.Angle = 90F;
            this.simpleTransparentGradientPanel1.DoubleTitled = false;
            this.simpleTransparentGradientPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            resources.ApplyResources(this.simpleTransparentGradientPanel1, "simpleTransparentGradientPanel1");
            this.simpleTransparentGradientPanel1.Name = "simpleTransparentGradientPanel1";
            this.simpleTransparentGradientPanel1.StartColor = System.Drawing.Color.Transparent;
            // 
            // Window
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.Controls.Add(this.simpleTransparentGradientPanel1);
            this.Controls.Add(this.ejectButton);
            this.Controls.Add(this.injectButton);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.status);
            this.Controls.Add(this.simpleGradientPanel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Exit);
            this.Load += new System.EventHandler(this.Start);
            this.Resize += new System.EventHandler(this.Window_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label status;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel discordLink;
        private PictureBox pictureBox1;
        private JControls.SimpleGradientPanel simpleGradientPanel1;
        private Button refreshButton;
        private Button injectButton;
        private Button ejectButton;
        private JControls.SimpleTransparentGradientPanel simpleTransparentGradientPanel1;
        private Label label1;
    }
}

