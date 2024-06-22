using System.Drawing;
using System.Windows.Forms;

namespace PoopGame.UControls
{
    public class UPoop : PictureBox
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;
                return handleParam;
            }
        }
        public UPoop(Image image)
        {
            DoubleBuffered = true;
            Image = image;
            BackColor = Color.Transparent;

            Size = new Size(image.Size.Width / 10, image.Size.Height / 10);

            SizeMode = PictureBoxSizeMode.Zoom;
        }
    }
}
