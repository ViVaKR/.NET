
using System.Windows.Forms;

namespace ZoomImage
{
    public class UPictureBox : PictureBox
    {
        public UPictureBox()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            UpdateStyles();
        }


        //protected override void OnMouseWheel(MouseEventArgs ea)
        //{
        //    //  flag = 1;
        //    // Override OnMouseWheel event, for zooming in/out with the scroll wheel
        //    if (Image != null)
        //    {
        //        // If the mouse wheel is moved forward (Zoom in)
        //        if (ea.Delta > 0)
        //        {
        //            // Check if the dimensions are in range (15 is the minimum and maximum zoom level)
        //            if ((Width < (15 * Width)) && (Height < (15 * Height)))
        //            {
        //                // Change the size of the picturebox, multiply it by the ZOOMFACTOR
        //                Width = (int)(Width * 1.25);
        //                Height = (int)(Height * 1.25);

        //                // Formula to move the picturebox, to zoom in the point selected by the mouse cursor
        //                Top = (int)(ea.Y - 1.25 * (ea.Y - Top));
        //                Left = (int)(ea.X - 1.25 * (ea.X - Left));
        //            }
        //        }
        //        else
        //        {
        //            // Check if the dimensions are in range (15 is the minimum and maximum zoom level)
        //            if ((Width > 50) && (Height >50))
        //            {
        //                // Change the size of the picturebox, divide it by the ZOOMFACTOR
        //                Width = (int)(Width / 1.25);
        //                Height = (int)(Height / 1.25);

        //                // Formula to move the picturebox, to zoom in the point selected by the mouse cursor
        //                Top = (int)(ea.Y - 0.80 * (ea.Y - Top));
        //                Left = (int)(ea.X - 0.80 * (ea.X - Left));
        //            }
        //        }
        //    }
        //}
    }
}
