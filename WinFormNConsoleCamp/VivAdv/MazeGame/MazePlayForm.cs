
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
        private int maxScore = 15;
        private readonly int count = 20;
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
            }
        }
        private List<int[]>? WayToGo { get; set; }
        private readonly List<PictureBox> monsters = [];
        private readonly Dictionary<int, PictureBox> targets = [];
        private readonly Dictionary<int[,], Rectangle> rc = [];
        private bool[,]? visited;
        private System.Windows.Forms.Timer? timer;
        private int counter;
        private List<Point> histories = [];
        public List<Point> Histories
        {
            get => histories;
            set
            {
                histories = value;
            }
        }
        private readonly List<History> history;

        public MazePlayForm()
        {
            SetStyle( // 더블버퍼링
                ControlStyles.AllPaintingInWmPaint
                | ControlStyles.UserPaint
                | ControlStyles.DoubleBuffer, true);

            InitializeComponent();

            WindowState = FormWindowState.Maximized;

            history = [];

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
                new UButton("AI", "Btn_PlayGame5", 7, DockStyle.Top, Button_Click),
                new UButton("Check", "Btn_PlayGame4", 6, DockStyle.Top, Button_Click),
                new UButton("Destination", "Btn_PlayGame3", 5, DockStyle.Top, Button_Click),
                new UButton("Clear", "Btn_PlayGame2", 4, DockStyle.Top, Button_Click),
                new UButton("TARGET", "Btn_PlayGame1", 3, DockStyle.Top, Button_Click),
                new UButton("-", "BtnPlayMaze", 2, DockStyle.Top, Button_Click, enable: false),
                new UButton("PLAYER", "BtnDrawing", 1, DockStyle.Top, Button_Click),
                new UButton("MAP", "Btn_PlayMaze", 0, DockStyle.Top, Button_Click),
            });

            Controls.Add(labelScore = new ULabel(string.Empty, "Lbl_Score", DockStyle.Top, Color.DarkOrange));
            Controls.Add(labelMessage = new ULabel(string.Empty, "Lbl_Timer", DockStyle.Bottom, Color.Gray));
        }

        /// <summary>
        /// 버튼 클릭 컬렉션
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        labelMessage.Text = $"미궁 완성, 플레이어 와 타겟을 생성하세요";
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

                        Histories.Add(Player.Location);

                        GetTrace();

                        FinalDestination();

                        visited = new bool[count, count];
                        visited[1, 1] = true;
                    }
                    break;

                case 2: break;

                case 3:
                    for (int i = 0; i < 29; i++)
                        Goals($"Ball_{i % 10}.png");

                    break;
                case 4: RemoveMap(); break;

                case 5: FinalDestination(); break;

                case 6:

                    if (WayToGo != null)
                        foreach (var way in WayToGo)
                        {
                            Debug.WriteLine(WayToGo.IndexOf(way) + " " + string.Join(", ", way));
                        }
                    break;
                case 7: // AI
                    {
                        
                        visited = new bool[count, count];
                        visited[1, 1] = true;

                        timer = new System.Windows.Forms.Timer { Interval = 30 };
                        timer.Tick += Timer_Tick;
                        timer?.Start();
                    }; break;
                case 8: // Draw Path
                    {
                        DrawPath();
                    }
                    break;
                case 9: Close(); return;
                case 10: // 이잡듯이 찾기
                    RepeatSearch();
                    return;
                default: break;
            }

        }

        /// <summary>
        /// 막다른 골목에 막혔을 때 
        /// 트리구조의 분깃점에서 다시 시작하기
        /// 버튼 클릭 반자동이나
        /// 충분히 검토후 완전 자동화 대상
        /// 즉, 테스트용으로 버튼 클릭시 작동하도록 함
        /// </summary>
        private void RepeatSearch()
        {
            if (Player == null || maze == null || timer == null) return;

            foreach (var item in history.OrderByDescending(x => x.Id))
            {
                timer.Stop();
                var p = BackTrace(item);

                if (p)
                {
                    maze.playerPositions[0] = item.Col;
                    maze.playerPositions[1] = item.Row;
                    Player.Location = item.Point;

                    timer.Start();
                    return;
                }
            }
        }

        /// <summary>
        /// 맵 지우기 (다시하기용)
        /// </summary>
        private void RemoveMap()
        {
            Ground?.Dispose();
            Player?.Dispose();
            targets?.Clear();


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
        }

        /// <summary>
        /// 목표 달성 확인
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
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
                    PictureBox pb = targets.FirstOrDefault(x => x.Key == idx).Value;
                    targets.Remove(idx);

                    MessageBox.Show("잡았습니다. You Win!!!", "완료");
                    if (InvokeRequired)
                    {
                        BeginInvoke(new MethodInvoker(() =>
                        {
                            pb.Hide();
                            pb.Dispose();
                            Score++;
                        }));
                        return;
                    }
                    pb.Hide();
                    pb.Dispose();
                    Score++;
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

            Histories.Add(Player.Location);
        }

        /// <summary>
        /// Ai 자동 무빙 용 타이머
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Timer_Tick(object? sender, EventArgs e) => await RunAiAsync();

        /// <summary>
        /// 갈수 있는길인지 판정하기
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool IsWay(int col, int row)
        {
            if (maze == null || visited == null || col < 0 || row < 0 || col > groundSize || row > groundSize) return false;

            bool check1 = maze.map[col, row] == 0;
            bool check2 = !visited[col, row];
            return check1 && check2;
        }

        /// <summary>
        /// 이잡듯 막 알로리즘
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 경로 그리기
        /// </summary>
        private void DrawPath()
        {
            if (maze == null) return;

            foreach (var item in Histories.Distinct())
            {
                var pan = new Panel
                {
                    Width = (int)maze.WallSize / 2,
                    Height = (int)maze.WallSize / 2,
                    Left = item.X + (int)maze.WallSize / 4,
                    Top = item.Y + (int)maze.WallSize / 4,
                    BackColor = Color.Magenta
                };
                Controls.Add(pan);
                pan.BringToFront();
                Graphics g = pan.CreateGraphics();
                g.FillRectangle(Brushes.Yellow, item.X, item.Y, pan.Width, pan.Height);
                pan.Refresh();
            }
        }

        /// <summary>
        /// AI 반 자동 무빙
        /// </summary>
        /// <returns></returns>
        private async Task RunAiAsync()
        {
            if (maze == null || Player == null || visited == null) return;

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
                        Histories.Add(next);
                        history.Add(new History(history.Count + 1, next, col, row));
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
                        Histories.Add(next);
                        history.Add(new History(history.Count + 1, next, col, row));
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
                        Histories.Add(next);
                        history.Add(new History(history.Count + 1, next, col, row));
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
                        Histories.Add(next);
                        history.Add(new History(history.Count + 1, next, col, row));
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

        /// <summary>
        /// 완전 수동 무딩 키 핸들
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (maze == null || Player == null || visited == null) return base.ProcessCmdKey(ref msg, keyData);

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
                            Histories.Add(next);
                            history.Add(new History(history.Count + 1, next, col, row));
                            //counter++;
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
                            Histories.Add(next);
                            history.Add(new History(history.Count + 1, next, col, row));
                            //counter++;

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
                            Histories.Add(next);
                            history.Add(new History(history.Count + 1, next, col, row));
                            //counter++;

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
                            Histories.Add(next);
                            history.Add(new History(history.Count + 1, next, col, row));
                            //counter++;
                        }
                        else
                            maze.playerPositions[1] -= 1;

                        IsReachedTarget(maze.playerPositions[0], maze.playerPositions[1]);
                        return true;
                    }

            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
