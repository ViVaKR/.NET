namespace VivAnimate.Effects.Bounds
{
    public class RightAnchoreEffect : IEffect
    {
        public int GetCurrentValue(Control control)
        {
            return control.Width;
        }

        public void SetValue(Control control, int originalValue, int valueToReach, int newValue)
        {
            var size = new Size(newValue, control.Height);
            var location = new Point(control.Left +
                (control.Width - newValue), control.Top);

            control.Bounds = new Rectangle(location, size);
        }

        public int GetMinimumValue(Control control)
        {
            if (control.MinimumSize.IsEmpty)
                return int.MinValue;

            return control.MinimumSize.Width;
        }

        public int GetMaximumValue(Control control)
        {
            if (control.MaximumSize.IsEmpty)
                return int.MaxValue;

            return control.MaximumSize.Width;
        }

        public EffectInteractions Interaction
        {
            get { return EffectInteractions.BOUNDS; }
        }
    }
}
