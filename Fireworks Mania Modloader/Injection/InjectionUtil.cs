using System;
using System.Diagnostics;
using System.IO;
using SharpMonoInjector;

namespace Fireworks_Mania_Modloader
{
    public static class InjectionUtil
    {
        public const string CLASS = "Injected.Loader";
        public const string INJECT_METHOD = "Init";
        public const string EJECT_METHOD = "Disable";

        public static Feedback Inject(Process process, string path, out IntPtr value)
        {
            try
            {
                value = IntPtr.Zero;
                if (CheckForFile(path) && process.Id != new int())
                {
                    Injector injector = new Injector(process.Id);
                    value = injector.Inject(
                        File.ReadAllBytes(path),
                        GetNamespace(),
                        GetClassName(),
                        INJECT_METHOD
                    );
                }
                if (value != IntPtr.Zero || value != new IntPtr())
                {
                    return Feedback.GenerateSuccessFeedback(0);
                }
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
                    Injector injector = new Injector(process.Id);
                    injector.Eject(
                        assembly,
                        GetNamespace(),
                        GetClassName(),
                        EJECT_METHOD
                    );
                    return Feedback.GenerateSuccessFeedback(0);
                }
                else return Feedback.GenerateErrorFeedback(1, "The assembly Handle was 0.");
            }
            catch (Exception e)
            {
                return Feedback.GenerateErrorFeedback(2, e.Message, e);
            }
        }

        private static bool CheckForFile(string path)
        {
            return File.Exists(path);
        }

        private static string GetNamespace()
        {
            return CLASS.Split('.')[0];
        }

        private static string GetClassName()
        {
            return CLASS.Split('.')[1];
        }
    }
}
