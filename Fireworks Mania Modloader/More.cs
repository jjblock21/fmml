using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fireworks_Mania_Modloader
{
    public partial class More : Form
    {
        public More()
        {
            InitializeComponent();
        }

        private void backLink_MouseEnter(object sender, EventArgs e)
        {
            backLink.LinkColor = Color.Gainsboro;
        }

        private void backLink_MouseLeave(object sender, EventArgs e)
        {
            backLink.LinkColor = Color.White;
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            var feedback = LoadPlayerLog(out string log);
            FailHandler.HandleException(feedback);
            Clipboard.SetText(log);
            MessageBox.Show("The Player log was successfully saved to your clipboard, insert it using Ctrl + V.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void backLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void More_Load(object sender, EventArgs e)
        {

        }

        private void discordLink_MouseEnter(object sender, EventArgs e)
        {
            discordLink.LinkColor = Color.Gainsboro;
        }

        private void discordLink_MouseLeave(object sender, EventArgs e)
        {
            discordLink.LinkColor = Color.White;
        }

        private void discordLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://discord.gg/WDWJ4RW5Qz");
        }

        private Feedback LoadPlayerLog(out string contents)
        {
            try
            {
                string fp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"Low\";
                string path = fp + @"Laumania ApS\Fireworks Mania\Player.log";
                contents = null;
                if (File.Exists(path))
                    contents = File.ReadAllText(path);
                else return new Feedback(69, null, false, -1, "The PlayerLog file doesn't exist, please run the game before you use this feature.");
                return new Feedback(0, null, true, 0, null);
            }
            catch (Exception e)
            {
                contents = null;
                return new Feedback(420, e, false, -1, e.Message);
            }
        }
    }
}
