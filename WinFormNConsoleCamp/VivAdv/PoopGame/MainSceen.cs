
using PoopGame.UControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace PoopGame
{
    public partial class MainSceen : Form
    {

        private readonly Panel ground;
        private readonly GroupBox box;
        private readonly UButton btnStart;
        private readonly UButton btnExit;
        private readonly Random random;
        private List<UvPoop> poops;
        private UvPoop player;
        private readonly Timer timer;
        private bool direction;
        private int level = 0;
        private int height = 0;
        private readonly Label lblScore;
        private int score;
        private SoundPlayer sound;
        private readonly List<UvPoop> dies;

        public MainSceen()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();

            FormBorderStyle = FormBorderStyle.None;

            dies = new List<UvPoop>();

            timer = new Timer { Interval = 30 };
            timer.Tick += Timer_Tick;
            random = new Random(new Guid().GetHashCode());

            Controls.Add(box = new GroupBox { Dock = DockStyle.Right, Width = 250 });
            box.BackColor = Color.Transparent;
            box.Controls.Add(btnStart = new UButton("\u2620", 0));
            box.Controls.Add(btnExit = new UButton("\u21DD", 0));
            btnExit.Click += (s, e) => Close();
            btnExit.SendToBack();

            btnStart.Click += Play_Click;
            Controls.Add(ground = new Panel { Dock = DockStyle.Fill, BackColor = Color.White });
            ground.BringToFront();
            ground.BackColor = Color.LightSkyBlue;

            box.Controls.Add(lblScore = new Label
            {
                Dock = DockStyle.Top,
                AutoSize = false,
                TextAlign = ContentAlignment.BottomCenter,
                Font = new Font(Font.FontFamily, 24f, FontStyle.Bold),
                Height = 200
            }) ;
            lblScore.BringToFront();
            lblScore.ForeColor = Color.DarkRed;
            lblScore.Text = 0.ToString();
        }

        /// <summary>
        /// 똥 초기화
        /// </summary>
        private void InitPoops()
        {
            poops = new List<UvPoop>();
            string imageFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets");
            var di = new DirectoryInfo(imageFile);
            foreach (FileInfo poop in di.GetFiles().OrderBy(x => x.Name))
            {
                if (poop.FullName.Contains("Boom.png") || poop.FullName.Contains("Smile.png") || poop.FullName.Contains("poopEnd.png") ) continue;
                if (poop.FullName.Contains("player"))
                {
                    player = new UvPoop(Image.FromFile(poop.FullName), ground);
                    direction = true;
                    continue;
                }

                poops.Add(new UvPoop(Image.FromFile(poop.FullName), ground));
            }
            foreach (FileInfo poop in di.GetFiles())
            {
                if (poop.FullName.Contains("Boom.png") || poop.FullName.Contains("Smile.png") || poop.FullName.Contains("poopEnd.png")) continue;
                if (poop.FullName.Contains("player")) continue;

                poops.Add(new UvPoop(Image.FromFile(poop.FullName), ground));
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            level += height++ % 30 == 0 ? 2 : 1;

            if(dies.Count >= 12)
            {
                foreach (var item in dies)
                {
                    ground.Controls.Remove(item);
                }
                dies.Clear();
            }


            var idx = random.Next(0, poops.Count);
            var loc = new Point(poops[idx].Location.X, poops[idx].Location.Y + level);
            if (loc.Y >= ground.Height - 200)
            {
                var die = new UvPoop(Image.FromFile(@"Assets\poopEnd.png"), ground);

                dies.Add(die);
                ground.Controls.Add(die);
                die.Location = new Point(poops[idx].Location.X, ground.Height - 200);
                poops[idx].Location = new Point(poops[idx].Location.X, random.Next(5, ground.Height / 4));

                lblScore.Text = $"{++score}";

            }
            else
            {
                poops[idx].Location = loc;
            }

            var r = new Rectangle(poops[idx].Location, poops[idx].Size);
            var p = new Rectangle(player.Location, player.Size);
            if (r.IntersectsWith(p))
            {
                timer.Stop();
                player.BackgroundImage = Image.FromFile(@"Assets\Smile.png");
                sound.Stop();
                PlayBackSound(@"poop.wav");
                MessageBox.Show("아~~ 드러,  똥 맞았넹. ", "게임종료");
            }

        }

        private void SetPoops()
        {
            int i = 0;
            foreach (var poop in poops)
            {
                ground.Invoke(new MethodInvoker(() =>
                {
                    ground.Controls.Add(poop);

                    int vertical = random.Next(5, ground.Height / 4);
                    poop.Location = new Point(((i++ * ground.Width) / 12) + 100, vertical);

                }));

            }

        }


        private void PlayBackSound(string wav)
        {
            sound = new SoundPlayer(wav);
            sound.Play();
 

        }

        /// <summary>
        /// 플레이 및 리플레이
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Play_Click(object sender, EventArgs e)
        {
            PlayBackSound(@"former.wav");
            score = 0;
            lblScore.Text = score.ToString();
            level = 0;
            height = 0;
            ground.Controls.Clear();
            InitPoops();
            dies.Clear();
            //await Task.Run(() => PlayBackSound());
            await Task.Run(() => SetPoops());
            ground.Controls.Add(player);
            player.Location = new Point(ground.ClientSize.Height - player.Height, (ground.ClientSize.Width - player.Width) / 2);

            timer.Start();
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                    player.Location = new Point(player.Location.X + 50, player.Location.Y);
                    if (direction)
                    {
                        player.BackgroundImage.RotateFlip(RotateFlipType.Rotate180FlipY);
                        direction = !direction;
                    }

                    return true;
                case Keys.Left:
                    player.Location = new Point(player.Location.X - 50, player.Location.Y);
                    if (!direction)
                    {
                        player.BackgroundImage.RotateFlip(RotateFlipType.Rotate180FlipY);
                        direction = !direction;
                    }

                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
