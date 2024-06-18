

using MazeGame.Libs;

namespace MazeGame.Entities
{
    public class Line
    {
        private Vector3? startPoint;
        public Vector3? StartPoint
        {
            get { return startPoint; }
            set { startPoint = value; }
        }

        private Vector3? endPoint;
        public Vector3? EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }

        private double thickness;

        public double Thickness
        {
            get { return thickness; }
            set { thickness = value; }
        }

        public Line() : this(Vector3.Zero, Vector3.Zero) { }
        public Line(Vector3? start, Vector3? end)
        {
            StartPoint = start;
            EndPoint = end;
            Thickness = 0.0F;
        }
    }
}
