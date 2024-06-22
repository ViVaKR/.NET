
using System.Drawing;
using System.Windows.Forms;

namespace Com.ViVaKR.Forms
{
    public class VivPanel : Panel
    {
        public VivPanel(DockStyle dockStyle, object tag, Color backColor, int width, int height)
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            Dock = dockStyle;
            BackColor = backColor;
            Tag = tag;
            if(dockStyle != DockStyle.Fill)
            {
                Width = width;
                Height = height;
            }

            Padding = new Padding(5);
        }
    }
}
