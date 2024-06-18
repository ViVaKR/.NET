

namespace VivAnimate.VControls
{
    public class VPanel : Panel
    {
        public VPanel(Color backcolor, params PictureBox[] images)
        {
            DoubleBuffered = true;
            BackColor = backcolor;
            Controls.AddRange(images);
        }
    }
}
