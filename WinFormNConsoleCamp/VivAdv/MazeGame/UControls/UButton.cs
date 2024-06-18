using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace MazeGame.UControls
{
    public class UButton : Button
    {
        private EventHandler? handler;
        public event EventHandler? Handler
        {
            add
            {
                if (handler == null)
                {
                    handler += value;
                    Click += value;
                }
            }
            remove
            {
                handler -= value;
                Click -= value;
            }
        }
        private readonly int degree = 0;

        public UButton(string text, string name, int tag,
            DockStyle dockStyle, EventHandler handler, 
            int fontSize = 12, int degree = 0, bool enable = true)
        {
            Height = 100;
            Text = text;
            Name = name;

            Font = new Font("Fira Code Retina", fontSize, FontStyle.Bold);
            ForeColor = Color.Teal;
            Tag = tag;
            Dock = dockStyle;
            Handler += handler;
            this.degree = degree;
            Enabled = enable;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int mx = Size.Width / 2;
            int my = Size.Height / 2;
            SizeF size = e.Graphics.MeasureString(Text, Font);
            string temp = Text;
            Text = string.Empty;

            base.OnPaint(e);

            if (DesignMode) return;
            Text = temp;

            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            e.Graphics.TranslateTransform(mx, my);
            e.Graphics.RotateTransform(degree);
            e.Graphics.TranslateTransform(-mx, -my);
            e.Graphics.DrawString(Text, Font, Brushes.Orange, mx - (int)size.Width / 2, my - (int)size.Height / 2);
        }
    }
}
