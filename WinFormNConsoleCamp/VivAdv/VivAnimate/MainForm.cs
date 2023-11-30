using System.Windows.Forms;
using VivAnimate.Effects;
using VivAnimate.Effects.Bounds;
using VivAnimate.Effects.Colors;
using VivAnimate.Effects.Opacity;
using VivAnimate.Extensions;
using VivAnimate.VControls;

namespace VivAnimate
{
    public partial class MainForm : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return handleParam;
            }
        }

        private Size size;

        private readonly VLabel label;

        private readonly GroupBox box;

        private readonly VButton? button;

        private ExampleFoldAnimation? example;

        public MainForm()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            InitializeComponent();

            WindowState = FormWindowState.Maximized;

            size = new Size(500, 500);

            Controls.Add(label = new VLabel(size, Color.Magenta));

            Resize += (s, e) => label.Location = CenterOfForm(label);

            Controls.Add(box = new GroupBox
            {
                Dock = DockStyle.Right,
                Width = 100
            });

            var icons = new string[] {
                    "\u2188", // 0
                    "\u2187",
                    "\u2182",
                    "\u2181",
                    "\u2180",
                    "\u216F",
                    "\u216E",
                    "\u216D",
                    "\u216C",
                    "\u216B",
                    "\u216A", // 10
                    "\u2169",
                    "\u2168",
                    "\u2167",
                    "\u2166",
                    "\u2165",
                    "\u2164",
                    "\u2163",
                    "\u2162",
                    "\u2161",
                    "\u2160" // 20
                };
            for (int i = 0; i < icons.Length; i++)
            {
                box.Controls.Add(button = new VButton(icons[i], $"Btn_{i}", Height = 100, DockStyle.Top, i));
                button.Click += Button_Click;
            }
        }

        private void Button_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btn) return;
            int tag = Convert.ToInt32(btn.Tag);
            switch (tag)
            {
                case 0:
                    {
                        var effect = GetEffect(EffectType.BottomAnchoreHeight, label);
                        if (effect != null)
                            Run(effect, label); // 컨트롤 효과
                    }
                    break;

                case 1:
                    {
                        var effect = GetEffect(EffectType.Fold, label);
                        if (effect != null)
                            Run(effect, label); // 컨트롤 효과
                    }
                    break;

                case 2:
                    {
                        var effect = GetEffect(EffectType.HorizontalFold, label);
                        if (effect != null)
                            Run(effect, label); // 컨트롤 효과
                    }
                    break;

                case 3:
                    {
                        var effect = GetEffect(EffectType.LeftAnchoredWidth, label);
                        if (effect != null)
                            Run(effect, label); // 컨트롤 효과
                    }
                    break;

                case 4:
                    {
                        var effect = GetEffect(EffectType.RightAnchoredWidth, label);
                        if (effect != null)
                            Run(effect, label); // 컨트롤 효과
                    }
                    break;

                case 5:
                    {
                        var effect = GetEffect(EffectType.TopAnchoreHeight, label);
                        if (effect != null)
                            Run(effect, label); // 컨트롤 효과
                    }
                    break;

                case 6:
                    {
                        var effect = GetEffect(EffectType.VerticalFold, label);
                        if (effect != null)
                            Run(effect, label); // 컨트롤 효과
                    }
                    break;

                case 7:
                    {
                        var effect = GetEffect(EffectType.XLocation, label);
                        if (effect != null)
                            Run(effect, label); // 컨트롤 효과
                    }
                    break;

                case 8:
                    {
                        var effect = GetEffect(EffectType.YLocation, label);
                        if (effect != null)
                            Run(effect, label); // 컨트롤 효과
                    }
                    break;
                case 9:
                    {
                        var effect = GetEffect(EffectType.ColorChannelShift, label);
                        if (effect != null)
                            Run(effect, label); // 컨트롤 효과
                    }
                    break;
                case 10:
                    {
                        var effect = GetEffect(EffectType.ColorShift, label);
                        if (effect != null)
                            Run(effect, label); // 컨트롤 효과
                    }
                    break;

                case 11:
                    {
                        var effect = GetEffect(EffectType.ControlFade, label);
                        if (effect != null)
                            Run(effect, label); // 컨트롤 효과
                    }
                    break;

                case 12:
                    {
                        var effect = GetEffect(EffectType.ColorShift, label);
                        if (effect != null)
                            Run(effect, label); // 컨트롤 효과
                    }
                    break;


                case 13:
                    {
                        var effect = GetEffect(EffectType.FormFade, this);
                        if (effect != null)
                            Run(effect, this); // 폼 효과, 50
                    }
                    break;

                case 19:
                    {
                        example?.Show();
                    }
                    break;
                case 20:
                    {
                        example = new ExampleFoldAnimation(label);
                        example.Hide();
                    }
                    break;

                default:
                    {
                        label.Size = size;
                        CenterOfForm(label);
                    }
                    break;
            }
        }

        private static void Run<T, C>(T effect, C control)
            where T : IEffect
            where C : Control
        {
            control.Animate
            (
                effect, //effect to apply implementing IEffect
                EasingFunctions.BounceEaseOut, //easing to apply
                0, //value to reach
                5000, //animation duration in milliseconds
                0 //delayed start in milliseconds
            );
        }

        private IEffect? GetEffect(EffectType type, Control control)
        {
            return type switch
            {
                EffectType.BottomAnchoreHeight => new BottomAnchoredHeightEffect(),
                EffectType.Fold => new FoldEffect(),
                EffectType.HorizontalFold => new HorizontalFoldEffect(),
                EffectType.LeftAnchoredWidth => new LeftAnchoredWidthEffect(),
                EffectType.RightAnchoredWidth => new RightAnchoredWidthEffect(),
                EffectType.TopAnchoreHeight => new TopAnchoredHeightEffect(),
                EffectType.VerticalFold => new VerticalFoldEffect(),
                EffectType.XLocation => new XLocationEffect(),
                EffectType.YLocation => new YLocationEffect(),
                EffectType.ColorChannelShift => new ColorChannelShiftEffect(ColorChannelShiftEffect.ColorChannels.B),
                EffectType.ColorShift => new ColorShiftEffect(),
                EffectType.ControlFade => new ControlFadeEffect(control),
                EffectType.FormFade => new FormFadeEffect(),
                _ => null,
            };
        }

        private Point CenterOfForm<T>(T t) where T : Control
            => new((Width - t.Width) / 2, (Height - t.Height) / 2);
    }
}


/*
 
    for (int i = 0; i <= 16; i++)
        Debug.WriteLine("{0,10} - {1:G}", i, (MultiHue)i);

    int a = 23;
    var text = "MyText";
    MultiHue hue = MultiHue.Green | MultiHue.Red | MultiHue.Blue;

    Debug.WriteLine($"x{text}x");
    Debug.WriteLine($"x{text,3}x");
    Debug.WriteLine($"x{text,10}x");
    Debug.WriteLine($"x{text,-10}x");
    Debug.WriteLine($"{hue}");
 
 */