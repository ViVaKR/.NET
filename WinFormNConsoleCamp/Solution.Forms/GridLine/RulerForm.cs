
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace GridLine
{
    public partial class RulerForm : Form
    {
        private readonly PictureBox pictureBox;
        private readonly MyPanel panelTop;
        private readonly MyPanel rulerTop;
        private readonly MyPanel rulerLeft;
        private readonly Label DasEiserneKreuz;
        private readonly float gridWidth = 100f;
        private readonly float rulerWidth;
        public RulerForm()
        {
           

            InitializeComponent();

            // 폼 설정
            DoubleBuffered = true;
            Width = 2048;
            Height = 1600;
            StartPosition = FormStartPosition.CenterScreen;

            rulerWidth = gridWidth / 10.0f;

            // 상단 가로 룰러 포함 용도 패널
            Controls.Add(panelTop = new MyPanel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.Khaki,
            });

            // 외쪽 상단 교차점 여백 용 라벨
            panelTop.Controls.Add(DasEiserneKreuz = new Label
            {
                Text = "\u2720",
                Width = 80,
                Dock = DockStyle.Left,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Magenta,
                Font = new Font(Font.FontFamily, 20f, FontStyle.Bold),
                BackColor = Color.Khaki
            });
            DasEiserneKreuz.BringToFront();

            // 상단 룰러
            panelTop.Controls.Add(rulerTop = new MyPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Khaki,
                Tag = 0
            });
            rulerTop.BringToFront();
            rulerTop.Paint += Panel_Paint;
            
            // 왼쪽 룰러
            Controls.Add(rulerLeft = new MyPanel
            {
                Width = 80,
                Height = ClientRectangle.Height, 
                BackColor = Color.Khaki,
                Location = new Point(0, 80),
                Tag = 1
            });
            rulerLeft.Paint += Panel_Paint;
            panelTop.BringToFront();

            // 바디 Picture Box
            Controls.Add(pictureBox = new PictureBox
            {
                Location = new Point(80, 80),
                Width = ClientRectangle.Width,
                Height = ClientRectangle.Height,
                SizeMode = PictureBoxSizeMode.CenterImage,
                BackColor = Color.Transparent
            });

            pictureBox.Paint += PictureBox_Paint;

            // 폼 사이즈 변경시, 좌측 룰러 및 바디 PictureBox Re Drawing
            Resize += (s, e) =>
            {
                rulerLeft.Location = new Point(0, 80);
                rulerLeft.Width = 80;
                rulerLeft.Height = ClientRectangle.Height;

                pictureBox.Location = new Point(80, 80);
                pictureBox.Width = ClientRectangle.Width;
                pictureBox.Height = ClientRectangle.Height;
            };

            // 폼 로드시 사진 바인딩
            Load += (s, e) => pictureBox.Image = Image.FromFile("background-flower.jpg");
        }

        /// <summary>
        /// 상단, 왼쪽 룰러 드로우잉 (1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            if (!(sender is Panel pn)) return;

            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Pixel;
            g.SmoothingMode = SmoothingMode.None;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            switch (pn.Tag)
            {
                case 0: DrawLine(g, (pn.Width / 10.0f), pn.Height); break;
                case 1: DrawLine(g, (pn.Height / 10.0f), pn.Width, false); break;
                default: return;
            }
        }

        /// <summary>
        /// 상단, 왼쪽 룰러 드로우잉 (1)
        /// </summary>
        /// <param name="g"></param>
        /// <param name="range"></param>
        /// <param name="area"></param>
        /// <param name="tf"></param>
        private void DrawLine(Graphics g, float range, int area, bool tf = true)
        {
            int count = 0;
            for (float x = 0; x <= range; x++)
            {
                float h = count % 5 == 0 ? area / 2.0f : area * (3.0f / 4.0f);

                if (count % 10 == 0)
                {
                    if (tf)
                        g.DrawString(count.ToString(), Font, Brushes.Gray, new PointF((float)x * rulerWidth, area / 5.0f));
                    else
                        g.DrawString(count.ToString(), Font, Brushes.Gray, new PointF(area / 5.0f, (float)x * rulerWidth));
                    
                    h = area * (1.0f / 4.0f);
                }

                Pen p = new Pen(Color.Black) { Width = 1.0f };

                if (tf)
                    g.DrawLine(p, (float)x * rulerWidth, h, (float)x * rulerWidth, area);
                else
                    g.DrawLine(p, h, (float)x * rulerWidth, area, (float)x * rulerWidth);
                count++;
            }

        }

        /// <summary>
        /// PictureBox 격자 그리기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            float width = pictureBox.Width;
            float height = pictureBox.Height;
            Pen p = new Pen(Color.LightGray);

            for (float y = 0; y < height; y += gridWidth)
                e.Graphics.DrawLine(p, 0, y, width, y);

            for (float x = 0; x < width; x += gridWidth)
                e.Graphics.DrawLine(p, x, 0, x, height);

        }
    }
}
