using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceShooter.UControls
{
    public class UPicture : PictureBox
    {
        public UPicture()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
        }
    }
}
