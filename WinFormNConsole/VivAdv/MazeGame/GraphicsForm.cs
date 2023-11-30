using System.Drawing.Drawing2D;

namespace MazeGame
{
    public partial class GraphicsForm : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;
                return handleParam;
            }
        }

        private double DPI
        {
            get
            {
                using var g = CreateGraphics();
                return g.DpiX;
            }
        }

        private readonly GroupBox box;
        private readonly PictureBox pb;
        private readonly Button btnPoint;
        private readonly Button btnLine;
        private readonly Button btnCircle;
        private readonly TextBox textBox;
        private Vector3? currentPosition;
        private Vector3? firstPoint;

        private int drawIndex = -1;
        private int clickNumber = 1;
        private bool isDrawing = false;

        private readonly List<Entities.Point> points = [];
        private readonly List<Entities.Line> lines = [];
        private readonly List<Entities.Circle> circles = [];

        public GraphicsForm()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            InitializeComponent();

            WindowState = FormWindowState.Maximized;

            Controls.Add(box = new UGroupBox("Control", "Grp_Control"));
            box.Controls.Add(btnPoint = new UButton("Points", "Btn_Point", 0, DockStyle.Top, Button_Click));
            box.Controls.Add(btnLine = new UButton("Line", "Btn_Line", 1, DockStyle.Top, Button_Click));
            box.Controls.Add(btnCircle = new UButton("Circle", "Btn_Circle", 2, DockStyle.Top, Button_Click));
            box.Controls.Add(btnCircle = new UButton("\u273C", "Btn_Circle", 3, DockStyle.Top, Button_Click));
            box.Controls.Add(textBox = new UTextBox(string.Empty, "Txt_Info"));
            textBox.BringToFront();

            pb = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Beige
            };

            Controls.Add(pb);

            Text = $"Maze, Screen DPI: {DPI}";

            EventsInit();
        }

        private void EventsInit()
        {
            pb.MouseMove += PictureBox_MouseMove;
            pb.MouseDown += PictureBox_MouseDown;
            pb.Paint += PictureBox_Paint;
        }

        private float PixelToMn(float pixel) => pixel * 25.4f / (float)DPI;

        private void PictureBox_MouseDown(object? sender, MouseEventArgs e)
        {
            
            if(e.Button == MouseButtons.Right)
            {
                clickNumber = 0;
                return;
            };

            if (e.Button != MouseButtons.Left) return;
            if (!isDrawing) return;

            switch (drawIndex)
            {
                case 0: // Point
                    if (currentPosition != null)
                        points.Add(new Entities.Point(currentPosition));
                    break;

                case 1: // Line
                    {
                        switch (clickNumber)
                        {
                            case 1:
                                firstPoint = currentPosition;
                                if (currentPosition != null)
                                    points.Add(new Entities.Point(currentPosition));
                                clickNumber++;
                                break;

                            case 2:
                                lines.Add(new Entities.Line(firstPoint, currentPosition));
                                if (currentPosition != null)
                                    points.Add(new Entities.Point(currentPosition));
                                firstPoint = currentPosition;
                                break;
                        }
                    }
                    break;

                case 2: // Circle
                    switch (clickNumber)
                    {
                        case 1:
                            firstPoint = currentPosition;
                            clickNumber++;
                            break;
                        case 2:
                            if (firstPoint != null && currentPosition != null)
                            {
                                double r = firstPoint.DistanceFrom(currentPosition);
                                circles.Add(new Entities.Circle(firstPoint, r));
                                clickNumber = 1;
                            }
                            break;
                    }
                    break;
            }
            pb.Refresh();
        }

        private void PictureBox_MouseMove(object? sender, MouseEventArgs e)
        {
            currentPosition = PointToCartesian(e.Location);
            textBox.Text = $"{currentPosition.X,0:F3}{Environment.NewLine}{currentPosition.Y,0:F3}";
            pb.Refresh();
        }

        private void Button_Click(object? sender, EventArgs e)
        {
            if (sender is not Button button) return;
            int tag = Convert.ToInt32(button.Tag);
            switch (tag)
            {
                case 0:
                    {
                        drawIndex = 0;
                        isDrawing = true;
                        pb.Cursor = Cursors.Cross;
                    }
                    break;
                case 1:
                    {
                        drawIndex = 1;
                        isDrawing = true;
                        pb.Cursor = Cursors.Cross;
                    }
                    break;

                case 2:
                    {
                        drawIndex = 2;
                        isDrawing = true;
                        pb.Cursor = Cursors.Cross;
                    }
                    break;

                case 3:
                    {

                    }
                    break;
            }
        }

        private Vector3 PointToCartesian(Point point)
             => new(PixelToMn(point.X), PixelToMn(pb.Height - point.Y));


        private void PictureBox_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.SetParameters(PixelToMn(pb.Height));

            Pen pen = new(Color.Chocolate, 1.5f);
            Pen extPen = new(Color.DarkTurquoise, 1.5f);

            // Draw All Points
            if (points.Count > 0)
                foreach (Entities.Point p in points)
                    e.Graphics.DrawPoint(new Pen(Brushes.Red, 0.1f), p);

            // Draw All Lines
            if (lines != null && lines.Count > 0)
                foreach (Entities.Line line in lines)
                {
                    if (line.StartPoint == null || line.EndPoint == null) continue;
                    e.Graphics.DrawLine(pen, line);
                }

            // Draw All Circle
            if(circles.Count > 0)
            {
                foreach (Entities.Circle circle in circles)
                {
                    e.Graphics.DrawCircle(pen, circle);
                }
            }

            // Draw Line Extended
            switch (drawIndex)
            {
                case 1:
                    if (clickNumber == 2)
                    {
                        Entities.Line line = new(firstPoint, currentPosition);
                        e.Graphics.DrawLine(extPen, line);
                    }
                    break;

                case 2:
                    if (clickNumber == 2)
                    {
                        Entities.Line line = new(firstPoint, currentPosition);
                        e.Graphics.DrawLine(extPen, line);
                        double r = firstPoint.DistanceFrom(currentPosition);
                        Entities.Circle circle = new Entities.Circle(firstPoint, r);
                        e.Graphics.DrawCircle(extPen, circle);
                    }
                    break;
            }
        }
    }
}
