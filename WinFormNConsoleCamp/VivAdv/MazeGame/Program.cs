namespace MazeGame
{
    internal static partial class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Environment.OSVersion.Version.Major >= 6) SetProcessDPIAware();

            ApplicationConfiguration.Initialize();
            // Application.Run(new MainForm());
            // Application.Run(new MazePlayForm());
            Application.Run(new MovementForm());
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
