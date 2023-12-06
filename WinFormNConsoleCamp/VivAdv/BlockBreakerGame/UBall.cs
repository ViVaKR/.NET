
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BlockBreakerGame
{
    public class UBall : Panel
    {
        public UBall(UPanel ground)
        {
            DoubleBuffered = true;
            Width = 25;
            Height = 25;
            BackColor = Color.Tomato;

            //var timer = new Timer
            //{
            //    Interval = 20
            //};

            //timer.Tick += Timer_Tick;
            // timer.Start();
        }

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            Graphics g = CreateGraphics();


            // Set world transform of graphics object to rotate.
            g.RotateTransform(30.0F);

            // Then to translate, appending to world transform.
            g.TranslateTransform(100.0F, 0.0F, MatrixOrder.Append);

            // Draw rotated, translated ellipse to screen.
            g.FillEllipse(Brushes.White, 5, 5, 30, 30);
        }


    }
}
