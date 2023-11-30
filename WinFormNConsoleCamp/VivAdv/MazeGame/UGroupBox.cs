
namespace MazeGame
{
    public class UGroupBox : GroupBox
    {
        public UGroupBox(string text, string name)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            Width = 150;
            Text = text;
            Name = name;
            ForeColor = Color.DeepPink;
            Font = new Font(Font.FontFamily, 12, FontStyle.Regular);
            Dock = DockStyle.Right;
        }
    }
}
