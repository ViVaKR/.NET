using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FormControls
{
    public partial class MainForm : Form
    {
        private Panel panel;
        private DataGridView grid;
        private readonly List<Student> students;

        public MainForm()
        {
            InitializeComponent();

            DoubleBuffered = true;

            Width = 2048;
            Height = 1600;
            StartPosition = FormStartPosition.CenterScreen;
            students = new List<Student>();

            Load += (s, e) =>
            {
                Controls.Add(panel = new Panel
                {
                    Dock = DockStyle.Fill,
                    Width = 1024,
                    Height = 1024,
                    BackColor = Color.DarkOrchid,
                    Padding = new Padding(30),
                    BorderStyle = BorderStyle.FixedSingle
                    
                });

                panel.Controls.Add(grid = new VivGrid
                {
                    Dock=  DockStyle.Fill,
                    ColumnHeadersHeight = 24,
                    Font = new Font(Font.FontFamily, 13f, FontStyle.Regular),
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                });

                SeedData(); // 시드 데이터
                grid.AutoResizeColumnHeadersHeight();
            };
        }

        private void SeedData()
        {
            students.AddRange(new[]
            {
                new Student ( 1, "장길산", 89),
                new Student ( 2, "임꺽정", 75),
                new Student ( 3, "두란이", 93),
                new Student ( 4, "오묘한", 69),
            });

            grid.DataSource = students;
        }
    }
}
