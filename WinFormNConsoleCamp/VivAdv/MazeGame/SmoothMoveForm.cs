namespace MazeGame
{
    public partial class SmoothMoveForm : Form
    {
        public SmoothMoveForm()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            InitializeComponent();

            Width = 1024;
            Height = 1024;
        }
    }
}
