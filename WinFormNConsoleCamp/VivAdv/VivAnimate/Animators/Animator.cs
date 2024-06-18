
using System.Diagnostics;
using VivAnimate.Effects;

namespace VivAnimate.Animators
{
    public static class Animator
    {
        public static event EventHandler<AnimationStatus>? Animated;

        public static AnimationStatus Animate(Control control,
                    IEffect iEffect,
                    EasingDelegate easing,
                    int valueToReach,
                    int duration,
                    int delay,
                    bool reverse = false, int loops = 1)
        {
            var stopwatch = new Stopwatch();
            var cancelTokenSource = new CancellationTokenSource();
            var animationStatus = new AnimationStatus(cancelTokenSource, stopwatch);
            _ = new System.Threading.Timer((state) =>
            {
                int originalValue = iEffect.GetCurrentValue(control);
                if (originalValue == valueToReach)
                {
                    animationStatus.IsCompleted = true;
                    return;
                }
                int maxVal = iEffect.GetMaximumValue(control);
                if (valueToReach > maxVal)
                {
                    string message = $"최대값: {maxVal} 이하로 설정하세요. {valueToReach}";
                    throw new ArgumentException(message, "값 설정오류발생");
                }
                int minVal = iEffect.GetMinimumValue(control);
                if (valueToReach < iEffect.GetMinimumValue(control))
                {
                    string message = $"최대값: {minVal} 이하로 설정하세요. {valueToReach}";
                    throw new ArgumentException(message, "값 설정오류발생");
                }

                bool reversed = false;
                int performedLoops = 0;

                int actualValueChange = Math.Abs(originalValue - valueToReach);

                System.Timers.Timer animationTimer = new()
                {
                    Interval = (duration > actualValueChange) ?
                    (duration / actualValueChange) : actualValueChange
                };
                if (iEffect.Interaction == EffectInteractions.COLOR) animationTimer.Interval = 10;


                animationTimer.Elapsed += (s, e) =>
                {
                    if (cancelTokenSource.Token.IsCancellationRequested)
                    {
                        animationStatus.IsCompleted = true;
                        animationTimer.Stop();
                        stopwatch.Stop();
                        return;
                    }

                    bool increasing = originalValue < valueToReach;

                    int minValue = Math.Min(originalValue, valueToReach);
                    int maxValue = Math.Abs(valueToReach - originalValue);
                    int newValue = (int)easing(stopwatch.ElapsedMilliseconds, minValue, maxValue, duration);

                    if (!increasing)
                        newValue = (originalValue + valueToReach) - newValue - 1;

                    control.Invoke(new MethodInvoker(() =>
                    {
                        iEffect.SetValue(control, originalValue, valueToReach, newValue);

                        bool timeout = stopwatch.ElapsedMilliseconds >= duration;
                        if (timeout)
                        {
                            if (reverse && (!reversed || loops <= 0 || performedLoops < loops))
                            {
                                reversed = !reversed;
                                if (reversed) performedLoops++;

                                int initialValue = originalValue;
                                int finalValue = valueToReach;

                                valueToReach = valueToReach == finalValue ? initialValue : finalValue;
                                originalValue = valueToReach == finalValue ? initialValue : finalValue;

                                stopwatch.Restart();
                                animationTimer.Start();
                            }
                            else
                            {
                                animationStatus.IsCompleted = true;
                                animationTimer.Stop();
                                stopwatch.Stop();

                                Animated?.Invoke(control, animationStatus);
                            }
                        }
                    }));
                };

                stopwatch.Start();
                animationTimer.Start();

            }, null, delay, Timeout.Infinite);

            return animationStatus;
        }
    }
}
