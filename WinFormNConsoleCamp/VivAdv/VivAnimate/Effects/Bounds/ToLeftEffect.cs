namespace VivAnimate.Effects.Bounds
{
    public class ToLeftEffect : IEffect
    {
        public EffectInteractions Interaction
        {
            get => EffectInteractions.X;
        }
        public int GetCurrentValue(Control control) => control.Left;
        public void SetValue(Control control, int originalValue, int valueToReach, int newValue) => control.Left = newValue;
        public int GetMinimumValue(Control control) => int.MinValue;
        public int GetMaximumValue(Control control) => int.MaxValue;

    }
}
