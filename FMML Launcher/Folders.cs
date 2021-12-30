using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMML_Launcher
{
    public static class Folders
    {
        public static string update_file_path()
        {
            return Path.Combine(update_folder_path(), @"version.txt");
        }

        public static string mod_update_tmp_path()
        {
            return Path.Combine(update_folder_path(), @"tmp.zip");
        }

        public static string mod_update_path()
        {
            return Path.Combine(update_folder_path(), @"client");
        }

        public static string mod_executable_path()
        {
            return Path.Combine(update_folder_path(), @"client\mdclnt.exe");
        }

        public static string update_folder_path()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appData, @"fmml");
        }

        public static string current_update_file_url()
        {
            return @"https://raw.githubusercontent.com/jjblock21/fmmlUpdate/main/version.txt";
        }

        public static string current_mod_file_url()
        {
            return @"https://raw.githubusercontent.com/jjblock21/fmmlUpdate/main/fmmlcn.zip";
        }
    }
}
