using System.Drawing.Drawing2D;

namespace MazeGame
{
    public partial class MazePlayForm : Form
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

        public Maze? maze;

        private UPictureBox? Ground { get; set; }

        public UPictureBox? Player { get; set; }

        public UGroupBox? Box { get; set; }

        private ULabel? labelScore;

        private ULabel? labelMessage;

        private int maxScore = 15;

        private readonly int count = 30;

        private readonly int groundSize = 2000;

        private int wallSize = 0;

        private int score;

        public int Score
        {
            get => score;
            set
            {
                score = value;

                if (labelMessage != null && Score >= maxScore)
                {
                    labelMessage.Text = "You win!!!";

                    return;
                }

                if (labelScore != null)
                    labelScore.Text = value.ToString();
            }
        }

        private List<int[]>? wayToGo;

        private readonly List<PictureBox> monsters = [];

        private readonly Dictionary<int, PictureBox> tartgets = [];

        private readonly Random random = new(new Guid().GetHashCode());

        public MazePlayForm()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            InitializeComponent();

            WindowState = FormWindowState.Maximized;

            CreateControls();

            EventInit();
        }

        private void EventInit()
        {
            Resize += (s, e) => CenterOfForm();
        }

        public void CreateControls()
        {
            Controls.Add(Box = new UGroupBox(string.Empty, "Grp_Control"));

            Box.Controls.AddRange(new[]
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
            Controls.Add(labelMessage = new ULabel(string.Empty, "Lbl_Timer", DockStyle.Bottom, Color.Gray));
        }

        private async void Button_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btn) return;
            if (labelMessage == null) return;
            if (labelMessage == null) return;
            int tag = Convert.ToInt32(btn.Tag);

            switch (tag)
            {
                case 0:
                    {
                        Controls.Add(Ground = new UPictureBox(groundSize, Ground_Paint));
                        maze = new Maze(count, Ground.Height);
                        await maze.RunAsync().ContinueWith(x => wallSize = (int)maze.WallSize);

                        CenterOfForm();
                        await Task.Delay(10);

                        labelMessage.Text = $"미궁 완성, 플레이어를 생성하세요";
                    }
                    break;

                case 1:
                    {
                        Controls.Add(Player = new UPictureBox(wallSize, Player_Paint));
                        if (Ground == null) return;
                        Player.Location
                            = new Point(Ground.Location.X + wallSize, Ground.Location.Y + wallSize);

                        Player.BringToFront();
                        labelMessage.Text = $"플레이어 입장완성, 미궁을 조정하세요.";
                    }
                    break;

                case 2:
                    {
                        GetTrace();
                        labelMessage.Text = $"미궁 조정 완료, 몬스터를 활성화 하세요.";
                    }
                    break;

                case 3:
                    for (int i = 0; i < 29; i++)
                    {
                        Goals($"Ball_{i % 10}.png");
                    }
                    labelMessage.Text = $"몬스터가 등장하였습니다. 시작하세요!";
                    break;

                case 4:
                    Ground?.Dispose();
                    Player?.Dispose();

                    foreach (var item in monsters)
                        item.Dispose();
                    maxScore = 15;
                    Score = 0;
                    labelMessage.Text = "0";
                    maze = null;
                    break;
            }

        }

        private void GetTrace()
        {
            if (maze == null || maze.map == null) return;

            wayToGo = [];

            for (int r = 0; r < count - 1; r++)
            {
                for (int c = 0; c < count - 1; c++)
                {
                    if (maze.map[c, r] == 0) wayToGo.Add([c, r]);
                    else
                    {
                        if (r == 0) continue;
                        if (c == 0) continue;
                        if (r == count - 2) continue;
                        if (c == count - 2) continue;
                        if (random != null && (r * c) % 5 == 0 && (r * c * random.Next(2, count - 2)) % random.Next(2, count - 2) == 0)
                        {
                            maze.map[c, r] = 0;
                            wayToGo.Add([c, r]);
                        }
                    }
                }
            }
        }

        private void Goals(string monster)
        {
            if (random == null || tartgets == null || wayToGo == null || Ground == null) return;
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
            monsters.Add(p);

            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", monster);
            var fi = new FileInfo(file);
            if (!fi.Exists) return;

            p.Image = Image.FromFile(fi.FullName);
            p.Location = new Point(Ground.Location.X + (wallSize * goal[0]), Ground.Location.Y + (wallSize * goal[1]));
            tartgets.Add(index, p);
        }

        private void CenterOfForm()
        {
            if (Ground == null) return;
            Ground.Left = (Width - Ground.Width) / 2;
            Ground.Top = (Height - Ground.Height) / 2;
            Ground.Invalidate(true);
        }

        private void Player_Paint(object? sender, PaintEventArgs e)
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

        private void Ground_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            for (int col = 0; col < count; col++)
            {
                for (int row = 0; row < count; row++)
                {
                    if (maze.map[col, row] != 1) continue;
                    Rectangle rectangle
                        = new((int)maze.WallSize * col, (int)maze.WallSize * row, (int)maze.WallSize, (int)maze.WallSize);
                    e.Graphics.FillRectangle(Brushes.DarkSlateGray, rectangle);
                }
            }
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

        private bool IsWay(int col, int row)
        {
            if (col < 0 || row < 0) return false;
            if (col > groundSize || row > groundSize) return false;
            if (maze == null) return false;
            return maze.map[col, row] == 0;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (Player == null || maze == null) return base.ProcessCmdKey(ref msg, keyData);

            switch (keyData)
            {
                case Keys.A:
                case Keys.Left:
                    {
                        if (IsWay(maze.playerPositions[0] -= 1, maze.playerPositions[1]))
                            Player.Location = new Point(Player.Location.X - wallSize, Player.Location.Y);
                        else
                            maze.playerPositions[0] += 1;

                        WayNumber(maze.playerPositions[0], maze.playerPositions[1]);
                        return true;
                    }
                case Keys.D:
                case Keys.Right:
                    {
                        if (IsWay(maze.playerPositions[0] += 1, maze.playerPositions[1]))
                            Player.Location = new Point(Player.Location.X + wallSize, Player.Location.Y);
                        else
                            maze.playerPositions[0] -= 1;

                        WayNumber(maze.playerPositions[0], maze.playerPositions[1]);
                        return true;
                    }
                case Keys.W:
                case Keys.Up:
                    {
                        if (IsWay(maze.playerPositions[0], maze.playerPositions[1] -= 1))
                            Player.Location = new Point(Player.Location.X, Player.Location.Y - wallSize);
                        else
                            maze.playerPositions[1] += 1;

                        WayNumber(maze.playerPositions[0], maze.playerPositions[1]);
                        return true;
                    }
                case Keys.S:
                case Keys.Down:
                    {
                        if (IsWay(maze.playerPositions[0], maze.playerPositions[1] += 1))
                            Player.Location = new Point(Player.Location.X, Player.Location.Y + wallSize);
                        else
                            maze.playerPositions[1] -= 1;

                        WayNumber(maze.playerPositions[0], maze.playerPositions[1]);
                        return true;
                    }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
