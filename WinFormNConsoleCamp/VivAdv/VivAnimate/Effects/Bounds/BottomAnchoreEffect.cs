﻿namespace VivAnimate.Effects.Bounds
{
    public class BottomAnchoreEffect : IEffect
    {
        public int GetCurrentValue(Control control)
        {
            return control.Height;
        }

        public void SetValue(Control control, int originalValue, int valueToReach, int newValue)
        {
            var size = new Size(control.Width, newValue);
            var location = new Point(control.Left, control.Top +
                (control.Height - newValue));

            control.Bounds = new Rectangle(location, size);
        }

        public int GetMinimumValue(Control control)
        {
            if (control.MinimumSize.IsEmpty)
                return int.MinValue;

            return control.MinimumSize.Height;
        }

        public int GetMaximumValue(Control control)
        {
            if (control.MaximumSize.IsEmpty)
                return int.MaxValue;

            return control.MaximumSize.Height;
        }

        public EffectInteractions Interaction
        {
            get { return EffectInteractions.BOUNDS; }
        }
    }
}
