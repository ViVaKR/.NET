namespace VivAnimate.VControls
{
    public class VButton : Button
    {
        public VButton(string text, string name, int height, DockStyle dockStyle, int tag)
        {
            Font = new Font("FiraCode Nerd Font", 18, FontStyle.Regular);
            Text = text;
            Name = name;
            Height = height;
            ForeColor = Color.DarkMagenta;
            Dock = dockStyle;
            Tag = tag;
        }
    }
}
