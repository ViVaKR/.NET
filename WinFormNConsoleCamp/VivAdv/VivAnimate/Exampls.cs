using VivAnimate.Animators;
using VivAnimate.Effects.Bounds;
using VivAnimate.Extensions;

namespace VivAnimate
{
    public class Exampls(Control control)
    {
        private readonly List<AnimationStatus> _cancellationTokens = [];

        public Control Control { get; private set; } = control;
        public Size MaxSize { get; set; } = control.Size;
        public Size MinSize { get; set; } = control.MinimumSize;
        public int Duration { get; set; } = 3000;
        public int Delay { get; set; } = 0;

        public void Show()
        {
            int duration = Duration;
            if (_cancellationTokens.Any(animation => !animation.IsCompleted))
            {
                var token = _cancellationTokens.First(animation => !animation.IsCompleted);
                duration = (int)(token.ElapsedMilliseconds);
            }
            Cancel();

            var cT1 = Control.Animate(new ToCenterFoldEffect(), EffectsEngine.CircEaseIn, MaxSize.Height, duration, Delay);
            var cT2 = Control.Animate(new VerticalFoldEffect(), EffectsEngine.CircEaseOut, MaxSize.Width, duration, Delay);

            _cancellationTokens.Add(cT1);
            _cancellationTokens.Add(cT2);
        }

        public void Hide()
        {
            int duration = Duration;

            if (_cancellationTokens.Any(animation => !animation.IsCompleted))
            {
                var token = _cancellationTokens.First(animation => !animation.IsCompleted);
                duration = (int)(token.ElapsedMilliseconds);
            }
            Cancel();

            var cT1 = Control.Animate(new ToCenterFoldEffect(),
                EffectsEngine.CircEaseOut, MinSize.Height, duration, Delay);

            var cT2 = Control.Animate(new VerticalFoldEffect(),
                EffectsEngine.CircEaseIn, MinSize.Width, duration, Delay);

            _cancellationTokens.Add(cT1);
            _cancellationTokens.Add(cT2);
        }

        public void Cancel()
        {
            foreach (var token in _cancellationTokens)
                token.CancellationToken.Cancel();

            _cancellationTokens.Clear();
        }
    }
}
