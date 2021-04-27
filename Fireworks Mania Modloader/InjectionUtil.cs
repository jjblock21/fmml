using System;
using System.Diagnostics;
using System.IO;
using SharpMonoInjector;

namespace Fireworks_Mania_Modloader
{
    public static class InjectionUtil
    {
        public static Feedback Inject(Process process, string path, out IntPtr value)
        {
            try
            {
                value = IntPtr.Zero;
                if (CheckForFile(path) && process.Id != new int())
                {
                    Assign(out string ns, out string cn, out string mn);
                    Injector injector = new Injector(process.Id);
                    value = injector.Inject(File.ReadAllBytes(path), ns, cn, mn);
                }
                if (value != IntPtr.Zero || value != new IntPtr())
                    return Feedback.GenerateSuccessFeedback(0);
                else return Feedback.GenerateErrorFeedback(1, "File " + path + " doesn't exist.");
            }
            catch (Exception e)
            {
                value = IntPtr.Zero;
                return Feedback.GenerateErrorFeedback(2, e.Message, e);
            }
        }

        public static Feedback Eject(Process process, IntPtr assembly)
        {
            try
            {
                if (assembly != IntPtr.Zero)
                {
                    AssignEject(out string ns, out string cn, out string mn);
                    Injector injector = new Injector(process.Id);
                    injector.Eject(assembly, ns, cn, mn);
                    return Feedback.GenerateSuccessFeedback(0);
                }
                else return Feedback.GenerateErrorFeedback(1, "The assembly Handle was 0.");
            }
            catch (Exception e)
            {
                return Feedback.GenerateErrorFeedback(2, e.Message, e);
            }
        }

        private static bool CheckForFile(string path) => File.Exists(path);

        private static void Assign(out string ns, out string cn, out string mn)
        {
            ns = "Injected";
            cn = "Loader";
            mn = "Init";
        }

        private static void AssignEject(out string ns, out string cn, out string mn)
        {
            ns = "Injected";
            cn = "Loader";
            mn = "Disable";
        }
    }
}
