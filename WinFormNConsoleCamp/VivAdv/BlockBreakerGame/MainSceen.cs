
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockBreakerGame
{
    public partial class MainSceen : Form
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

        private UBall ball;
        private readonly int colums = 20;
        private readonly int rows = 3;
        private readonly Label label;
        private readonly Timer timer;
        private UPanel ground;
        private int brickWidth;
        private int brickHeight;
        private readonly int margin = 100;
        private float posX;
        private float posY;
        private float dX = (float)Math.PI;
        private float dY = (float)Math.E;
        private float speed = 3f;
        private readonly List<Brick> brickList = new List<Brick>();

        private int score = 0;
        public int Score
        {
            get => score;
            set
            {
                score = value;
                label.Text = Score.ToString();
            }
        }

        public MainSceen()
        {

            InitializeComponent();

            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            DoubleBuffered = true;
            Text = "Main Sceen";
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;

            timer = new Timer
            {
                Interval = 10
            };

            timer.Tick += Timer_Tick;

            Load += (s, e) =>
            {

                Controls.Add(ground = new UPanel());
                ground.BringToFront();
                brickWidth = (ground.ClientRectangle.Width - margin) / colums;
                brickHeight = 20;

                var r = ground.ClientRectangle;
                ground.Racket.Location = new Point((r.Width - ground.Racket.Width) / 2, Math.Abs(r.Y - r.Height) - 15);
            };

            Controls.Add(label = new Label
            {
                Dock = DockStyle.Top,
                AutoSize = false,
                Height = 50,

                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Teal,
                Font = new Font(Font.FontFamily, 24f, FontStyle.Bold)
            });

            Score = 0;

        }

        private void SetBrick()
        {
            int left = margin / 2;
            int top = brickHeight * 3;

            int i = 0;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < colums; col++)
                {
                    var brick = new Brick
                    {
                        Id = ++i,
                        Width = brickWidth - 4,
                        Height = brickHeight - 4,
                        BackColor = Color.SkyBlue,
                        Padding = new Padding(3),
                        Location = new Point(left + (col * brickWidth), top + (row * brickHeight)),
                        Rect = new Rectangle(left + (col * brickWidth), top + (row * brickHeight), brickWidth, brickHeight),
                        Health = 1
                    };

                    ground.Controls.Add(brick);
                    brick.BringToFront();
                    brickList.Add(brick);
                }
            }

            ground.BringToFront();
        }

        private async Task StartBall()
        {
            await Task.Run(() =>
            {
                Invoke(new MethodInvoker(() =>
                {

                    if (posX - ball.Width <= -ball.Width) dX = speed; // Left Wall
                    if (posX + ball.Width >= ClientRectangle.Right) dX = -speed; // Right Wall
                    posX += dX;

                    if (posY - ball.Height <= -ball.Height) dY = speed; // Top Wall

                    Rectangle r1 = new Rectangle(ball.Location.X, ball.Location.Y, ball.Width, ball.Height);
                    Rectangle r2 = new Rectangle(ground.Racket.Location.X, ground.Racket.Location.Y, ground.Racket.Width, ground.Racket.Height);

                    if (r1.IntersectsWith(r2)) dY = -speed;

                    if (brickList.Any(x => x.Rect.IntersectsWith(r1)))
                    {
                        dY = speed;
                        var item = brickList.FirstOrDefault(x => x.Rect.IntersectsWith(r1));
                        ground.Controls.Remove(item);
                        brickList.Remove(item);
                        Score++;
                    }

                    posY += dY;

                    ball.Location = new Point((int)posX, (int)posY);

                }));
            });


        }

        private void Ball_LocationChanged(object sender, EventArgs e)
        {
            if (!(sender is UBall ball)) return;
            if (ball.Location.Y > ground.Racket.Location.Y)
            {
                ball.Dispose();
                Controls.Remove(ball);
                timer.Stop();
                label.Text = $"You Failed!! {ball.Location.X} - {ball.Location.Y}";
            }

        }

        private async void Timer_Tick(object sender, EventArgs e)
            => await StartBall().ContinueWith(x => speed = (new Random().Next(3, 15)));

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int mv = ground.ClientRectangle.Width / 20;

            switch (keyData)
            {
                case Keys.J:
                case Keys.Right:
                    {
                        if (ground.Racket.Location.X >= ground.ClientRectangle.Width - ground.Racket.Width) return true;
                        ground.Racket.Location = new Point(ground.Racket.Location.X + mv, ground.Racket.Location.Y);

                    }
                    return true;

                case Keys.F:
                case Keys.Left:
                    {
                        if (ground.Racket.Location.X < 0) return true;
                        ground.Racket.Location = new Point(ground.Racket.Location.X - mv, ground.Racket.Location.Y);
                    }
                    return true;
                case Keys.S:
                    SetBrick();
                    break;
                case Keys.R:

                    ball = new UBall(ground);
                    ground.Controls.Add(ball);
                    posX = ground.Racket.Location.X + ground.Racket.Width / 2;
                    posY = ground.Racket.Top - ball.Height;
                    ball.Location = new Point((int)posX, (int)posY);
                    ball.LocationChanged += Ball_LocationChanged;
                    ball.BringToFront();
                    timer.Start();
                    return true;

                case Keys.Q:
                    timer.Stop();
                    Close();
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
