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
            if (log == null) return;
            Clipboard.SetText(log);
            MessageBox.Show("The Player log was successfully saved to your clipboard, insert it using Ctrl + V.",
                "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void changeLogsLink_MouseEnter(object sender, EventArgs e)
        {
            changeLogsLink.LinkColor = Color.Gainsboro;
        }

        private void changeLogsLink_MouseLeave(object sender, EventArgs e)
        {
            changeLogsLink.LinkColor = Color.White;
        }

        private void changeLogsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowChangelog();
        }

        #region Midend

        private Feedback LoadPlayerLog(out string contents)
        {
            try
            {
                string fp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"Low\";
                string path = fp + @"Laumania ApS\Fireworks Mania\Player.log";
                contents = null;
                if (File.Exists(path))
                    contents = File.ReadAllText(path);
                else return Feedback.GenerateErrorFeedback(69,
                    "The Playerlog file doesn't exist, please start the game once and try again.");
                return Feedback.GenerateSuccessFeedback(0);
            }
            catch (Exception e)
            {
                contents = null;
                return new Feedback(420, e, false, -1, e.Message);
            }
        }

        private void ShowChangelog()
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + @"\changelog.txt";
                if (File.Exists(path))
                {
                    string text = File.ReadAllText(path);
                    MessageBox.Show(text, Program.Version + " Changelog");
                }
                else
                {
                    MessageBox.Show("The " + Program.Version + " Changelog is currently not available.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Failed to display the Changelog because of an unexpected Error.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void moddingGuideButton_Click(object sender, EventArgs e)
        {
            Process.Start(@"https://github.com/Laumania/FireworksMania.ModTools#getting-started");
        }

        private void fmmlLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@"https://github.com/jjblock21/fmml");
        }

        private void smiLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@"https://github.com/warbler/SharpMonoInjector");
        }
    }
}