using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace MazeGame
{
    public partial class MainForm : Form
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

        private int[,]? Maze { get; set; }
        private bool[,]? IsVisited { get; set; }

        private readonly Point[] directions;
        private UPanel? board;
        private UPanel? monster;

        // 몬스터 현재위치
        public Point current;
        private int[] playerPositions = [1, 1];

        // 벽사이즈
        private int wallSize;
        public int WallSize
        {
            get => wallSize;
            set
            {
                wallSize = value;
                var modCheck = mazeSize % WallSize;

                if (board != null)
                    board.Width = board.Height -= modCheck == 0 ? wallSize : wallSize * 2;

                current.X = wallSize;
                current.Y = wallSize;
                PanelSetPosition();
            }
        }

        private readonly Bitmap? bmp;
        private readonly int count;
        private readonly int mazeSize;
        private Random? random;

        // Controls
        private readonly UGroupBox box;
        private readonly ULabel labelScore;
        private readonly ULabel labelTimer;

        private List<int[]>? wayToGo;

        private int totalTime;
        private int maxTime = 60 * 3;
        private int maxScore = 15;

        private int score;

        public int Score
        {
            get => score;
            set
            {
                score = value;
                if (Score == maxScore && timer != null)
                {
                    timer.Stop();
                    labelScore.Text = "You win!!!";

                    foreach (var target in tartgets)
                    {
                        target.Value.Dispose();
                    }
                }

                else
                {
                    labelScore.Text = value.ToString();
                }
            }
        }

        private readonly System.Windows.Forms.Timer? timer;

        private Dictionary<int, PictureBox> tartgets = [];

        public MainForm()
        {
            //SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            InitializeComponent();

            #region MainForm
            Name = "MainForm";

            count = 40;
            mazeSize = 2000;

            Padding = new(0);
            Width = 1600;
            Height = 1200;

            StartPosition = FormStartPosition.CenterScreen;

            Controls.Add(box = new UGroupBox("\u2707", "Grp_Control"));

            box.Controls.AddRange(new[]
            {
                new UButton("\u2766", "Btn_PlayGame7", 9, DockStyle.Top, Button_Click, 24),
                new UButton("\u273C", "Btn_PlayGame6", 8, DockStyle.Top, Button_Click, 24),
                new UButton("\u2645", "Btn_PlayGame5", 7, DockStyle.Top, Button_Click, 24),
                new UButton("\u2B72", "Btn_PlayGame4", 6, DockStyle.Top, Button_Click, 24),
                new UButton("\u2B70", "Btn_PlayGame3", 5, DockStyle.Top, Button_Click, 24),
                new UButton("\u2B73", "Btn_PlayGame2", 4, DockStyle.Top, Button_Click, 24),
                new UButton("\u2B71", "Btn_PlayGame1", 3, DockStyle.Top, Button_Click, 24),
                new UButton("\u2BA8", "BtnPlayMaze", 2, DockStyle.Top, Button_Click, 24),
                new UButton("\u2740", "BtnDrawing", 1, DockStyle.Top, Button_Click, 24),
                new UButton("\u269D", "Btn_PlayMaze", 0, DockStyle.Top, Button_Click, 24),
            });

            Controls.Add(labelScore = new ULabel(string.Empty, "Lbl_Score", DockStyle.Top, Color.DarkOrange));

            Controls.Add(labelTimer = new ULabel(string.Empty, "Lbl_Timer", DockStyle.Bottom, Color.Gray));

            #endregion

            directions = [new Point(1, 0), new Point(0, 1), new Point(-1, 0), new Point(0, -1)];

            timer = new System.Windows.Forms.Timer { Interval = 1000 };

            EventsInit();
        }

        private void EventsInit()
        {
            ResizeEnd += (s, e) => board?.Invalidate();

            Load += (s, e) => WindowState = FormWindowState.Maximized;
            if (timer != null)
            {
                timer.Tick -= Timer_Tick;
                timer.Tick += Timer_Tick;
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (timer != null && --totalTime <= 0)
            {
                labelTimer.Text = "Game Over!";
                timer.Stop();
            }
            else
            {
                labelTimer.Text = $"{totalTime}";
            }
        }

        private async void Button_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btn) return;
            int tag = Convert.ToInt32(btn.Tag);

            switch (tag)
            {
                case 0:
                    {
                        totalTime = maxTime = 60;
                        maxScore = 15;
                        Score = 0;
                        Maze = null;
                        IsVisited = null;
                        Maze = new int[count, count];
                        IsVisited = new bool[count, count];

                        playerPositions = [1, 1];

                        tartgets?.Clear();
                        tartgets = [];

                        current = new Point(0, 0);
                        if (monster != null) monster?.Dispose();
                        if (board != null) board?.Dispose();

                        Controls.Add(board = new UPanel(Location = new Point(0, 0), Width = mazeSize, Height = mazeSize, Panel_Paint));
                        board.BackColor = Color.Transparent;
                        WallSize = Convert.ToInt32(Math.Floor(mazeSize / (decimal)count));

                        InitiallizeMaze();
                        timer?.Start();

                        await RunAsync();

                        // bmp = FormScreenShot(board);

                        GetTrace();

                        current.X = wallSize;
                        current.Y = wallSize;

                        board.Visible = true;

                        if (monster != null)
                        {
                            monster.Visible = true;
                            monster.BringToFront();
                        }
                    }
                    break;

                case 1:
                    {
                        var frm = new GraphicsForm
                        {
                            StartPosition = FormStartPosition.CenterScreen,
                            Width = 1024,
                            Height = 1024
                        };
                        frm.Show();
                    }
                    break;

                case 2: // Maze Play
                    {
                        var mz = new MazePlayForm();
                        mz.Show();
                    }
                    break;

                    case 3:
                    {
                        var shap = new ShapForm();
                        
                        shap.Show();
                    }
                    break;
            }
        }

        private void GetTrace()
        {
            if (Maze == null) return;

            wayToGo = null;
            wayToGo = [];

            for (int r = 0; r < count - 1; r++)
            {
                for (int c = 0; c < count - 1; c++)
                {
                    if (Maze[c, r] == 0)
                    {
                        wayToGo.Add([c, r]);
                    }
                    else
                    {
                        if (r == 0) continue;
                        if (c == 0) continue;
                        if (r == count - 2) continue;
                        if (c == count - 2) continue;

                        if (random != null && (r * c) % 5 == 0 && (r * c * random.Next(2, count - 2)) % random.Next(2, count - 2) == 0)
                        {
                            Maze[c, r] = 0;
                            wayToGo.Add([c, r]);
                        }
                    }
                }
            }

            for (int i = 0; i < 30; i++)
            {
                Goals();
            }
        }

        private void Goals()
        {
            if (random == null || tartgets == null || wayToGo == null || board == null) return;
            var index = 0;
            do
            {
                index = random.Next(2, wayToGo.Count);
            } while (tartgets.Any(x => x.Key == index));

            var goal = wayToGo[index];

            var p = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = wallSize,
                Height = wallSize
            };

            Controls.Add(p);
            p.BringToFront();

            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Female.png");
            var fi = new FileInfo(file);
            if (!fi.Exists) return;

            p.Image = Image.FromFile(fi.FullName);
            p.Location = new Point(board.Location.X + (wallSize * goal[0]), board.Location.Y + (wallSize * goal[1]));
            tartgets.Add(index, p);
        }

        public static Bitmap FormScreenShot(Panel board)
        {
            var bmp = new Bitmap(board.Width, board.Height, PixelFormat.Format32bppArgb);
            bmp.SetResolution(board.DeviceDpi, board.DeviceDpi);
            board.DrawToBitmap(bmp, new Rectangle(Point.Empty, board.Size));
            return bmp;
        }

        private void PanelSetPosition()
        {
            if (board == null) return;
            board.Left = (Width - board.Width) / 2;
            board.Top = (Height - board.Height) / 2;
            board.Invalidate(true);
        }

        private void InitiallizeMaze()
        {
            if (Maze == null || IsVisited == null) return;
            for (int col = 0; col < count; col++)
            {
                for (int row = 0; row < count; row++)
                {
                    IsVisited[col, row] = false;
                    Maze[col, row] = 1;
                }
            }
        }

        private void Panel_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            for (int col = 0; col < count - 1; col++)
            {
                for (int row = 0; row < count - 1; row++)
                {
                    if (Maze == null || Maze[col, row] != 1) continue;
                    Rectangle rectangle = new(WallSize * col, WallSize * row, WallSize, WallSize);
                    e.Graphics.FillRectangle(Brushes.DarkSlateGray, rectangle);
                }
            }
        }

        private void Monster_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "RedCar.png");
            var fi = new FileInfo(file);
            if (!fi.Exists) return;
            using Bitmap bmp = new(Image.FromFile(fi.FullName), new Size(wallSize, wallSize));
            Color c = bmp.GetPixel(0, 0);
            bmp.MakeTransparent(c);

            e.Graphics.DrawImage(bmp, 0, 0, bmp.Width, bmp.Height);
        }

        private List<T> ShuffleList<T>(List<T> dirs)
        {
            random = new(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < dirs.Count; i++)
            {
                int j = random.Next(i, dirs.Count);
                (dirs[j], dirs[i]) = (dirs[i], dirs[j]);
            }
            return dirs;
        }

        private bool ToBeOrNotToBeThatIsQnQ(int nextCol, int nextRow)
        {
            return
            (nextCol >= 0 && nextCol < count - 1) &&
            (nextRow >= 0 && nextRow < count - 1) &&
            (IsVisited != null && !IsVisited[nextCol, nextRow]);
        }

        private void GenerateMaze(int col, int row)
        {
            if (IsVisited == null || Maze == null) return;
            IsVisited[col, row] = true;
            Maze[col, row] = 0;
            var directions = ShuffleList(new List<int> { 0, 1, 2, 3 });
            foreach (int direction in directions)
            {
                int nextCol = col + this.directions[direction].X * 2;
                int nextRow = row + this.directions[direction].Y * 2;
                if (ToBeOrNotToBeThatIsQnQ(nextCol, nextRow))
                {
                    var dx = col + this.directions[direction].X;
                    var dy = row + this.directions[direction].Y;
                    Maze[dx, dy] = 0;
                    GenerateMaze(nextCol, nextRow);
                }
            }
        }

        private bool IsWay(int col, int row)
        {
            if (col < 0 || row < 0) return false;
            if (col > mazeSize || row > mazeSize) return false;
            if (Maze == null) return false;
            return Maze[col, row] == 0;
        }


        private void MakeMonster()
        {
            if (monster != null) Controls.Remove(monster);

            if (board == null) return;
            var location = new Point(board.Location.X + wallSize, board.Location.Y + wallSize);
            Controls.Add(monster = new UPanel(location, Width = wallSize, Height = wallSize, Monster_Paint));
            monster.BringToFront();
        }

        private void WayNumber(int col, int row)
        {
            if (wayToGo != null && wayToGo.Any(x => x[0] == col && x[1] == row))
            {
                var item = wayToGo.First(x => x[0] == col && x[1] == row);
                var idx = Array.IndexOf(wayToGo.ToArray(), item);

                if (tartgets.Any(x => x.Key == idx))
                {
                    PictureBox pb = tartgets.FirstOrDefault(x => x.Key == idx).Value;
                    tartgets.Remove(idx);

                    pb.Hide();
                    pb.Dispose();
                    Score++;
                }
            }
        }

        private async Task RunAsync()
        {
            await Task.Run(() => GenerateMaze(1, 1)).ContinueWith(t => Invoke(new Action(() => MakeMonster())));
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (monster == null) return base.ProcessCmdKey(ref msg, keyData);

            switch (keyData)
            {
                case Keys.A:
                case Keys.Left:
                    {
                        if (IsWay(playerPositions[0] -= 1, playerPositions[1]))
                            monster.Location = new Point(monster.Location.X - wallSize, monster.Location.Y);
                        else
                            playerPositions[0] += 1;

                        WayNumber(playerPositions[0], playerPositions[1]);
                        return true;
                    }
                case Keys.D:
                case Keys.Right:
                    {
                        if (IsWay(playerPositions[0] += 1, playerPositions[1]))
                            monster.Location = new Point(monster.Location.X + wallSize, monster.Location.Y);
                        else
                            playerPositions[0] -= 1;

                        WayNumber(playerPositions[0], playerPositions[1]);
                        return true;
                    }
                case Keys.W:
                case Keys.Up:
                    {
                        if (IsWay(playerPositions[0], playerPositions[1] -= 1))
                            monster.Location = new Point(monster.Location.X, monster.Location.Y - wallSize);
                        else
                            playerPositions[1] += 1;

                        WayNumber(playerPositions[0], playerPositions[1]);
                        return true;
                    }
                case Keys.S:
                case Keys.Down:
                    {
                        if (IsWay(playerPositions[0], playerPositions[1] += 1))
                            monster.Location = new Point(monster.Location.X, monster.Location.Y + wallSize);
                        else
                            playerPositions[1] -= 1;

                        WayNumber(playerPositions[0], playerPositions[1]);
                        return true;
                    }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    
    }
}
