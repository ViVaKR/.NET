namespace MazeGame.Libs
{
    public class Vector3
    {
        private double x;
        public double X { get { return x; } set { x = value; } }

        private double y;
        public double Y { get { return y; } set { y = value; } }

        private double z;
        public double Z { get { return z; } set { z = value; } }

        public Vector3(double x, double y)
        {
            X = x;
            Y = y;
            Z = 0.0;
        }

        public Vector3(double x, double y, double z) : this(x, y) => Z = z;


        public static Vector3 Zero
        {
            get => new(0.0, 0.0, 0.0);
        }

        public PointF ToPointF
        {
            get => new((float)X, (float)Y);
        }

        /// <summary>
        /// 피타고라스 정리
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public double DistanceFrom(Vector3 v)
        {
            double dx = v.X - X;
            double dy = v.Y - Y;
            double dz = v.Z - Z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
    }
}
