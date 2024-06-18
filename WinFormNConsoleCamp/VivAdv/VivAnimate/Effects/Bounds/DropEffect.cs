namespace VivAnimate.Effects.Bounds
{
    public class DropEffect : IEffect
    {
        public EffectInteractions Interaction
        {
            get => EffectInteractions.Y;
        }
        public int GetCurrentValue(Control control) => control.Top;
        public void SetValue(Control control, int originalValue, int valueToReach, int newValue) => control.Top = newValue;
        public int GetMinimumValue(Control control) => int.MinValue;
        public int GetMaximumValue(Control control)=>int.MaxValue;

    }
}
