
using System.Drawing;
using System.Windows.Forms;

namespace ControlAnimateDemo
{
    public class VivLabel : Label
    {
        public VivLabel(int width, int height, Color color)
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            Width = width;
            Height = height;
            BackColor = color;
        }
    }
}
