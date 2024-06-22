
using System.Windows.Forms;

namespace GridLine
{
    public  class MyPictureBox : PictureBox
    {
        public MyPictureBox()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }
    }
}
