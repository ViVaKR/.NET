namespace VivAnimate.VControls
{
    public class VLabel : Label
    {
        public VLabel(Size size, Color backColor)
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            AutoSize = false;
            Size = size;
            BackColor = backColor;
        }
    }
}
