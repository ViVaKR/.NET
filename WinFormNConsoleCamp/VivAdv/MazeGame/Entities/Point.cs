using MazeGame.Libs;

namespace MazeGame.Entities
{
    public class Point
    {
        private Vector3? position;
        public Vector3? Position
        {
            get => position;
            set { position = value; }
        }

        private double thickness;
        public double Thickness
        {
            get { return thickness; }
            set { thickness = value; }
        }

        public Point()
        {
            Position = Vector3.Zero;
            Thickness = 0.0;
        }

        public Point(Vector3 position)
        {
            Position = position;
            Thickness = 0.0;
        }
    }
}
