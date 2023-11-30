
namespace MazeGame
{
    public class UPictureBox : PictureBox
    {
        public UPictureBox(int rectSize, PaintEventHandler handler)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            Width = Height = rectSize;
            BackColor = Color.Transparent;
            Paint += handler;
        }
    }
}
