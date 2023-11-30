using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Com.ViVaKR.Forms
{
    public class VivForm : Form
    {
        public VivForm()
        {
            WindowState = FormWindowState.Maximized;
            TopLevel = true;

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            // BackColor = Color.Transparent;
            BackColor = Color.White;
            TransparencyKey = Color.White;
            // Other stuff
        }
    }
}
