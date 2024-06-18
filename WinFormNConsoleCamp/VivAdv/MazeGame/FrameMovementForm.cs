
using System.Diagnostics;

namespace MazeGame
{
    public partial class FrameMovementForm : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;
                return handleParam;
            }
        }

        private readonly float speedXperSecond = 1000f / 30;
        private const int bar_width = 40;
        private float x_pos = 0;
        private TimeSpan _lastFrameTime = TimeSpan.Zero;
        private readonly Stopwatch _frameTimer = Stopwatch.StartNew();

        public FrameMovementForm()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            TimeSpan currentFramTime = _frameTimer.Elapsed;
            float distance = (float)(currentFramTime - _lastFrameTime).TotalSeconds * speedXperSecond;

            x_pos += distance;
            while (x_pos > Width) x_pos -= Width;

            e.Graphics.FillRectangle(Brushes.Coral, new RectangleF(x_pos, 0, bar_width, 500));
            _lastFrameTime = currentFramTime;
            Invalidate();
        }
    }
}
