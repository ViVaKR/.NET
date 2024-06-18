
using System.Diagnostics;
using System.Drawing.Drawing2D;
using MazeGame.UControls;

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
        private int maxScore = 100;
        private readonly int count = 30;
        private readonly int groundSize = 2000;
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

        private bool isStopped;
        public bool IsStopped
        {
            get => isStopped;
            set
            {
                isStopped = value;

                if (IsStopped)
                    Invoke(new Action(async () => await RepeatSearch()));
            }
        }

        private List<int[]>? WayToGo { get; set; }
        private List<PictureBox> monsters = [];
        private Dictionary<int, PictureBox> targets = [];
        private Dictionary<int[,], Rectangle> rc = [];
        private bool[,]? visited;
        private System.Windows.Forms.Timer? timer;
        private int counter;
        private List<History>? histories;

        public MazePlayForm()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint
                | ControlStyles.UserPaint
                | ControlStyles.DoubleBuffer, true);

            InitializeComponent();

            WindowState = FormWindowState.Maximized;

            CreateControls();

            EventInit();
        }

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        private void EventInit()
        {
            Resize += (s, e) => CenterOfForm();
        }

        /// <summary>
        /// 폼 내부 컨트롤 런타임에 작성하기
        /// </summary>
        public void CreateControls()
        {
            Controls.Add(Box = new UGroupBox(string.Empty, "Grp_Control"));

            Box.Controls.AddRange(new[] // 버튼 만들기
            {
                new UButton("Back", "Btn_PlayGame10", 10, DockStyle.Top, Button_Click),
                new UButton("Close", "Btn_PlayGame7", 9, DockStyle.Top, Button_Click),
                new UButton("Draw Path", "Btn_PlayGame6", 8, DockStyle.Top, Button_Click),
                new UButton("-", "Btn_PlayGame5", 7, DockStyle.Top, Button_Click),
                new UButton("Stop", "Btn_PlayGame4", 6, DockStyle.Top, Button_Click),
                new UButton("-", "Btn_PlayGame3", 5, DockStyle.Top, Button_Click),
                new UButton("Clear", "Btn_PlayGame2", 4, DockStyle.Top, Button_Click),
                new UButton("Monsters", "Btn_PlayGame1", 3, DockStyle.Top, Button_Click),
                new UButton("Start", "Btn_Start", 2, DockStyle.Top, Button_Click),
                new UButton("PLAYER", "BtnDrawing", 1, DockStyle.Top, Button_Click),
                new UButton("MAP", "Btn_PlayMaze", 0, DockStyle.Top, Button_Click),
            });

            Controls.Add(labelScore = new ULabel(string.Empty, "Lbl_Score", DockStyle.Top, Color.DarkOrange));
            Controls.Add(labelMessage = new ULabel(string.Empty, "Lbl_Timer", DockStyle.Bottom, Color.Gray));
        }


        private async Task RepeatSearch()
        {
            if (Player == null || maze == null || timer == null || histories == null) return;

            await Task.Run(() =>
            {
                foreach (var item in histories.OrderByDescending(x => x.Id))
                {
                    timer.Stop();
                    var intersection = BackTrace(item);

                    if (intersection)
                    {
                        maze.playerPositions[0] = item.Col;
                        maze.playerPositions[1] = item.Row;

                        Invoke(new MethodInvoker(() =>
                        {
                            Player.Location = item.Point;
                            timer.Start();
                        }));
                        return;
                    }
                }
            });

        }

        private void RemoveMap()
        {
            isStopped = true;
            isPath = false;
            WayToGo?.Clear();
            WayToGo = [];
            Ground?.Dispose();
            Ground = null;
            Player?.Dispose();
            Player = null;

            monsters.Clear();
            monsters = [];
            targets?.Clear();
            targets = [];
            rc.Clear();
            rc = [];
            visited = new bool[count, count];
            histories?.Clear();
            histories = [];
            foreach (var item in monsters) item.Dispose();
            maxScore = 15;
            Score = 0;
            if (labelMessage != null) labelMessage.Text = "0";
            maze = null;
        }

        /// <summary>
        /// 길 목록 작성
        /// </summary>
        private void GetTrace()
        {
            if (maze == null || maze.map == null) return;

            WayToGo = [];

            for (int r = 0; r < count - 1; r++)
            {
                for (int c = 0; c < count - 1; c++)
                {
                    if (maze.map[c, r] == 0) WayToGo.Add([c, r]);
                    else
                    {
                        if (r == 0) continue;
                        if (c == 0) continue;
                        if (r == count - 2) continue;
                        if (c == count - 2) continue;
                    }
                }
            }

        }

        /// <summary>
        /// 다중 목표물 설정
        /// </summary>
        /// <param name="monster"></param>
        private void Goals(string monster)
        {
            Random random = new();
            if (maze == null || targets == null || WayToGo == null || Ground == null)
                return;

            int index = 0;

            do
            {
                index = random.Next(2, WayToGo.Count);

            } while (targets.Any(x => x.Key == index));

            int[] goal = WayToGo[index];

            PictureBox p = new()
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = Convert.ToInt32(maze.WallSize),
                Height = Convert.ToInt32(maze.WallSize)
            };

            Controls.Add(p);
            p.BringToFront();
            monsters.Add(p);

            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", monster);

            var fi = new FileInfo(file);

            if (!fi.Exists) return;

            p.Image = Image.FromFile(fi.FullName);

            p.Location = new Point(
                Ground.Location.X + (Convert.ToInt32(maze.WallSize) * goal[0]),
                Ground.Location.Y + (Convert.ToInt32(maze.WallSize) * goal[1]));

            targets.Add(index, p);
        }

        /// <summary>
        /// 목표물 만들기
        /// </summary>
        private void FinalDestination()
        {
            if (maze == null || targets == null || WayToGo == null || Ground == null) return;

            int[]? lastPostion = WayToGo.LastOrDefault();

            if (lastPostion == null) return;


            var idx = Array.IndexOf(WayToGo.ToArray(), lastPostion);
            var goal = WayToGo[idx];
            var p = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = Convert.ToInt32(maze.WallSize),
                Height = Convert.ToInt32(maze.WallSize)
            };

            Controls.Add(p);
            p.BringToFront();
            monsters.Add(p);

            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Ball_0.png");
            var fi = new FileInfo(file);
            if (!fi.Exists) return;

            p.Image = Image.FromFile(fi.FullName);
            p.Location = new Point(Ground.Location.X + (Convert.ToInt32(maze.WallSize) * goal[0]), Ground.Location.Y + (Convert.ToInt32(maze.WallSize) * goal[1]));
            targets.Add(idx, p);

        }

        /// <summary>
        /// 미로 폼 중앙에 맞추기
        /// </summary>
        private void CenterOfForm()
        {
            if (Ground == null) return;
            Ground.Left = (Width - Ground.Width) / 2;
            Ground.Top = (Height - Ground.Height) / 2;
            Ground.Invalidate(true);
        }

        /// <summary>
        /// 플레이어 그리기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Player_Paint(object? sender, PaintEventArgs e)
        {
            if (maze == null) return;

            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Avata.png");
            var fi = new FileInfo(file);
            if (!fi.Exists) return;
            using Bitmap bmp = new(Image.FromFile(fi.FullName), new Size(Convert.ToInt32(maze.WallSize), Convert.ToInt32(maze.WallSize)));
            e.Graphics.DrawImage(bmp, 0, 0, bmp.Width, bmp.Height);

        }


        private bool isPath = false;
        /// <summary>
        /// 미로 그리기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ground_Paint(object? sender, PaintEventArgs e)
        {
            if (maze == null || Ground == null) return;

            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            rc.Clear();

            for (int col = 0; col < count; col++)
            {
                for (int row = 0; row < count; row++)
                {
                    if (maze.map[col, row] == 0)
                    {
                        if ((col > 0 && col < count - 2) && (row > 0 && row < count - 2))
                        {
                            var point = new Point(Ground.Location.X + ((int)maze.WallSize * col), Ground.Location.Y + ((int)maze.WallSize * row));
                            var size = new Size((int)maze.WallSize, (int)maze.WallSize);
                            var rect = new Rectangle(point, size);
                            var temp = new int[col, row];
                            if (!rc.TryAdd(temp, rect)) rc[temp] = rect;
                        }
                    }
                    else
                    {
                        var point = new Point((int)maze.WallSize * col, (int)maze.WallSize * row);
                        var size = new Size((int)maze.WallSize, (int)maze.WallSize);
                        var rect = new Rectangle(point, size);
                        e.Graphics.FillRectangle(Brushes.DarkSlateGray, rect);
                    }
                }
            }

            if (isPath)
                DrawPath(e.Graphics);

        }

        private void IsReachedTarget(int col, int row)
        {
            if (timer == null) return;

            if (WayToGo != null && WayToGo.Any(x => x[0] == col && x[1] == row))
            {
                var item = WayToGo.First(x => x[0] == col && x[1] == row);
                var idx = WayToGo.IndexOf(item);

                if (targets.Any(x => x.Key == idx))
                {
                    timer.Stop();
                    PictureBox target = targets.FirstOrDefault(x => x.Key == idx).Value;
                    targets.Remove(idx);

                    timer = null;
                    isStopped = true;
                    if (InvokeRequired)
                    {
                        BeginInvoke(new MethodInvoker(() =>
                        {
                            target.Hide();
                            target.Dispose();
                            Score++;
                            if (labelMessage != null)
                                labelMessage.Text = $"출구 도착";
                        }));
                        return;
                    }
                    target.Hide();
                    target.Dispose();
                    Score++;

                    if (labelMessage != null)
                        labelMessage.Text = $"출구 도착";
                }
            }
        }

        /// <summary>
        /// 플레이어 무빙
        /// </summary>
        /// <param name="next"></param>
        private void MovePlayer(Point next)
        {
            if (Player == null || labelScore == null || Ground == null) return;

            IsStopped = false;

            if (InvokeRequired)
                Invoke(new MethodInvoker(() => Player.Location = next));
            else
                Player.Location = next;

            // Histories.Add(Player.Location);
        }

        private async void Timer_Tick(object? sender, EventArgs e) => await RunAiAsync();

        private bool IsWay(int col, int row)
        {
            if (maze == null || visited == null || col < 0 || row < 0 || col > groundSize || row > groundSize)
                return false;

            bool check1 = maze.map[col, row] == 0;
            bool check2 = !visited[col, row];
            return check1 && check2;
        }

        private bool BackTrace(History item)
        {
            int c = item.Col;
            int r = item.Row;

            if (IsWay(c - 1, r)) return true;
            if (IsWay(c + 1, r)) return true;
            if (IsWay(c, r - 1)) return true;
            if (IsWay(c, r + 1)) return true;
            return false;
        }


        private void DrawPath(Graphics g)
        {
            if (maze == null || Ground == null || histories == null) return;
            List<Point> points = histories.DistinctBy(x => x.Point).Select(s => s.Point).ToList();
            foreach (var point in points)
            {
                var w = (int)maze.WallSize / 2;
                var h = (int)maze.WallSize / 2;
                var x = (point.X - Ground.Left) + (int)maze.WallSize / 4;
                var y = (point.Y - Ground.Top) + (int)maze.WallSize / 4;
                g.FillRectangle(Brushes.Magenta, x, y, w, h);
            }

        }

        private async Task RunAiAsync()
        {
            if (maze == null || Player == null || visited == null || histories == null) return;

            await Task.Run(() =>
            {
                counter = 0;
                { // 하
                    int col = maze.playerPositions[0];
                    int row = maze.playerPositions[1] += 1;
                    Point next = new(Player.Location.X, Player.Location.Y + Convert.ToInt32(maze.WallSize));

                    if (IsWay(col, row))
                    {
                        MovePlayer(next);
                        visited[col, row] = true;
                        histories.Add(new History(histories.Count + 1, next, col, row));
                        counter++;
                        return;
                    }
                    else
                        maze.playerPositions[1] -= 1;


                    IsReachedTarget(maze.playerPositions[0], maze.playerPositions[1]);
                }
                { // 우
                    int col = maze.playerPositions[0] += 1;
                    int row = maze.playerPositions[1];
                    Point next = new(Player.Location.X + Convert.ToInt32(maze.WallSize), Player.Location.Y);

                    if (IsWay(col, row))
                    {
                        MovePlayer(next);
                        visited[col, row] = true;
                        histories.Add(new History(histories.Count + 1, next, col, row));
                        counter++;
                        return;
                    }
                    else
                        maze.playerPositions[0] -= 1;

                    IsReachedTarget(maze.playerPositions[0], maze.playerPositions[1]);
                }
                { // 좌
                    int col = maze.playerPositions[0] -= 1;
                    int row = maze.playerPositions[1];
                    Point next = new(Player.Location.X - Convert.ToInt32(maze.WallSize), Player.Location.Y);

                    if (IsWay(col, row))
                    {
                        MovePlayer(next);
                        visited[col, row] = true;
                        histories.Add(new History(histories.Count + 1, next, col, row));
                        counter++;
                        return;
                    }
                    else
                        maze.playerPositions[0] += 1;

                    IsReachedTarget(maze.playerPositions[0], maze.playerPositions[1]);
                }

                { // 상
                    int col = maze.playerPositions[0];
                    int row = maze.playerPositions[1] -= 1;
                    Point next = new(Player.Location.X, Player.Location.Y - Convert.ToInt32(maze.WallSize));

                    if (IsWay(col, row))
                    {
                        MovePlayer(next);
                        visited[col, row] = true;
                        histories.Add(new History(histories.Count + 1, next, col, row));
                        counter++;
                        return;
                    }
                    else
                        maze.playerPositions[1] += 1;

                    IsReachedTarget(maze.playerPositions[0], maze.playerPositions[1]);
                }

                IsStopped = counter == 0;
            });
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (maze == null || Player == null || visited == null || histories == null)
                return base.ProcessCmdKey(ref msg, keyData);

            switch (keyData)
            {
                case Keys.A:
                case Keys.Left: // 좌
                    {
                        int col = maze.playerPositions[0] -= 1;
                        int row = maze.playerPositions[1];
                        Point next = new(Player.Location.X - Convert.ToInt32(maze.WallSize), Player.Location.Y);

                        if (IsWay(col, row))
                        {
                            MovePlayer(next);
                            visited[col, row] = true;
                            histories.Add(new History(histories.Count + 1, next, col, row));
                        }
                        else
                            maze.playerPositions[0] += 1;

                        IsReachedTarget(maze.playerPositions[0], maze.playerPositions[1]);
                        return true;
                    }
                case Keys.D:
                case Keys.Right:
                    { // 우
                        int col = maze.playerPositions[0] += 1;
                        int row = maze.playerPositions[1];
                        Point next = new(Player.Location.X + Convert.ToInt32(maze.WallSize), Player.Location.Y);

                        if (IsWay(col, row))
                        {
                            MovePlayer(next);
                            visited[col, row] = true;
                            histories.Add(new History(histories.Count + 1, next, col, row));
                        }
                        else
                            maze.playerPositions[0] -= 1;

                        IsReachedTarget(maze.playerPositions[0], maze.playerPositions[1]);
                        return true;
                    }
                case Keys.W:
                case Keys.Up: // 상
                    { // 상
                        int col = maze.playerPositions[0];
                        int row = maze.playerPositions[1] -= 1;
                        Point next = new(Player.Location.X, Player.Location.Y - Convert.ToInt32(maze.WallSize));

                        if (IsWay(col, row))
                        {
                            MovePlayer(next);
                            visited[col, row] = true;
                            histories.Add(new History(histories.Count + 1, next, col, row));

                        }
                        else
                            maze.playerPositions[1] += 1;

                        IsReachedTarget(maze.playerPositions[0], maze.playerPositions[1]);
                        return true;
                    }
                case Keys.S:
                case Keys.Down: // 하
                    {
                        int col = maze.playerPositions[0];
                        int row = maze.playerPositions[1] += 1;
                        Point next = new(Player.Location.X, Player.Location.Y + Convert.ToInt32(maze.WallSize));

                        if (IsWay(col, row))
                        {
                            MovePlayer(next);
                            visited[col, row] = true;
                            histories.Add(new History(histories.Count + 1, next, col, row));

                        }
                        else
                            maze.playerPositions[1] -= 1;

                        IsReachedTarget(maze.playerPositions[0], maze.playerPositions[1]);
                        return true;
                    }

            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private async void Button_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btn) return;
            if (labelMessage == null) return;

            int tag = Convert.ToInt32(btn.Tag);

            switch (tag)
            {
                case 0:
                    {
                        Controls.Add(Ground = new UPictureBox(groundSize, Ground_Paint));
                        maze = new Maze(count, Ground.Height);
                        await maze.RunAsync();
                        CenterOfForm();
                    }
                    break;

                case 1:
                    {
                        if (maze == null) return;

                        Controls.Add(Player = new UPictureBox(Convert.ToInt32(maze.WallSize), Player_Paint));
                        if (Ground == null) return;

                        Player.Location
                            = new Point(Ground.Location.X + Convert.ToInt32(maze.WallSize), Ground.Location.Y + Convert.ToInt32(maze.WallSize));

                        Player.BringToFront();

                        histories = [];
                        histories.Add(new History(1, Player.Location, 1, 1));

                        GetTrace();
                        FinalDestination();
                        visited = new bool[count, count];
                        visited[1, 1] = true;
                    }
                    break;

                case 2: // Start
                    {
                        visited = new bool[count, count];
                        visited[1, 1] = true;
                        timer = new System.Windows.Forms.Timer { Interval = 30 };
                        timer.Tick += Timer_Tick;
                        timer?.Start();
                        IsStopped = false;
                    }; break;

                case 3:
                    for (int i = 0; i < 29; i++)
                        Goals($"Ball_{i % 10}.png");

                    break;
                case 4: RemoveMap(); break;
                case 5:; break;
                case 6: // Stop
                    {
                        IsStopped = true;
                        timer?.Stop();
                        timer = null;
                    }
                    break;
                case 7:
                    break;

                case 8: // 경로그리기
                    {
                        if (Ground == null) return;
                        isPath = true;
                        Ground.Invalidate();
                    }
                    break;
                case 9: Close(); return;
                case 10: await RepeatSearch(); return;
                default: break;
            }

        }


    }
}
