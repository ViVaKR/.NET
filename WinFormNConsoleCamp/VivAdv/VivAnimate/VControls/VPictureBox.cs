namespace VivAnimate.VControls
{
    public class VPictureBox : PictureBox
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
        public VPictureBox(string image)
        {
            Image = Image.FromFile(image);
            BackColor = Color.FromArgb(0, 0, 0, 0);
            SizeMode = PictureBoxSizeMode.Zoom;

        }
    }
}
