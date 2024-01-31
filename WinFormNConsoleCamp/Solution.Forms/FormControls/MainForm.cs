
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
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

                // 데이터 그리드 뷰 컨트롤을 이용한 
                // 데이터바인딩시 속성 활용 데모
                // 문의에 대한 답변 파트
                panel.Controls.Add(grid = new DataGridView
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
