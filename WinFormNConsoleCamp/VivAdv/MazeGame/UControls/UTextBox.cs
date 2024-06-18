namespace MazeGame.UControls
{
    public class UTextBox : TextBox
    {
        public UTextBox(string text, string name)
        {
            Text = text;
            Name = name;
            Dock = DockStyle.Bottom;
            Height = 150;
            BackColor = Color.Beige;
            Font = new Font(Font.FontFamily, 14, FontStyle.Regular);
            TextAlign = HorizontalAlignment.Center;
            ForeColor = Color.Chocolate;
            Multiline = true;
        }
    }
}
