using System.Diagnostics;
namespace VivAnimate.Animators
{
    public class AnimationStatus(CancellationTokenSource token, Stopwatch stopwatch) : EventArgs
    {
        private readonly Stopwatch _stopwatch = stopwatch;

        public long ElapsedMilliseconds
        {
            get => _stopwatch.ElapsedMilliseconds;
        }

        public bool IsCompleted { get; set; } = false;

        public CancellationTokenSource CancellationToken { get; private set; } = token;

        public long ElapseMilliseconds
        {
            get => _stopwatch.ElapsedMilliseconds;
        }
    }
}
