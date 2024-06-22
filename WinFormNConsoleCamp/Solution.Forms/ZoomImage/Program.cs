using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ZoomImage
{

    internal static class Program
    {

        private enum PROCESS_DPI_AWARENESS
        {
            Process_DPI_Unaware = 0,
            Process_System_DPI_Aware = 1,
            Process_Per_Monitor_DPI_Aware = 2,
        }

        [DllImport("user32.dll", SetLastError =true)]
        private static extern bool SetProcessDPIAware();

        [DllImport("SHCore.dll")]
        private static extern bool SetProcessDpiAwareness(PROCESS_DPI_AWARENESS awareness);


        [STAThread]
        static void Main()
        {
            TryEnableDPIAware();
            // if (Environment.OSVersion.Version.Major >= 6) SetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static void TryEnableDPIAware()
        {
            try
            {
                SetProcessDpiAwareness(PROCESS_DPI_AWARENESS.Process_System_DPI_Aware);
            }
            catch
            {
                try
                {
                    SetProcessDPIAware();
                }
                catch { }
            }
        }
    }
}
