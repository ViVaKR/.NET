namespace MazeGame
{
    public class ULabel : Label
    {
        public ULabel(string text, string name, DockStyle dockStyle, Color color)
        {
            Text = text;
            Name = name;
            TextAlign = ContentAlignment.MiddleCenter;
            Dock = dockStyle;
            AutoSize = false;
            Height = 100;
            ForeColor = color;
            BackColor = Color.Wheat;
            Font = new Font(Font.FontFamily, 24, FontStyle.Bold);
        }
    }
}
