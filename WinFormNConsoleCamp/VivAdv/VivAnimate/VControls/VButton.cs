namespace VivAnimate.VControls
{
    public class VButton : Button
    {
        public VButton(string text, string name, int width, DockStyle dockStyle, int tag)
        {
            Font = new Font("FiraCode Nerd Font", 12f, FontStyle.Regular);
            Text = text;
            Name = name;
            Width = width;
            ForeColor = Color.DarkMagenta;
            Dock = dockStyle;
            Tag = tag;
        }
    }
}
