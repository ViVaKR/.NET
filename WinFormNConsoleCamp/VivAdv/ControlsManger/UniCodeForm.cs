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

            // ��ư ��Ʈ
            Font f = new("Arial", 72, FontStyle.Regular);

            // �����Ͻ� �����ܿ� ���� ������ �����ڵ�
            string text = "\u00BB";

            // �׸� ��������  ���� ����
            var size = 150;

            for (int i = 0; i < 4; i++) // �ߺ� ���ۿ� ����
            {
                // ������ �� ��Ʈ������ �׸���
                Bitmap bmp = new(size, size);

                // �׶��� ��ü�� ��Ʈ������ ����
                using Graphics g = Graphics.FromImage(bmp);

                // ȸ���� �ϸ鼭 �߽��� ����ֱ� ����
                g.TranslateTransform(bmp.Width / 2, bmp.Height / 2);

                // 90�� �� ȸ�� ��Ű��
                g.RotateTransform((i + 1) * 90);

                // �߽��� ��� ���Ͽ� �׷��� �ؽ�Ʈ ũ�� ���
                SizeF textSize = g.MeasureString(text, f);

                // �ؽ�Ʈ �׸� �� ��� ���� ���ֱ�
                g.TextRenderingHint = TextRenderingHint.AntiAlias;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // ��ư �߽ɿ� ������ �ֱ� ��
                g.DrawString(text, f, Brushes.DarkKhaki, -(textSize.Width / 2), -(textSize.Height / 2));

                // ��ư �����
                var btn = new Button
                {
                    Width = size * 2,
                    Height = size * 2,
                    Dock = DockStyle.Left,
                    BackgroundImage = bmp,
                    BackgroundImageLayout = ImageLayout.Center
                };

                // �����ֱ� �� ��Ʈ�� ��ġ
                groupBox1.Controls.Add(btn);
            }
        }
    }
}
