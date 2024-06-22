using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ApiRef
{
    public class MyCalendar : MonthCalendar
    {
        public MyCalendar()
        {

            var current = DateTime.Now;

            // Font Size 로 카렌더 크기 재 조정됨
            Font = new Font(Font.FontFamily, 48f, FontStyle.Regular);
            MinDate = new DateTime(current.Year, current.Month, 1);
            MaxDate = new DateTime(current.Year, current.Month, DateTime.DaysInMonth(current.Year, current.Month) - 1);

        }



        #region API uxtheme
        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);
        protected override void OnHandleCreated(EventArgs e)
        {
            SetWindowTheme(Handle, string.Empty, string.Empty);
            Top = 50;
            base.OnHandleCreated(e);
        }
        #endregion
    }
}
