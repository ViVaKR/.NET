using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SpaceShooter
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Debug.WriteLine(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion);

            //// 윈도우 폴더의 만만한 노트패드 추출
            //var exeFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "notepad.exe");

            //// 노트패드 실행 파일 모든 정보
            //FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(exeFilePath);
            //Debug.WriteLine(versionInfo.ProductVersion); // 문의에 대한 답변
            //Debug.WriteLine("=="); // 절취선
            //Debug.WriteLine(versionInfo); // 추가 모든 정보들

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainSceen());
        }
    }
}
