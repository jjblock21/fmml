
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
            this.status = new DarkUI.Controls.DarkLabel();
            this.refreshButton = new DarkUI.Controls.DarkButton();
            this.injectButton = new DarkUI.Controls.DarkButton();
            this.ejectButton = new DarkUI.Controls.DarkButton();
            this.imgBox = new System.Windows.Forms.PictureBox();
            this.imgBorder = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.imgBox)).BeginInit();
            this.imgBorder.SuspendLayout();
            this.SuspendLayout();
            // 
            // status
            // 
            resources.ApplyResources(this.status, "status");
            this.status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.status.Name = "status";
            // 
            // refreshButton
            // 
            resources.ApplyResources(this.refreshButton, "refreshButton");
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // injectButton
            // 
            resources.ApplyResources(this.injectButton, "injectButton");
            this.injectButton.Name = "injectButton";
            this.injectButton.Click += new System.EventHandler(this.injectButton_Click);
            // 
            // ejectButton
            // 
            resources.ApplyResources(this.ejectButton, "ejectButton");
            this.ejectButton.Name = "ejectButton";
            this.ejectButton.Click += new System.EventHandler(this.ejectButton_Click);
            // 
            // imgBox
            // 
            this.imgBox.Image = global::Fireworks_Mania_Modloader.Properties.Resources.img;
            resources.ApplyResources(this.imgBox, "imgBox");
            this.imgBox.Name = "imgBox";
            this.imgBox.TabStop = false;
            // 
            // imgBorder
            // 
            this.imgBorder.BackColor = System.Drawing.Color.Silver;
            this.imgBorder.Controls.Add(this.imgBox);
            this.imgBorder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            resources.ApplyResources(this.imgBorder, "imgBorder");
            this.imgBorder.Name = "imgBorder";
            // 
            // Window
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.imgBorder);
            this.Controls.Add(this.ejectButton);
            this.Controls.Add(this.injectButton);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.status);
            this.MaximizeBox = false;
            this.Name = "Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Exit);
            this.Load += new System.EventHandler(this.Start);
            this.Resize += new System.EventHandler(this.Window_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.imgBox)).EndInit();
            this.imgBorder.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DarkUI.Controls.DarkLabel status;
        private DarkUI.Controls.DarkButton refreshButton;
        private DarkUI.Controls.DarkButton injectButton;
        private DarkUI.Controls.DarkButton ejectButton;
        private System.Windows.Forms.PictureBox imgBox;
        private System.Windows.Forms.Panel imgBorder;
    }
}

