using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.ViVaKR.Forms
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


        public static void DrawRectangle(this Graphics g, Pen pen, Rect rect)
        {
            g.SetTrasnform();
            g.DrawRectangle(pen, rect);
            g.ResetTransform();
        }
    }
}
