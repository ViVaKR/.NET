using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenWinApp
{
    public class OpenApp
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string? lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        private const int WM_KEYDOWN = 0x100;
        private const int VK_CONTROL = 0x11;
        private const int VK_RETURN = 0x0D;
        private const int EM_LINESCROLL = 0xB6;
        public static void Run()
        {
            var demoText = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "demo.txt");
            int lineNumber = 20; // 이동하고자 하는 줄 번호

            ProcessStartInfo psi = new()
            {
                FileName = "notepad.exe",
                Arguments = demoText,
                UseShellExecute = true
            };

            Process? notepadProcess = Process.Start(psi);

            if (notepadProcess == null) return;

            IntPtr notepadHandle = notepadProcess.MainWindowHandle; //FindWindow(null, "제목 없음 - 메모장"); // 메모장 창 핸들 찾기

            SetForegroundWindow(notepadHandle); // 메모장 창을 활성화

            // Ctrl + G 를 눌러 이동 다이얼로그를 열기
            int a = SendMessage(notepadHandle, WM_KEYDOWN, VK_CONTROL, IntPtr.Zero);
            int b = SendMessage(notepadHandle, WM_KEYDOWN, 'G', IntPtr.Zero);
            int c = SendMessage(notepadHandle, WM_KEYDOWN, VK_CONTROL, IntPtr.Zero);

            // 특정 줄로 이동
            for (int i = 1; i < lineNumber; i++)
            {
                int temp = SendMessage(notepadHandle, EM_LINESCROLL, IntPtr.Zero, i);
                Debug.WriteLine($"{a} {b} {c} {temp} ");
            }

            // Enter 키를 눌러 이동
            int f = SendMessage(notepadHandle, WM_KEYDOWN, VK_RETURN, IntPtr.Zero);
            Debug.WriteLine(f);

        }
    }
}
