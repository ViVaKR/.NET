
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GridLine
{
    public partial class MainForm : Form
    {
        private readonly MyPanel panel;
        private readonly MyPanel panelLeft;
        private readonly MyPanel panelTop;
        private readonly MyPictureBox pictureBox1;
        private readonly Label label;
        private readonly int rulerWidth = 100;
        private readonly int step = 1;
        private readonly int small = 5;
        private readonly int big = 10;
        private readonly int number = 10;
        private readonly float stroke = 2.5f;

        public MainForm()
        {
            DoubleBuffered = true;

            InitializeComponent();

            Controls.Add(panel = new MyPanel
            {
                Dock = DockStyle.Top,
                Height = rulerWidth,
                BackColor = Color.Transparent
            });

            // Label
            panel.Controls.Add(label = new Label
            {
                AutoSize = false,
                Text = "+",
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Khaki,
                ForeColor = Color.Magenta,
                Dock = DockStyle.Left,
                Padding = new Padding(10)
            });
            label.SendToBack();

            // Top Ruler
            panel.Controls.Add(panelTop = new MyPanel
            {
                Location = new Point(rulerWidth, 0),
                Height = rulerWidth,
                Width = ClientRectangle.Width - rulerWidth,
                BackColor = Color.Khaki
            });
            panelTop.Paint += PanelTop_Paint;

            // Left Ruler
            Controls.Add(panelLeft = new MyPanel
            {
                Location = new Point(0, rulerWidth),
                Width = rulerWidth,
                Height = ClientRectangle.Height - rulerWidth,
                BackColor = Color.Khaki
            });
            panelLeft.Paint += PanelLeft_Paint;

            // PictureBox
            Controls.Add(pictureBox1 = new MyPictureBox
            {
                Location = new Point(rulerWidth, rulerWidth),
                Width = ClientRectangle.Width - rulerWidth,
                Height = ClientRectangle.Height - rulerWidth,
                Image = Image.FromFile("background-flower.jpg"),
                SizeMode = PictureBoxSizeMode.Zoom
            });

            Resize += MainForm_Resize;

            WindowState = FormWindowState.Maximized;

            Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "boom.png");

            FileInfo fi = new FileInfo(path);

            if (!fi.Exists)
            {
                Debug.WriteLine("파일이 없습니다.");
            }
        }

        private void MainForm_Resize(object sender, System.EventArgs e)
        {
            label.Width = rulerWidth;
            label.Height = rulerWidth;
            panelTop.Height = rulerWidth;
            panelTop.Width = ClientRectangle.Width - rulerWidth;
            panelLeft.Width = rulerWidth;
            panelLeft.Height = ClientRectangle.Height - rulerWidth;
            pictureBox1.Width = ClientRectangle.Width - rulerWidth;
            pictureBox1.Height = ClientRectangle.Height - rulerWidth;
        }

        /// <summary>
        /// Ruler LEFT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanelLeft_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Millimeter;
            int length = (panelLeft.Height - rulerWidth) / 3;

            for (int i = 0; i < length; i += step)
            {
                float d = 1;
                if (i % small == 0)
                    d = (i % big == 0) ? 3 : 2;

                Pen pen = new Pen(Color.Black, 0.25f);
                StringFormat format = StringFormat.GenericTypographic;

                g.DrawLine(pen, 0f, i, d * stroke, i);

                if (i == 0) continue;
                if ((i % number) == 0)
                {
                    string text = (i / number).ToString();
                    SizeF size = g.MeasureString(text, Font, length, format);
                    g.DrawString(text, Font, Brushes.Black, d * stroke, i - (size.Height / 2), format);
                }
            }
        }

        /// <summary>
        /// Ruler TOP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanelTop_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Millimeter;
            int length = (panelTop.Width - rulerWidth) / 3;

            for (int i = 0; i < length; i += step)
            {
                float d = 1;
                if (i % small == 0)
                    d = (i % big == 0) ? 3 : 2;

                Pen pen = new Pen(Brushes.Black, 0.25f);
                StringFormat format = StringFormat.GenericTypographic;
                g.DrawLine(pen, i, 0f, i, d * stroke);

                if (i == 0) continue;
                if ((i % number) == 0)
                {
                    string text = (i / number).ToString();
                    SizeF size = g.MeasureString(text, Font, length, format);
                    g.DrawString(text, Font, Brushes.Black, i - (size.Width / 2), d * stroke, format);
                }
            }

        }
    }
}
