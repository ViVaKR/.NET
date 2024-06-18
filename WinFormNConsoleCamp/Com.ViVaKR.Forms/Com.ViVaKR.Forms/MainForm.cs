using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Com.ViVaKR.Forms
{
    public partial class MainForm : Form
    {
        // 사용자 그룹 박스
        private readonly VivGroupBox box;

        // 칼라 선택기
        private readonly ColorDialog colorPicker;

        // 시작 칼라
        private Color initColor;

        Timer timer;
        public MainForm()
        {
            InitializeComponent();

            timer = new Timer
            {
                Interval = 1000
            };
            timer.Tick += Timer_Tick;

            // 메인폼 기본 설정
            AutoScaleMode = AutoScaleMode.Dpi;
            Width = 1024;
            Height = 768;
            // Font = new Font(new FontFamily("IBM Plex Sans KR"), 18f, FontStyle.Regular);
            DoubleBuffered = true;

            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;

            // 컨트롤들 기본 설정
            groupBox1.ForeColor =
            button1.ForeColor =
            textBox1.ForeColor =
            initColor = Color.DarkKhaki;

            textBox1.TextAlign = HorizontalAlignment.Center;

            // 칼라 선택기 초기화
            colorPicker = new ColorDialog();

            // 그룹박스 초기화 및 기본 설정
            Controls.Add(box = new VivGroupBox(textBox1.Text = "소제목", initColor, Width / 2, Height / 2));

            box.Left = (Width - box.Width) / 2;
            box.Top = (Height - box.Height) / 2;

            textBox1.Font =
            box.Font =
            Font = new Font(new FontFamily("IBM Plex Sans KR"), 18f, FontStyle.Regular);

            // 폼 크기 변경시 그룹 박스 재 조정 (정 중앙)
            Resize += (s, e) =>
            {
                box.Left = (Width - box.Width) / 2;
                box.Top = (Height - box.Height) / 2;
            };

            // 텍스트박스 글 수정하기 위해 들어가면? 기존 글 지우기
            textBox1.Enter += (s, e) => textBox1.Text = string.Empty;

            groupBox1.Padding = new Padding(5);
            groupBox1.Height += 5 + groupBox1.Padding.Bottom;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            var target = new DateTime(2023, 9, 26, 16, 22, 15);
            var n = DateTime.Now;
            var now = new DateTime(n.Year, n.Month, n.Day, n.Hour, n.Minute, n.Second);
            Debug.WriteLine($"{now} - {target}");
            Debug.WriteLine($"{DateTime.Compare(now, target)}");
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            timer.Start();
            //var result1 = DateTime.Compare(new DateTime(2023, 1, 20, 9, 29, 0), new DateTime(2023, 1, 20, 9, 30, 0));
            //var result2 = DateTime.Compare(new DateTime(2023, 1, 20, 9, 30, 0), new DateTime(2023, 1, 20, 9, 30, 0));
            //var result3 = DateTime.Compare(new DateTime(2023, 1, 20, 9, 35, 0), new DateTime(2023, 1, 20, 9, 30, 0));

            //Debug.WriteLine($"{result1} {result2} {result3}");

            return;

            if (colorPicker.ShowDialog() == DialogResult.OK)
            {
                // 보더 및 타이들 색 변경
                groupBox1.ForeColor =
                button1.ForeColor =
                textBox1.ForeColor =
                initColor =

                box.BorderColor = colorPicker.Color;

                // 타이들 내용 변경
                box.Text = textBox1.Text;
            }
        }
    }
}
