using System.Windows.Forms;

namespace Fireworks_Mania_Modloader
{
    public class FailHandler
    {
        public static void HandleException(string description, string stacktrace = null)
        {
            string msg = description;
            if (stacktrace != null) msg = msg + "\n\n" + stacktrace;
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
