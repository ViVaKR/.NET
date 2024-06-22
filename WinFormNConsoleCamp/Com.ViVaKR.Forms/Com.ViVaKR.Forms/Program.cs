using System;
using System.Windows.Forms;

namespace Com.ViVaKR.Forms
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DrawForm()
            {
                Width = 1200,
                Height = 1024,
                StartPosition = FormStartPosition.CenterScreen
            });
        }
    }
}
