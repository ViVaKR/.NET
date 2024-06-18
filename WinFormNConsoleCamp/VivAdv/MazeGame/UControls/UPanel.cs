namespace MazeGame.UControls
{
    public class UPanel : Panel
    {
        public UPanel(Point location, int width, int height, PaintEventHandler handler)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            Location = location;
            Width = width;
            Height = height;
            Visible = false;
            BackColor = Color.Transparent;
            Paint -= handler;
            Paint += handler;
        }



    }
}
