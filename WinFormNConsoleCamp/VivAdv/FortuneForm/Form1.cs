using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FortuneForm
{
    public partial class Form1 : Form
    {
        private readonly List<string> fortunes;
        private PictureBox board;
        private Button button;
        private readonly int fortuneCount;
        private int padding;
        private Graphics g;
        private Font font;

        public Form1()
        {
            InitializeComponent();

            WindowState = FormWindowState.Maximized;
            fortuneCount = 40;
            padding = 1;
            fortunes = new List<string>(fortuneCount); // 행운목록 초기화
            FillFortunes(); // 행운 초기화
            Load += Form1_Load;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 데모 출력 용 버튼
            button = new Button
            {
                Text = "Print",
                Dock = DockStyle.Bottom,
                Height = 100,
                Font = new Font(Font.FontFamily, 24f, FontStyle.Bold),
                ForeColor = Color.Teal
            };
            button.Click += Button_Click;

            Controls.Add(button);

            // 영수증 시뮬레이터, 데모용 PicturBox
            board = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            Controls.Add(board);
            board.BringToFront();

            g = board.CreateGraphics();
            // 영수증 출력 시 안티알리아싱 텍스트 효과
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            font = new Font(Font.FontFamily, 18f, FontStyle.Regular);

        }

        /// <summary>
        /// 영수증 출력하기 위하여 버튼 클릭하는 파트 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, EventArgs e)
        {
            if (fortunes == null) return;

            // 40 개 행운 모두 소비시 다시 행운 초기화
            if (fortunes.Count() < 1)
            {
                fortunes.Clear();
                FillFortunes();
            }

            // 랜덤 뽑기
            var index = new Random().Next(fortunes.Count - 1);
            var fortune = $"영수증 번호 ({padding:0000}) -> {fortunes[index]}";
            var point = new PointF(10F, (padding++) * 25f);

            // 출력 파트
            g.DrawString(fortune, font, Brushes.DarkMagenta, point);

            // 행운 갯수 40개 내에서는 중복 방지하기
            // 출력한 것은 목록에서 삭제
            fortunes.RemoveAt(index);
        }


        /// <summary>
        /// 운세 내용 넣기
        /// 내용을...
        /// 별도의 배열에 내용을 넣고 인덱싱으로 바인딩해야 하는 파트
        /// </summary>
        private void FillFortunes()
        {
            for (int i = 0; i < fortuneCount; i++)
                fortunes.Add($"귀하의 내년 운세는 {i:000} 블라블라 가상 데이터 입니다.");
        }
    }
}
