using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Com.ViVaKR.Forms
{
    public partial class DrawForm : Form
    {
        private readonly VivPanel[,] panels;

        private Point startPos;
        private Point currentPos;
        private bool drawing;

        private VivPanel layer;
        private readonly TableLayoutPanel table;
        private readonly List<Rectangle> Rectangles;
        private readonly Button btn;
        private readonly Color orgColor;

        public DrawForm()
        {
            InitializeComponent();

            btn = new Button
            {
                Dock = DockStyle.Bottom,
                Height = 70,
                Text = "선택"
            };
            btn.Click += Button_Click;
            Controls.Add(btn);

            orgColor = Color.IndianRed;
            Rectangles = new List<Rectangle>();

            // 가로 세로 5개 테이블 레이아웃
            table = new TableLayoutPanel
            {
                ColumnCount = 5,
                RowCount = 5,
                Dock = DockStyle.Fill
            };
            Controls.Add(table);
            table.BringToFront();
            table.RowStyles.Clear();
            foreach (RowStyle rs in table.RowStyles)
                table.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            drawing = false;
            WindowState = FormWindowState.Maximized;
            panels = new VivPanel[5, 5];
            Load += DrawForm_Load;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            SetLayer(false);
        }

        private void SetLayer(bool tableVisible)
        {
            table.Visible = tableVisible;
            layer.Visible = !table.Visible;
            if (!layer.Visible)
            {
                startPos = new Point();
                currentPos = new Point();
                Rectangles.Clear();
                layer.BringToFront();
            }
            else
            {
                ResetPanelBackColor();
            }
        }

        private void ResetPanelBackColor()
        {
            for (int i = 0; i < panels.GetLength(0); i++)
            {
                for (int j = 0; j < panels.GetLength(1); j++)
                {
                    panels[i, j].BackColor = orgColor;
                }
            }
        }

        private void DrawForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < panels.GetLength(0); i++)
            {
                for (int j = 0; j < panels.GetLength(1); j++)
                {
                    panels[i, j] = new VivPanel(DockStyle.Left, $"{i}{j}", orgColor, Width / 5, Height / 5);
                    table.Controls.Add(panels[i, j], i, j);

                    panels[i, j].Controls.Add(new Label
                    {
                        Text = $"{i}-{j}",
                        Font = new Font(Font.FontFamily, 48, FontStyle.Regular),
                        AutoSize = false,
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter
                    });
                }
            }
            CreateLayer();

        }

        private void CreateLayer()
        {
            table.Visible = false;
            layer = new VivPanel(DockStyle.Fill, nameof(layer), Color.Khaki, 0, 0);
            layer.MouseDown += Panel_MouseDown;
            layer.MouseUp += Panel_MouseUp;
            layer.MouseMove += Panel_MouseMove;
            layer.Paint += Panel_Paint;
            layer.BackColor = Color.Transparent;
            Controls.Add(layer);
            layer.BringToFront();
        }

        private void Panel_MouseUp(object sender, MouseEventArgs e)
        {
            if (!(sender is VivPanel panel) || !drawing) return;

            drawing = false;
            var r = GetRectangle();

            if (r.Width > 0 && r.Height > 0) Rectangles.Add(r);
            panel.Invalidate();

            for (int i = 0; i < panels.GetLength(0); i++)
            {
                for (int j = 0; j < panels.GetLength(1); j++)
                {
                    var pn = panels[i, j]; // 각각의 패널
                    var horizontal = r.Left > pn.Right || r.Right < pn.Left;
                    var vertical = r.Top > pn.Bottom || r.Bottom < pn.Top;
                    if (!horizontal && !vertical)
                        panels[i, j].BackColor = Color.LightSkyBlue;
                }
            }
            SetLayer(true);
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (!(sender is VivPanel panel)) return;

            currentPos = e.Location;
            if (drawing) panel.Invalidate();
        }

        private Rectangle GetRectangle()
        {
            return new Rectangle(
            Math.Min(startPos.X, currentPos.X),
            Math.Min(startPos.Y, currentPos.Y),
            Math.Abs(startPos.X - currentPos.X),
            Math.Abs(startPos.Y - currentPos.Y));
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (sender is Panel panel && panel.Tag.ToString().Equals(nameof(layer)))
            {
                for (int i = 0; i < panels.GetLength(0); i++)
                {
                    for (int j = 0; j < panels.GetLength(1); j++)
                    {
                        var p = panels[i, j];
                        g.DrawRectangle(Pens.Red, new Rectangle(p.Location.X, p.Location.Y, p.Width, p.Height));
                    }
                }
                if (Rectangles.Count > 0) g.DrawRectangles(Pens.Black, Rectangles.ToArray());
                if (drawing) g.DrawRectangle(Pens.Red, GetRectangle());
            }
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (!(sender is VivPanel panel)) return;

            panel.Cursor = Cursors.Cross;
            Rectangles.Clear();
            currentPos = startPos = e.Location;
            drawing = true;
        }
    }
}
