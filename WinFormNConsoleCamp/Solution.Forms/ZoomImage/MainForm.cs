using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ZoomImage
{
    public partial class MainForm : Form
    {
        private readonly double ratio = 1.1;      // 축소 확대 비율 : 10% 
        private readonly int zoomRate = 5;        // 최대 원본 비율 : 5
        private int imageWidth;
        private int imageHeight;

        private Point mousePoint;
        private bool isDragging = false;

        private readonly UPictureBox pictureBox;
        private readonly UPanel panel;

        public MainForm()
        {
            InitializeComponent();

            #region 메인폼 기본설정
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            StartPosition = FormStartPosition.CenterScreen;

            #endregion

            Controls.Add(panel = new UPanel { Dock = DockStyle.Fill });
            mousePoint = new Point(Cursor.Position.X, Cursor.Position.Y);

            panel.Controls.Add(pictureBox = new UPictureBox
            {
                Image = Image.FromFile("background-flower.jpg"),
                SizeMode = PictureBoxSizeMode.StretchImage
            });

            #region Activator
            //// 데이터 베이스로 부터 받아온 유저 컨트롤 이름과 동일한 문자열
            //string name = "UserControl1";

            //// 버전 정보 뺀 순수 어셈블리 이름 추출
            //string assemblyName = Assembly.GetCallingAssembly().GetName().Name;

            //// string -> Type 정보 가져오기
            //Type type = Type.GetType($"{assemblyName}.{name}");

            //if (type != null)
            //{
            //    // Type 으로 인스턴스 만들기
            //    var instance = Activator.CreateInstance(type) as Control;

            //    // Form 에 컨트롤 바인딩하기
            //    instance.Dock = DockStyle.Fill;
            //    Controls.Add(instance);

            //    instance.Controls.OfType<Button>().FirstOrDefault().Text = Assembly.GetCallingAssembly().GetName().FullName;
            //} 
            #endregion


            Resize += (s, e) =>
            {
                imageWidth = Width / 2;
                imageHeight = Height / 2;

                pictureBox.Width = imageWidth;
                pictureBox.Height = imageHeight;

                int left = Convert.ToInt32((panel.Width - imageWidth) / 2);
                int top = Convert.ToInt32((panel.Height - imageHeight) / 2);

                pictureBox.Location = new Point(left, top);
            };

            panel.MouseWheel += (s, e) => pictureBox.Location = Zoom(e.Delta < 0);

            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseUp += PictureBox_MouseUp;
            WindowState = FormWindowState.Maximized;

        }

        private Point Zoom(bool inOut)
        {
            switch (inOut)
            {
                case true:
                    if (pictureBox.Width <= panel.Width * zoomRate)
                        pictureBox.Size = ReSetSize(pictureBox.Width * ratio, pictureBox.Height * ratio);
                    break;

                case false:
                    if (pictureBox.Width >= imageWidth / zoomRate)
                        pictureBox.Size = ReSetSize(pictureBox.Width / ratio, pictureBox.Height / ratio);

                    break;
            }

            return ResetLocation(pictureBox.Width, pictureBox.Height);

        }

        private Size ReSetSize(double Width, double height)
            => new Size(Convert.ToInt32(Width), Convert.ToInt32(height));

        private Point ResetLocation(int w, int h)
        {

            int offsetX = panel.Width / 2 - Cursor.Position.X;
            int offsetY = panel.Height / 2 - Cursor.Position.Y;

            int left = Convert.ToInt32((panel.Width - w) / 2 - offsetX);
            int top = Convert.ToInt32((panel.Height - h) / 2 - offsetY);

            return new Point(left, top);
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
            => mousePoint = (isDragging = e.Button == MouseButtons.Left) ? e.Location : new Point();

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                PictureBox pictureBox = (PictureBox)sender;
                pictureBox.Left += e.X - mousePoint.X;
                pictureBox.Top += e.Y - mousePoint.Y;
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
            => isDragging = e.Button != MouseButtons.Left;
    }
}
