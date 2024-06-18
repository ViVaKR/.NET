
using System.Windows.Forms;

namespace VivCam
{
    public class UVideo : PictureBox
    {
        public UVideo()
        {
            DoubleBuffered = true;
            Dock = DockStyle.Fill;
        }
    }
}
