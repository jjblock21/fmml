using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FMML_Launcher
{
    public static class UpdateFile
    {
        public static bool NeedsUpdate()
        {
            try
            {
                string localVer = OpenUpdateFile();
                string newestVer = GetVersionFromGithub();
                if (localVer == newestVer) return false;
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "An error occured while checking for updates.\n" +
                    "Please make sure you are connected to the internet.\n" +
                    "Error message: " + e.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error
                );
                return false;
            }
        }

        private static string GetVersionFromGithub()
        {
            WebClient myBotNewVersionClient = new WebClient();
            Stream stream = myBotNewVersionClient.OpenRead(Folders.current_update_file_url());
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        private static string OpenUpdateFile()
        {
            string updateFolderPath = Folders.update_folder_path();
            string updateFilePath = Folders.update_file_path();
            string versionFromGithub = GetVersionFromGithub();

            if (!Directory.Exists(updateFolderPath))
                Directory.CreateDirectory(updateFolderPath);

            if (!File.Exists(updateFilePath))
            {
                ResetFile();
                return "none";
            }

            using (StreamReader file = new StreamReader(updateFilePath, false))
                return file.ReadLine();
        }

        public static void ResetFile()
        {
            using (StreamWriter file = new StreamWriter(Folders.update_file_path(), false))
                file.WriteLine(GetVersionFromGithub());
        }
    }
}
