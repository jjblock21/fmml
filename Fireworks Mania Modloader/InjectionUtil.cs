using System;
using System.Diagnostics;
using System.IO;
using SharpMonoInjector;

namespace Fireworks_Mania_Modloader
{
	public static class InjectionUtil
	{
		public static bool Inject(Process process, string path, out IntPtr value)
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
					return true;
				else return false;
			}
			catch
            {
				value = IntPtr.Zero;
				return false;
            }
		}

		public static bool Eject(Process process, IntPtr assembly)
		{
			try
			{
				if (assembly != IntPtr.Zero)
				{
					AssignEject(out string ns, out string cn, out string mn);
					Injector injector = new Injector(process.Id);
					injector.Eject(assembly, ns, cn, mn);
					return true;
				}
				else return false;
			}
			catch
            {
				return false;
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
