using VivAnimate.Animators;
using VivAnimate.Effects;

namespace VivAnimate.Extensions
{
    public static class AnimationExtensions
    {
        public static AnimationStatus Animate(
            this Control control,
            IEffect iAnimation,
            EasingDelegate easing,
            int valueToReach,
            int duration,
            int delay,
            bool reverse = false,
            int loops = 1)
            => Animator.Animate(control, iAnimation, easing, valueToReach, duration, delay, reverse, loops);
    }
}
