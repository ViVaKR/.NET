using MazeGame.Libs;

namespace MazeGame.Entities
{
    public class Circle
    {
        private Vector3? vector3;
        public Vector3? Center
        {
            get { return vector3; }
            set { vector3 = value; }
        }

        private double radius;
        public double Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        private double thickness;

        public double Thickness
        {
            get { return thickness; }
            set { thickness = value; }
        }

        public double Diameter
        {
            get => Radius * 2.0;
        }
        public Circle() : this(Vector3.Zero, 1.0) { }

        public Circle(Vector3 center, double radius)
        {
            Center = center;
            Radius = radius;
            Thickness = 0.0;
        }
    }
}
