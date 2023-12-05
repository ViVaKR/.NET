namespace MazeGame.UControls
{
    public class UGroupBox : GroupBox
    {
        public UGroupBox(string text, string name)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            Width = 300;
            Text = text;
            Name = name;

            Font = new Font(Font.FontFamily, 12, FontStyle.Regular);
            ForeColor = Color.Teal;
            Dock = DockStyle.Right;
        }
    }
}
