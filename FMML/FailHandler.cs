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

        public static void HandleException(Feedback feedback, bool showStacktrace = false)
        {
            if (feedback == null || feedback.WasSuccessful) return;
            string msg = feedback.ErrorDescription;
            if (feedback.Exception != null && showStacktrace)
            {
                msg = msg + "\n\n" + feedback.Exception.Message;
            }
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
