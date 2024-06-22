
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WMPLib;

namespace SpaceShooter
{
    public partial class MainSceen : Form
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

        private readonly Random random;

        private PictureBox player;

        private Timer StarMoveTimer { get; set; }

        private Timer[] PlayerMoveTimer { get; set; }

        private Timer playerBulletTimer;
        private Timer enemyMoveTimer;
        private Timer enemyBulletTimer;

        private const int playerSpeed = 60;
        private const int playerBulletSpeed = 50;

        private int enemySpeed = 2;
        private int enemyBulletSpeed = 1;

        private string playerImageFile;

        private PictureBox[] enemies;
        private PictureBox[] enemyBullets;
        private PictureBox[] playerBullets;

        private string playerBulletImageFile;
        private WindowsMediaPlayer gameMedia;
        private WindowsMediaPlayer shootMedia;
        private WindowsMediaPlayer explosion;

        private int score;
        private Label lblScore;
        private int level;
        private Label lblLevel;
        private int dificulty;
        private bool pause;
        private bool gameIsOver;

        public MainSceen()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            DoubleBuffered = true;

            InitializeComponent();

            WindowState = FormWindowState.Maximized;
            BackColor = Color.Blue;

            score = 0;
            level = 1;
            dificulty = 9;
            random = new Random();

            LblTitle.Visible =
            BtnReplay.Visible =
            BtnExit.Visible =
            PicTitleImage.Visible = false;

            Load += (s, e) => GameStart();
            BtnReplay.Click += BtnReplay_Click;

            // Paint += MainSceen_Paint;
            BtnReplay.BackColor = Color.LightSkyBlue;
            BtnExit.BackColor = Color.LightSkyBlue;
        }

        private readonly Color[] colors = new[]
        {
            Color.White,
            Color.Yellow,
            Color.Magenta,
            Color.LightSeaGreen,
            Color.Wheat,
            Color.FromArgb(125, 255, 0, 255),
            Color.FromArgb(125, 125, 125,125),
            Color.FromArgb(125, 125, 0, 0),
            Color.FromArgb(50, 0, 255, 0),
            Color.FromArgb(80, 0, 0, 255)

        };
        private void MainSceen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            for (int i = 0; i < 40; i++)
            {
                var x = random.Next(0, Width);
                var y = random.Next(0, Height);
                var wh = random.Next(4, 16);
                int color = random.Next(0, colors.Length);


                e.Graphics.FillEllipse(new SolidBrush(colors[color]), x, y, wh, wh);
            }

        }

        private void GameStart()
        {
            #region Score
            // 스코어
            lblScore = new Label
            {
                BackColor = Color.Transparent,
                AutoSize = false,
                Font = new Font(Font.FontFamily, 24f, FontStyle.Bold),
                ForeColor = Color.Yellow,
                Location = new Point(50, 0),
                TextAlign = ContentAlignment.MiddleLeft,
                Height = 100,
                Width = 300,
                Text = "SCORE: 0"
            };
            Controls.Add(lblScore);

            // 레벨
            lblLevel = new Label
            {
                BackColor = Color.Transparent,
                AutoSize = false,
                Font = new Font(Font.FontFamily, 24f, FontStyle.Bold),
                ForeColor = Color.Yellow,
                Location = new Point(Width - 300, 0),
                TextAlign = ContentAlignment.MiddleLeft,
                Height = 100,
                Width = 300,
                Text = "LEVEL: 0"
            };
            Controls.Add(lblLevel);
            #endregion

            #region Player
            // Player

            playerImageFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "player.png");
            player = new PictureBox
            {
                Width = 96,
                Height = 96,
                Name = "Player",
                BackColor = Color.Transparent,
                Location = new Point((Width - 96) / 2, Height - (96 * 2)),
                Image = Image.FromFile(playerImageFile),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            PicTitleImage.Image = Image.FromFile(playerImageFile);
            Controls.Add(player);

            playerBulletImageFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "arrow.png");
            playerBullets = new PictureBox[20];
            for (int i = 0; i < playerBullets.Length; i++)
            {
                playerBullets[i] = new PictureBox
                {
                    Size = new Size(20, 20),
                    Image = Image.FromFile(playerBulletImageFile),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.None
                };
                Controls.Add(playerBullets[i]);
            }

            playerBulletTimer = new Timer { Interval = 5 };
            playerBulletTimer.Tick += PlayerBulletTimer_Tick;

            #endregion

            #region Enemy
            enemies = new PictureBox[20];
            Image[] e = new Image[enemies.Length];

            for (int i = 0; i < e.Length; i++)
                e[i] = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", $"E{i % 5}.png"));


            for (int i = 0; i < enemies.Length; i++)
            {

                enemies[i] = new PictureBox
                {
                    Size = new Size(40, 40),
                    Visible = false,
                    BorderStyle = BorderStyle.None,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BackColor = Color.Transparent,
                    Location = new Point((i + 1) * (Width / enemies.Length) * 2, (i % 3 == 0) ? -50 : -250)

                };
                Controls.Add(enemies[i]);
            }

            for (int i = 0; i < e.Length; i++)
                enemies[i].Image = e[i];

            enemyMoveTimer = null;
            enemyMoveTimer = new Timer
            {
                Interval = 30,
                Enabled = true
            };
            enemyMoveTimer.Tick += EnemyMoveTimer_Tick;

            var bullet = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "boom.png");
            enemyBullets = new PictureBox[enemies.Length];
            for (int i = 0; i < enemyBullets.Length; i++)
            {
                int x = random.Next(0, enemies.Length);
                enemyBullets[i] = new PictureBox
                {
                    Size = new Size(25, 25),
                    Visible = false,
                    Image = Image.FromFile(bullet),
                    BackColor = Color.Transparent,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Location = new Point(enemies[x].Location.X, enemies[x].Location.Y - 20)
                };
                Controls.Add(enemyBullets[i]);
            }

            enemyBulletTimer = null;
            enemyBulletTimer = new Timer
            {
                Interval = 1000/25,
                Enabled = true
            };
            enemyBulletTimer.Tick += EnemyBulletTimer_Tick;



            #endregion

            StartTimers();

            MessageShow(false);
            CreateWMP();
        }


        private void CreateWMP()
        {
            gameMedia = new WindowsMediaPlayer
            {
                URL = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", "GameSong.mp3")
            };
            gameMedia.settings.setMode("loop", true);
            gameMedia.settings.volume = 5;
            gameMedia.controls.play();

            shootMedia = new WindowsMediaPlayer
            {
                URL = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", "shoot.mp3")
            };
            shootMedia.settings.volume = 1;

            explosion = new WindowsMediaPlayer
            {
                URL = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", "boom.mp3")
            };
            explosion.settings.volume = 6;
        }


        private void EnemyBulletTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < enemyBullets.Length; i++)
            {
                if (enemyBullets[i].Top < Height)
                {
                    enemyBullets[i].Visible = true;
                    enemyBullets[i].Top += enemyBulletSpeed * (i + 1);

                    CollisionWithEnemyBullets();
                }
                else
                {
                    enemyBullets[i].Visible = false;
                    int x = random.Next(0, enemies.Length);
                    enemyBullets[i].Location = new Point(enemies[x].Location.X, enemies[x].Location.Y);
                }
            }
        }

        private void EnemyMoveTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].Visible = true;
                enemies[i].Top += random.Next(enemySpeed, enemySpeed * 5);

                if (enemies[i].Top > Height)
                {
                    var x = random.Next(10, ClientRectangle.Width - 100);
                    var y = random.Next(100, 500);

                    enemies[i].Location = new Point(x, -y);
                }
            }
        }

        private void PlayerBulletTimer_Tick(object sender, EventArgs e)
        {
            if (playerBullets == null || playerBullets.Length <= 0) return;
            if (player == null) return;

            shootMedia.controls.play(); // Shoot Sound Play

            for (int i = 0; i < playerBullets.Length; i++)
            {
                if (playerBullets[i].Top > 0)
                {
                    playerBullets[i].Visible = true;
                    playerBullets[i].Top -= playerBulletSpeed;

                    Collision();
                }
                else
                {
                    playerBullets[i].Visible = false;

                    var x = (i % 2 == 0) ? player.Location.X : player.Location.X + player.Width;
                    playerBullets[i].Location = new Point(x, player.Location.Y - (i * 20));
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    {
                        if (player.Left > 10) player.Left -= playerSpeed;
                    }
                    return true;

                case Keys.Right:
                    {
                        if (player.Right < (Width - 48)) player.Left += playerSpeed;
                    }
                    return true;

                case Keys.Up:
                    {
                        if (player.Top > 10) player.Top -= playerSpeed;
                    }
                    return true;

                case Keys.Down:
                    {
                        if (player.Top < (Height - player.Height - 50)) player.Top += playerSpeed;
                    }
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Collision()
        {
            for (int i = 0; i < playerBullets.Length; i++)
            {
                for (int j = 0; j < enemies.Length; j++)
                {
                    // 적 요격시
                    if (playerBullets[i].Bounds.IntersectsWith(enemies[j].Bounds))
                    {
                        explosion.controls.play();

                        score += 1;

                        lblScore.Text = $"SCORE: {score}";
                        if (score % 30 == 0)
                        {
                            level += 1;
                            lblLevel.Text = $"LEVEL: {level}";
                            if (enemySpeed <= 10 && enemyBulletSpeed <= 10 && dificulty >= 0)
                            {
                                dificulty--;
                                enemySpeed++;
                                enemyBulletSpeed++;
                            }
                            if (level == 10)
                                GameOver("NICE DONE", true);

                        }

                        var x = random.Next(50, ClientRectangle.Width - 150);
                        enemies[j].Location = new Point(x, (j % 2 == 0) ? -50 : -100);
                    }

                    // 적과 충돌시
                    if (player.Bounds.IntersectsWith(enemies[j].Bounds))
                    {
                        explosion.settings.volume = 30;
                        explosion.controls.play();
                        player.Visible = false;
                        GameOver("Game Over", true);
                        return;
                    }
                }
            }
        }

        private void GameOver(string message, bool show)
        {
            gameMedia.controls.stop();
            LblTitle.Text = message;
            MessageShow(show);
        }

        private void MessageShow(bool show)
        {
            gameIsOver =
            pause =
            LblTitle.Visible =
            BtnReplay.Visible =
            BtnExit.Visible =
            PicTitleImage.Visible = show;
            player.Visible = !show;
            if (show)
            {
                pause = gameIsOver = true;
                var pointLabel = new Point((Width - LblTitle.Width) / 2, 100);
                PicTitleImage.Width = BtnReplay.Width = BtnExit.Width = 400;
                BtnReplay.Height = BtnExit.Height = 100;
                PicTitleImage.Height = 400;
                var pointReplay = new Point((Width - BtnReplay.Width) / 2, 700);
                var pointExit = new Point(pointReplay.X, pointReplay.Y + 110);

                PicTitleImage.Location = new Point((Width - PicTitleImage.Width) / 2, 300);
                LblTitle.Location = pointLabel;
                BtnReplay.Location = pointReplay;
                BtnExit.Location = pointExit;
            }
        }

        private void StopTimers()
        {
            enemyMoveTimer.Stop();
            playerBulletTimer.Stop();
            enemyBulletTimer.Stop();
        }

        private bool StartTimers()
        {
            enemyMoveTimer.Start();
            playerBulletTimer.Start();
            enemyBulletTimer.Start();
            return true;
        }

        private void CollisionWithEnemyBullets()
        {
            if (player == null) return;

            for (int i = 0; i < enemyBullets.Length; i++)
            {
                if (enemyBullets[i].Bounds.IntersectsWith(player.Bounds))
                {
                    enemyBullets[i].Visible = false;
                    explosion.settings.volume = 30;
                    explosion.controls.play();

                    GameOver("Game Over", true);
                    return;
                }
            }
        }

        private void BtnReplay_Click(object sender, EventArgs e)
        {
            List<Control> result = Controls.OfType<Control>().Where(item => !item.Name.Equals(nameof(LblTitle))
                    && !item.Name.Equals(nameof(BtnReplay))
                    && !item.Name.Equals(nameof(BtnExit))
                    && !item.Name.Equals(nameof(PicTitleImage))
                    && !item.Name.Equals("Player")).ToList();

            foreach (var item in result)
                Controls.Remove(item);

            GameStart();
        }

        private void BtnExit_Click(object sender, EventArgs e) => Environment.Exit(1);

    }
}
