using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace ControlsManger
{
    public partial class UniCodeForm : Form
    {
        public UniCodeForm()
        {
            InitializeComponent();

            Load += Form1_Load;
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            groupBox1.Text = string.Empty;

            // 버튼 폰트
            Font f = new("Arial", 72, FontStyle.Regular);

            // 문의하신 아이콘에 가장 근접한 유니코드
            string text = "\u00BB";

            // 그릴 아이콘의  가로 세로
            var size = 150;

            for (int i = 0; i < 4; i++) // 견본 제작용 루프
            {
                // 아이콘 을 비트맵으로 그리기
                Bitmap bmp = new(size, size);

                // 그라픽 객체를 비트맵으로 생성
                using Graphics g = Graphics.FromImage(bmp);

                // 회전을 하면서 중심점 잡아주기 로직
                g.TranslateTransform(bmp.Width / 2, bmp.Height / 2);

                // 90도 씩 회전 시키기
                g.RotateTransform((i + 1) * 90);

                // 중심점 잡기 위하여 그려진 텍스트 크기 계산
                SizeF textSize = g.MeasureString(text, f);

                // 텍스트 그릴 때 계단 현상 없애기
                g.TextRenderingHint = TextRenderingHint.AntiAlias;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // 버튼 중심에 아이콘 넣기 용
                g.DrawString(text, f, Brushes.DarkKhaki, -(textSize.Width / 2), -(textSize.Height / 2));

                // 버튼 만들기
                var btn = new Button
                {
                    Width = size * 2,
                    Height = size * 2,
                    Dock = DockStyle.Left,
                    BackgroundImage = bmp,
                    BackgroundImageLayout = ImageLayout.Center
                };

                // 보여주기 용 컨트롤 배치
                groupBox1.Controls.Add(btn);
            }
        }
    }
}
