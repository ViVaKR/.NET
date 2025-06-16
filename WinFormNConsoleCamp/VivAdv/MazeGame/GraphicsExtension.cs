

namespace MazeGame
{
    public static class GraphicsExtension
    {
        private static float _height;
        public static void SetParameters(this Graphics _, float height)
        {
            _height = height;
        }

        public static void SetTrasnform(this Graphics g)
        {
            g.PageUnit = GraphicsUnit.Millimeter;
            g.TranslateTransform(0, _height);
            g.ScaleTransform(1.0f, -1.0f);
           
        }

        public static void DrawPoint(this Graphics g, Pen pen, Entities.Point point)
        {
            g.SetTrasnform();
            if (point.Position == null) return;

            PointF p = point.Position.ToPointF;
            g.DrawEllipse(pen, p.X - 1, p.Y - 1, 2, 2);
            g.ResetTransform();
        }

        public static void DrawLine(this Graphics g, Pen pen, Entities.Line line)
        {
            g.SetTrasnform();
            if (line.StartPoint == null || line.EndPoint == null) return;
            g.DrawLine(pen, line.StartPoint.ToPointF, line.EndPoint.ToPointF);
            g.ResetTransform();
        }

        public static void DrawCircle(this Graphics g, Pen pen, Entities.Circle circle)
        {
            if(circle.Center == null) return;

            float x = (float)(circle.Center.X - circle.Radius);
            float y = (float)(circle.Center.Y - circle.Radius);
            float d = (float)circle.Diameter;
            g.SetTrasnform();
            g.DrawEllipse(pen, x, y, d, d);
            g.ResetTransform();
        }
    }
}
