
using System.Diagnostics;
using System.Drawing.Drawing2D;


namespace MazeGame.UControls
{
    internal class UvPanel : Panel
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }

        private float speedXperSecond;
        private float speedYperSecond;
        private float posX = 0;
        private float posY = 0;
        private TimeSpan lastFrameTime = TimeSpan.Zero;
        private readonly Stopwatch frameTimer = Stopwatch.StartNew();

        public UvPanel()
        {
            Dock = DockStyle.Fill;
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }

        private readonly int radius = 50;
        private float distanceX;
        private float distanceY;

        private float dX = 1;
        private float dY = 1;

        private float speed = (float)(30f / Math.E);
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            TimeSpan currentFrameTime = frameTimer.Elapsed;
            speedXperSecond = 1000f / 30f;
            speedYperSecond = 1000f / 30f;
            distanceX = (float)(currentFrameTime - lastFrameTime).TotalSeconds * speedXperSecond;
            distanceY = (float)(currentFrameTime - lastFrameTime).TotalSeconds * speedYperSecond;

            if (posX - radius <= -radius) dX = speed;
            if (posX + radius >= ClientRectangle.Right) dX = -speed;
            posX += dX * distanceX;

            if (posY - radius <= -radius) dY = speed;
            if (posY + radius >= ClientRectangle.Bottom) dY = -speed;
            posY += dY * distanceY;

            lastFrameTime = currentFrameTime;
            Rectangle targetRect = ClientRectangle;
            BufferedGraphicsContext ctx = new();
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            BufferedGraphics bg = ctx.Allocate(e.Graphics, targetRect);
            bg.Graphics.Clear(BackColor);
            bg.Graphics.FillEllipse(Brushes.Magenta, posX, posY, radius, radius);
            Invalidate();

            bg.Render(e.Graphics);
            bg.Dispose();
            ctx.Dispose();
        }
    }
}
