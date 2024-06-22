using System.Windows.Forms;

namespace ApiRef
{
    public partial class MainForm : Form
    {

        private Panel panel;
        private MyCalendar calendar;

        public MainForm()
        {

            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Font;

            WindowState = FormWindowState.Maximized;
            StartPosition = FormStartPosition.CenterScreen;

            Load += (s, e) =>
            {
                Controls.Add(calendar = new MyCalendar());
                calendar.Left = (Width - calendar.Width) / 2;
            };

        }
    }
}
