namespace VivAnimate.Effects.Bounds
{
    public class VerticalFoldEffect : IEffect
    {
        public int GetCurrentValue(Control control)
        {
            return control.Width;
        }

        public void SetValue(Control control, int originalValue, int valueToReach, int newValue)
        {
            var center = new Point((control.Left + control.Right) / 2, control.Top);
            var size = new Size(newValue, control.Height);
            var location = new Point(center.X - (newValue / 2), control.Top);
            control.Bounds = new Rectangle(location, size);
        }

        public int GetMinimumValue(Control control)
        {
            if (control.MinimumSize.IsEmpty) return int.MinValue;
            return control.MinimumSize.Width;
        }

        public int GetMaximumValue(Control control)
        {
            if (control.MaximumSize.IsEmpty) return int.MaxValue;
            return control.MaximumSize.Width;
        }

        public EffectInteractions Interaction
        {
            get { return EffectInteractions.BOUNDS; }
        }
    }
}
