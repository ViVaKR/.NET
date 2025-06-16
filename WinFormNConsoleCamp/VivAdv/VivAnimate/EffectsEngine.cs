
namespace VivAnimate
{
    public delegate double EasingDelegate(double currentTime, double minValue, double maxValue, double duration);

    public class EffectsEngine
    {
        #region Linear
        public static double Linear(double currentTime, double minHeight, double maxHeight, double duration)
        {
            return maxHeight * currentTime / duration + minHeight;
        }

        #endregion

        #region Expo
        public static double ExpoEaseOut(double currentTime, double minHeight, double maxHeight, double duration)
        {
            return (currentTime == duration) ? minHeight + maxHeight : maxHeight * (-Math.Pow(2, -10 * currentTime / duration) + 1) + minHeight;
        }

        public static double ExpoEaseIn(double currentTime, double minHeight, double maxHeight, double duration)
        {
            return (currentTime == 0) ? minHeight : maxHeight * Math.Pow(2, 10 * (currentTime / duration - 1)) + minHeight;
        }

        public static double ExpoEaseInOut(double currentTime, double minHeight, double maxHeight, double duration)
        {
            if (currentTime == 0)
                return minHeight;

            if (currentTime == duration)
                return minHeight + maxHeight;

            if ((currentTime /= duration / 2) < 1)
                return maxHeight / 2 * Math.Pow(2, 10 * (currentTime - 1)) + minHeight;

            return maxHeight / 2 * (-Math.Pow(2, -10 * --currentTime) + 2) + minHeight;
        }

        public static double ExpoEaseOutIn(double currentTime, double minHeight, double maxHeight, double duration)
        {
            if (currentTime < duration / 2)
                return ExpoEaseOut(currentTime * 2, minHeight, maxHeight / 2, duration);

            return ExpoEaseIn((currentTime * 2) - duration, minHeight + maxHeight / 2, maxHeight / 2, duration);
        }

        #endregion

        #region Circular
        public static double CircEaseOut(double currentTime, double minHeight, double maxHeight, double duration)
        {
            return maxHeight * Math.Sqrt(1 - (currentTime = currentTime / duration - 1) * currentTime) + minHeight;
        }

        public static double CircEaseIn(double currentTime, double minHeight, double maxHeight, double duration)
        {
            var sqrt = Math.Sqrt(1 - (currentTime /= duration) * currentTime);
            if (double.IsNaN(sqrt)) sqrt = 0;
            return -maxHeight * (sqrt - 1) + minHeight;
        }

        public static double CircEaseInOut(double currentTime, double minHeight, double maxHeight, double duration)
        {
            if ((currentTime /= duration / 2) < 1)
                return -maxHeight / 2 * (Math.Sqrt(1 - currentTime * currentTime) - 1) + minHeight;

            return maxHeight / 2 * (Math.Sqrt(1 - (currentTime -= 2) * currentTime) + 1) + minHeight;
        }

        public static double CircEaseOutIn(double currentTime, double minHeight, double maxHeight, double duration)
        {
            if (currentTime < duration / 2)
                return CircEaseOut(currentTime * 2, minHeight, maxHeight / 2, duration);

            return CircEaseIn((currentTime * 2) - duration, minHeight + maxHeight / 2, maxHeight / 2, duration);
        }

        #endregion

        #region Quad
        public static double QuadEaseOut(double currentTime, double minHeight, double maxHeight, double duration)
        {
            return -maxHeight * (currentTime /= duration) * (currentTime - 2) + minHeight;
        }
        public static double QuadEaseIn(double currentTime, double minHeight, double maxHeight, double duration)
        {
            return maxHeight * (currentTime /= duration) * currentTime + minHeight;
        }

        public static double QuadEaseInOut(double currentTime, double minHeight, double maxHeight, double duration)
        {
            if ((currentTime /= duration / 2) < 1)
                return maxHeight / 2 * currentTime * currentTime + minHeight;

            return -maxHeight / 2 * ((--currentTime) * (currentTime - 2) - 1) + minHeight;
        }

        public static double QuadEaseOutIn(double currentTime, double minHeight, double maxHeight, double duration)
        {
            if (currentTime < duration / 2)
                return QuadEaseOut(currentTime * 2, minHeight, maxHeight / 2, duration);

            return QuadEaseIn((currentTime * 2) - duration, minHeight + maxHeight / 2, maxHeight / 2, duration);
        }

        #endregion

        #region Sine

        public static double SineEaseOut(double currentTime, double minHeight, double maxHeight, double duration)
        {
            return maxHeight * Math.Sin(currentTime / duration * (Math.PI / 2)) + minHeight;
        }

        public static double SineEaseIn(double currentTime, double minHeight, double maxHeight, double duration)
        {
            return -maxHeight * Math.Cos(currentTime / duration * (Math.PI / 2)) + maxHeight + minHeight;
        }

        public static double SineEaseInOut(double currentTime, double minHeight, double maxHeight, double duration)
        {
            if ((currentTime /= duration / 2) < 1)
                return maxHeight / 2 * (Math.Sin(Math.PI * currentTime / 2)) + minHeight;

            return -maxHeight / 2 * (Math.Cos(Math.PI * --currentTime / 2) - 2) + minHeight;
        }

        public static double SineEaseOutIn(double currentTime, double minHeight, double maxHeight, double duration)
        {
            if (currentTime < duration / 2)
                return SineEaseOut(currentTime * 2, minHeight, maxHeight / 2, duration);

            return SineEaseIn((currentTime * 2) - duration, minHeight + maxHeight / 2, maxHeight / 2, duration);
        }

        #endregion

        #region Cubic

        public static double CubicEaseOut(double currentTime, double minHeight, double maxHeight, double duration)
        {
            return maxHeight * ((currentTime = currentTime / duration - 1) * currentTime * currentTime + 1) + minHeight;
        }

        public static double CubicEaseIn(double currentTime, double minHeight, double maxHeight, double duration)
        {
            return maxHeight * (currentTime /= duration) * currentTime * currentTime + minHeight;
        }

        public static double CubicEaseInOut(double currentTime, double minHeight, double maxHeight, double duration)
        {
            if ((currentTime /= duration / 2) < 1)
                return maxHeight / 2 * currentTime * currentTime * currentTime + minHeight;

            return maxHeight / 2 * ((currentTime -= 2) * currentTime * currentTime + 2) + minHeight;
        }


        public static double CubicEaseOutIn(double currentTime, double minHeight, double maxHeight, double duration)
        {
            if (currentTime < duration / 2)
                return CubicEaseOut(currentTime * 2, minHeight, maxHeight / 2, duration);

            return CubicEaseIn((currentTime * 2) - duration, minHeight + maxHeight / 2, maxHeight / 2, duration);
        }

        #endregion

        #region Quartic

        public static double QuartEaseOut(double currentTime, double minHeight, double maxHeight, double duration)
        {
            return -maxHeight * ((currentTime = currentTime / duration - 1) * currentTime * currentTime * currentTime - 1) + minHeight;
        }

        public static double QuartEaseIn(double currentTime, double minHeight, double maxHeight, double duration)
        {
            return maxHeight * (currentTime /= duration) * currentTime * currentTime * currentTime + minHeight;
        }

        public static double QuartEaseInOut(double currentTime, double minHeight, double maxHeight, double duration)
        {
            if ((currentTime /= duration / 2) < 1)
                return maxHeight / 2 * currentTime * currentTime * currentTime * currentTime + minHeight;

            return -maxHeight / 2 * ((currentTime -= 2) * currentTime * currentTime * currentTime - 2) + minHeight;
        }


        public static double QuartEaseOutIn(double currentTime, double minHeight, double maxHeight, double duration)
        {
            if (currentTime < duration / 2)
                return QuartEaseOut(currentTime * 2, minHeight, maxHeight / 2, duration);

            return QuartEaseIn((currentTime * 2) - duration, minHeight + maxHeight / 2, maxHeight / 2, duration);
        }

        #endregion

        #region Quintic

        public static double QuintEaseOut(double currentTime, double minHeight, double maxHeight, double duration)
        {
            return maxHeight * ((currentTime = currentTime / duration - 1) * currentTime * currentTime * currentTime * currentTime + 1) + minHeight;
        }

        public static double QuintEaseIn(double currentTime, double minHeight, double maxHeight, double duration)
        {
            return maxHeight * (currentTime /= duration) * currentTime * currentTime * currentTime * currentTime + minHeight;
        }


        public static double QuintEaseInOut(double currentTime, double minHeight, double maxHeight, double duration)
        {
            if ((currentTime /= duration / 2) < 1)
                return maxHeight / 2 * currentTime * currentTime * currentTime * currentTime * currentTime + minHeight;
            return maxHeight / 2 * ((currentTime -= 2) * currentTime * currentTime * currentTime * currentTime + 2) + minHeight;
        }


        public static double QuintEaseOutIn(double currentTime, double minHeight, double maxHeight, double duration)
        {
            if (currentTime < duration / 2)
                return QuintEaseOut(currentTime * 2, minHeight, maxHeight / 2, duration);
            return QuintEaseIn((currentTime * 2) - duration, minHeight + maxHeight / 2, maxHeight / 2, duration);
        }

        #endregion

        #region Elastic
        public static double ElasticEaseOut(double currentTime, double minHeight, double maxHeight, double duration)
        {
            if ((currentTime /= duration) == 1) return minHeight + maxHeight;

            double p = duration * 0.3;
            double s = p / 4;

            return (maxHeight * Math.Pow(2, -10 * currentTime) * Math.Sin((currentTime * duration - s) * (2 * Math.PI) / p) + maxHeight + minHeight);
        }


        public static double ElasticEaseIn(double currentTime, double minHeight, double maxHeight, double duration)
        {
            if ((currentTime /= duration) == 1)
                return minHeight + maxHeight;

            double p = duration * .3;
            double s = p / 4;

            return -(maxHeight * Math.Pow(2, 10 * (currentTime -= 1)) * Math.Sin((currentTime * duration - s) * (2 * Math.PI) / p)) + minHeight;
        }

        public static double ElasticEaseInOut(double currentTime, double minHeight, double maxHeight, double duration)
        {
            if ((currentTime /= duration / 2) == 2)
                return minHeight + maxHeight;

            double p = duration * (.3 * 1.5);
            double s = p / 4;

            if (currentTime < 1)
                return -.5 * (maxHeight * Math.Pow(2, 10 * (currentTime -= 1)) * Math.Sin((currentTime * duration - s) * (2 * Math.PI) / p)) + minHeight;
            return maxHeight * Math.Pow(2, -10 * (currentTime -= 1)) * Math.Sin((currentTime * duration - s) * (2 * Math.PI) / p) * .5 + maxHeight + minHeight;
        }


        public static double ElasticEaseOutIn(double currentTime, double minHeight, double maxHeight, double duration)
        {
            if (currentTime < duration / 2)
                return ElasticEaseOut(currentTime * 2, minHeight, maxHeight / 2, duration);
            return ElasticEaseIn((currentTime * 2) - duration, minHeight + maxHeight / 2, maxHeight / 2, duration);
        }

        #endregion

        #region Bounce

        /// <summary>
        /// 바운스 완화 함수
        /// 지수적으로 감소하는 포물선 바운스
        /// 속도 0 에서 감속합니다.
        /// </summary>
        public static double BounceEaseOut(double currentTime, double minHeight, double maxHeight, double duration)
        {
            var prac = Math.E; // 자연상수 2.75
            if ((currentTime /= duration) < (1 / prac))
                return maxHeight * (7.5625 * currentTime * currentTime) + minHeight;

            if (currentTime < (2 / prac))
                return maxHeight * (7.5625 * (currentTime -= (1.5 / prac)) * currentTime + .75) + minHeight;

            if (currentTime < (2.5 / prac))
                return maxHeight * (7.5625 * (currentTime -= (2.25 / prac)) * currentTime + .9375) + minHeight;

            return maxHeight * (7.5625 * (currentTime -= (2.625 / prac)) * currentTime + .984375) + minHeight;
        }

        public static double BounceEaseIn(double currentTime, double minHeight, double maxHeight, double duration)
        {
            return maxHeight - BounceEaseOut(duration - currentTime, 0, maxHeight, duration) + minHeight;
        }

        public static double BounceEaseInOut(double currentTime, double minHeight, double maxHeight, double duration)
        {
            if (currentTime < duration / 2)
                return BounceEaseIn(currentTime * 2, 0, maxHeight, duration) * .5 + minHeight;

            return BounceEaseOut(currentTime * 2 - duration, 0, maxHeight, duration) * .5 + maxHeight * .5 + minHeight;
        }

        public static double BounceEaseOutIn(double currentTime, double minHeight, double maxHeight, double duration)
        {
            if (currentTime < duration / 2)
                return BounceEaseOut(currentTime * 2, minHeight, maxHeight / 2, duration);

            return BounceEaseIn((currentTime * 2) - duration, minHeight + maxHeight / 2, maxHeight / 2, duration);
        }

        #endregion

        #region Back
        public static double BackEaseOut(double currentTime, double minHeight, double maxHeight, double duration)
        {
            return maxHeight * ((currentTime = currentTime / duration - 1) * currentTime * ((1.70158 + 1) * currentTime + 1.70158) + 1) + minHeight;
        }

        public static double BackEaseIn(double currentTime, double minHeight, double maxHeight, double duration)
        {
            return maxHeight * (currentTime /= duration) * currentTime * ((1.70158 + 1) * currentTime - 1.70158) + minHeight;
        }

        public static double BackEaseInOut(double currentTime, double minHeight, double maxHeight, double duration)
        {
            double s = 1.70158;
            if ((currentTime /= duration / 2) < 1)
                return maxHeight / 2 * (currentTime * currentTime * (((s *= (1.525)) + 1) * currentTime - s)) + minHeight;
            return maxHeight / 2 * ((currentTime -= 2) * currentTime * (((s *= (1.525)) + 1) * currentTime + s) + 2) + minHeight;
        }

        public static double BackEaseOutIn(double currentTime, double minHeight, double maxHeight, double duration)
        {
            if (currentTime < duration / 2)
                return BackEaseOut(currentTime * 2, minHeight, maxHeight / 2, duration);
            return BackEaseIn((currentTime * 2) - duration, minHeight + maxHeight / 2, maxHeight / 2, duration);
        }

        #endregion
    }
}
