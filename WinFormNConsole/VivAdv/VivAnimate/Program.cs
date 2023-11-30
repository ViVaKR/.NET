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
            Application.Run(new MainForm
            {
                Text = "Main",
                Width = 1600,
                Height = 1600,
                Font = new Font("IBM Plex Sans KR", 24, FontStyle.Regular),
                StartPosition = FormStartPosition.CenterScreen
            });
        }

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool SetProcessDPIAware();
    }
}