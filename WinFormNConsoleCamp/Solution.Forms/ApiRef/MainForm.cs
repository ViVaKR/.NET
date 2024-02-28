using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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
