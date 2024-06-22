using MazeGame.UControls;

namespace MazeGame
{
    public partial class MovementForm : Form
    {

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }

        public MovementForm()
        {

            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);
            InitializeComponent();

            WindowState = FormWindowState.Normal;
            StartPosition = FormStartPosition.CenterScreen;
            Width = 2000;
            Height = 1500;

            Controls.Add(new UvPanel());
        }
    }
}
