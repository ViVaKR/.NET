namespace VivAnimate.Effects.Colors
{
    public class ColorChannelEffect(ColorChannelEffect.ColorChannels channel) : IEffect
    {
        public enum ColorChannels { A, R, G, B };

        public ColorChannels colorChannel = channel;

        public EffectInteractions Interaction
        {
            get { return EffectInteractions.COLOR; }
        }

        public int GetCurrentValue(Control control)
        {
            return colorChannel switch
            {
                ColorChannels.A => control.BackColor.A,
                ColorChannels.R => control.BackColor.R,
                ColorChannels.G => control.BackColor.G,
                ColorChannels.B => control.BackColor.B,
                _ => (int)control.BackColor.A,
            };
        }

        public void SetValue(Control control, int originalValue, int valueToReach, int newValue)
        {
            if (newValue >= 0 && newValue <= 255)
            {
                int a = control.BackColor.A;
                int r = control.BackColor.R;
                int g = control.BackColor.G;
                int b = control.BackColor.B;

                switch (colorChannel)
                {
                    case ColorChannels.A: a = newValue; break;
                    case ColorChannels.R: r = newValue; break;
                    case ColorChannels.G: g = newValue; break;
                    case ColorChannels.B: b = newValue; break;
                }

                control.BackColor = Color.FromArgb(a, r, g, b);
            }
        }

        public int GetMinimumValue(Control control)
        {
            return 0;
        }

        public int GetMaximumValue(Control control)
        {
            return 255;
        }
    }
}
