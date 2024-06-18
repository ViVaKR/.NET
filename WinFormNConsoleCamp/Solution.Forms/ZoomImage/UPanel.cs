
using System.Windows.Forms;

namespace ZoomImage
{
    public class UPanel : Panel
    {
        public UPanel()
        {
            DoubleBuffered = true;

            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |ControlStyles.AllPaintingInWmPaint, true);

            UpdateStyles();
        }


    }
}
