using System.Runtime.InteropServices;

namespace VivAnimate
{
    internal static partial class Program
    {
        [STAThread]
        static void Main()
        {
            if (Environment.OSVersion.Version.Major >= 6) SetProcessDPIAware();
            ApplicationConfiguration.Initialize();
            Application.Run(new AvoidPooPGame
            {
                WindowState = FormWindowState.Maximized
            });
        }

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool SetProcessDPIAware();
    }
}