using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.ViVaKR.Forms
{
    public class Rect
    {
        private Vector3 startPoint;
        public Vector3 StartPoint
        {
            get => startPoint;
            set => startPoint = value;
        }

        private Vector3 endPoint;
        public Vector3 EndPoint
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
        public Rect() : this(Vector3.Zero, Vector3.Zero)
        {

        }

        public Rect(Vector3 start, Vector3 end)
        {
            StartPoint = start;
            EndPoint = end;
            Thickness = 0.0F;
        }

    }
}
