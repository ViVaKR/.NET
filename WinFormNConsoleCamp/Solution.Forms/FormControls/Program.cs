using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FormControls
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            //if (Environment.OSVersion.Version.Major >= 6) 
            //    SetProcessDPIAware();

            TryDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private enum PROCESS_DPI_AWARENESS
        {
            Process_DPI_Unaware = 0,
            Process_System_DPI_Aware = 1,
            Process_Per_Monitor_DPI_Aware = 2
        }

        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        [DllImport("SHCore.dll")]
        private static extern bool SetProcessDpiAwareness(PROCESS_DPI_AWARENESS awareness);

       
        private static void TryDPIAware()
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
                catch
                {
                }
            }
        }
    }
}
