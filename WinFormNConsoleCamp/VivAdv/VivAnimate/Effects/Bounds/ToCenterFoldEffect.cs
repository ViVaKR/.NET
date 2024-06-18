namespace VivAnimate.Effects.Bounds
{
    public class ToCenterFoldEffect : IEffect
    {
        public EffectInteractions Interaction
        {
            get { return EffectInteractions.BOUNDS; }
        }

        public int GetCurrentValue(Control control) 
            => control.Height;

        public void SetValue(Control control, int originalValue, int valueToReach, int newValue)
        {
            var center = new Point(control.Left,  (control.Top + control.Bottom) / 2);

            var size = new Size(control.Width, newValue);
            var location = new Point(control.Left,
                center.Y - (newValue / 2));

            control.Bounds = new Rectangle(location, size);
        }

        public int GetMinimumValue(Control control)
        {
            if (control.MinimumSize.IsEmpty) return int.MinValue;

            return control.MinimumSize.Height;
        }

        public int GetMaximumValue(Control control)
        {
            if (control.MaximumSize.IsEmpty) return int.MaxValue;

            return control.MaximumSize.Height;
        }


    }
}
