using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace VivLogin
{
    public partial class MainForm : Form
    {
        private readonly Patient patient;

        private readonly MyDataGridView grid;

        private readonly Button button;

        public MainForm()
        {
            InitializeComponent();

            DoubleBuffered = true;

            patient = new Patient();

            Controls.Add(button = new Button
            {
                Dock = DockStyle.Bottom,
                Text = "환자 검색",
                Height = 60,
                Font = new System.Drawing.Font(Font.FontFamily, 16f, FontStyle.Bold)
            });

            // 환자 검색 시뮬레이션-> 박사랑으로 검색, 같은 이름의 환자 모두 목록화
            // 로그인시에는 유니크 해야 함으로
            // 회원 비교 처리 로직을 위해 서는
            // 데이터 베이스에서 유니크한 회원번호가 필요한 이유가 됨..
            button.Click += (s, e) =>
            {
                var persons = patient.GetPerson("박사랑");

                StringBuilder sb = new StringBuilder();

                foreach (var person in persons)
                {
                    sb.AppendLine($"{person.Name} {person.Age} {person.PersonNumber} {person.Address} {person.Reserved}");
                }

                MessageBox.Show(sb.ToString(), "환자 검색 결과");
            };

            Controls.Add(grid = new MyDataGridView());
            var textData = File.ReadAllLines("patient.txt");

            if (textData == null) return;
            grid.DataSource = patient.MakeList(textData);
            grid.ColumnHeadersHeight = 50;

            StartPosition = FormStartPosition.CenterScreen;
            Width = 1024;
            Height = 768;
        }

    }
}
