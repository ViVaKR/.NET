using System.Drawing;
using System.Windows.Forms;
using VisualEffects;
using VisualEffects.Animations.Effects;
using VisualEffects.Easing;
using VisualEffects.Effects.Bounds;

namespace ControlAnimateDemo
{

    /// <summary>
    /// Ref. `https://www.codeproject.com/Articles/827808/Control-Animation-in-Winforms`
    /// </summary>
    public partial class MainForm : Form
    {
        //이렇게 하면 양식 수준 아래의 모든 컨트롤에 대해 이중 버퍼링이 활성화됩니다.
        //그렇지 않으면 각 컨트롤에 대해 이중 버퍼링을 개별적으로 활성화해야 합니다.
        //담요형 이중 버퍼링이 원치 않는 부작용을 일으킬 수 있으므로 이후에 이중 버퍼링을 미세 조정하는 것이 좋습니다.
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return handleParam;
            }
        }

        private readonly VivLabel label;

        public MainForm()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            InitializeComponent();

            Width = 1600;
            Height = 1600;
            StartPosition = FormStartPosition.CenterScreen;

            Controls.Add(label = new VivLabel(400, 400, Color.Magenta));

            label.Location = CenterOfForm(label);

            Run(GetEffect(EffectType.BottomAnchoreHeight, label), label); // 컨트롤 효과
            // Run(GetEffect(EffectType.FormFade, this), this); // 폼 효과, 50
        }

        private Point CenterOfForm(Control control) => new Point((Width - control.Width) / 2, (Height - control.Height) / 2);

        private IEffect GetEffect(EffectType type, Control control)
        {
            switch (type)
            {
                case EffectType.XLocation: return new XLocationEffect();
                case EffectType.YLocation: return new YLocationEffect();
                case EffectType.Fold: return new FoldEffect();
                case EffectType.VerticalFolld: return new VerticalFoldEffect();
                case EffectType.BottomAnchoreHeight: return new BottomAnchoredHeightEffect();
                case EffectType.LeftAnchoredWidth: return new LeftAnchoredWidthEffect();
                case EffectType.RightAnchoredWidth: return new RightAnchoredWidthEffect();
                case EffectType.TopAnchoredHeight: return new TopAnchoredHeightEffect();
                case EffectType.ColorChannelShift: return new ColorChannelShiftEffect(ColorChannelShiftEffect.ColorChannels.B);
                case EffectType.ColorShift: return new ColorShiftEffect(); // value to reach -> -1
                case EffectType.ControlFade: return new ControlFadeEffect(control); // 1 ~ 100
                case EffectType.FormFade: return new FormFadeEffect(); // 1 ~ 100
                default: return null;
            }
        }

        private void Run<T, C>(T effect, C control) where T : IEffect where C : Control
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
    }

    public enum EffectType
    {
        BottomAnchoreHeight,
        Fold,
        HorizontalFold,
        LeftAnchoredWidth,
        RightAnchoredWidth,
        TopAnchoredHeight,
        VerticalFolld,
        XLocation,
        YLocation,

        ColorChannelShift,
        ColorShift,

        ControlFade,
        FormFade
    }
}
