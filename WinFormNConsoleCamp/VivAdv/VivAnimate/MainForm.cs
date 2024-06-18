
using VivAnimate.Effects;
using VivAnimate.Effects.Bounds;
using VivAnimate.Effects.Colors;
using VivAnimate.Effects.Opacity;
using VivAnimate.Extensions;
using VivAnimate.VControls;
using Timer = System.Windows.Forms.Timer;

namespace VivAnimate
{
    public partial class MainForm : Form
    {
        // Double Bufferring
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return handleParam;
            }
        }

        private readonly GroupBox box; // 버튼 컨테이너
        private readonly VButton? button; // 버튼 사용자 컨트롤
        private VPictureBox? player; // 사진박스 사용자 컨트롤 (플레이어)
        private int reachPosition; // 애니메이션 최종 목적지
        private readonly int duration = 4000; // 애니메이션 총 진행시간
        private int time;
        private int delay; // 시작전 지연시간
        private readonly int top; // Player Top
        private readonly int playerSize = 1000;
        private readonly ColorChannelEffect.ColorChannels colorChannel;
        private readonly Timer timer;
        private readonly Label label; // 플레이 시간

        public MainForm()
        {

            InitializeComponent();

            time = duration / 1000;
            timer = new Timer { Interval = 1000 };
            timer.Tick += Timer_Tick;

            FormBorderStyle = FormBorderStyle.None; // 폼 보더 없애기

            WindowState = FormWindowState.Maximized;

            CreatePlayer(); // 플레이어 생성

            Controls.Add(box = new GroupBox { Dock = DockStyle.Bottom, Height = 200 });

            // 경과 시간 및 플레이 상태 정보
            Controls.Add(label = new Label
            {
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(Font.FontFamily, 48f, FontStyle.Bold),
                ForeColor = Color.Teal,
                Height = 150,
                BackColor = Color.Transparent,
                Text = time.ToString()
            });
            label.SendToBack();

            top = label.Bottom + 10;

            for (int i = 0; i < Enum.GetValues(typeof(EffectType)).Length; i++)
            {
                box.Controls.Add(button = new VButton(((EffectType)i).ToString(), $"Btn_{i}", Width = 300, DockStyle.Left, i));
                button.Click += Button_Click;
                button.BringToFront();
            }

            Load += (s, e) =>
            {
                if (player == null) return;
                player.Location = PlayerTop(player);
            };

        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            label.Text = $"{--time}";
            if (time <= 0)
            {
                box.Enabled = true;
                timer.Stop();
                time = duration / 1000;
            }

        }

        /// <summary>
        /// 플레이어 생성
        /// </summary>
        /// <param name="width">가로</param>
        /// <param name="height">세로</param>
        private void CreatePlayer(int width = 1000, int height = 1000)
        {
            player?.Dispose();
            player ??= null;

            // 플레이어 이미지
            var imgFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sprites", "Jump.png");

            Image img = Image.FromFile(imgFile);

            // Y 축을 기준으로 180 회전, 우측을 보던것을 좌측을 보게 하기
            img.RotateFlip(RotateFlipType.Rotate180FlipY);

            player = new(imgFile)
            {
                Image = img,
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(width, height),

            };
            player.Location = PlayerTop(player);
            Controls.Add(player);
            player.BringToFront();
        }

        private async void Button_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btn || player == null) return;

            timer.Start();
            box.Enabled = false;
            int tag = Convert.ToInt32(btn.Tag);
            try
            {
                switch (tag)
                {
                    case 0: // 문의에 대한 답변 파트 : 추락 효과
                        {
                            int goal = ClientRectangle.Height - player.Height - box.ClientRectangle.Height;
                            await BeforRunSettings(goal, EffectType.Drop, false);
                        }
                        break;
                    case 1: await BeforRunSettings(0, EffectType.ToLeft, true); break;
                    case 2: await BeforRunSettings(0, EffectType.ToCenter, true); break;
                    case 3: await BeforRunSettings(0, EffectType.RightFade, true); break;
                    case 4: await BeforRunSettings(0, EffectType.TopFade, true); break;
                    case 5: await BeforRunSettings(0, EffectType.LeftFade, true); break;
                    case 6: await BeforRunSettings(0, EffectType.BottomFade, true); break;
                    case 7: await BeforRunSettings(20, EffectType.FormFade, true, true); break;
                    case 8: await BeforRunSettings(50, EffectType.Channel, true); break;
                    case 9: await BeforRunSettings(0, EffectType.ControlFade, true); break;
                    default: Close(); break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }

        private async Task BeforRunSettings(int goal, EffectType effectType, bool center, bool isForm = false)
        {
            reachPosition = goal;

            delay = 0;

            if (!isForm) // Player
            {
                if (player == null) return;
                CreatePlayer(playerSize, playerSize);
                player.Location = center ? PlayerCenter(player) : PlayerTop(player);

                if (effectType == EffectType.ToLeft)
                    player.Location = new Point(ClientRectangle.Right - player.Width, label.Height + 10);


                var effect = GetEffect(effectType, player);
                if (effect != null) await RunAsync(effect, player);
            }
            else // 폼 자체
            {
                var frmEffect = GetEffect(effectType, this);
                if (frmEffect != null)
                    await RunAsync(frmEffect, this);
            }
        }

        private async Task RunAsync<T, C>(T effect, C control)
            where T : IEffect
            where C : Control
        {
            await Task.Run(() => control.Animate
            (
                // 적용될 효과 (EffectTyp)
                effect,

                EffectsEngine.BounceEaseOut,
                // EffectsEngine.ElasticEaseOut,
                //  EffectsEngine.ElasticEaseInOut,
                reachPosition, // 도달할 위치 및 색상값
                duration, // milliseconds (진행하는 총 시간)
                delay // milliseconds (시작전 지연시간)
            ));
        }

        private IEffect? GetEffect(EffectType type, Control control)
        {
            return type switch
            {
                EffectType.Drop => new DropEffect(), // Drop
                EffectType.ToLeft => new ToLeftEffect(), // To Left
                EffectType.ToCenter => new ToCenterFoldEffect(),
                EffectType.BottomFade => new BottomAnchoreEffect(),
                EffectType.LeftFade => new LeftAnchoreEffect(),
                EffectType.RightFade => new RightAnchoreEffect(),
                EffectType.TopFade => new TopAnchoreEffect(),
                EffectType.FormFade => new FormFadeEffect(),
                EffectType.Channel => new ColorChannelEffect(colorChannel),
                EffectType.ControlFade => new ControlFadeEffect(control),
                _ => null
            };
        }

        private Point PlayerTop<T>(T t) where T : Control => new((Width - t.Width) / 2, top);

        private Point PlayerCenter<T>(T t) where T : Control => new((Width - t.Width) / 2, (ClientRectangle.Bottom - t.Height) / 2);
    }
}
