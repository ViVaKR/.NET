using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Com.ViVaKR.Forms
{
    public class VivGroupBox : GroupBox
    {
        private string text = string.Empty;

        public new string Text
        {
            get => text;
            set
            {
                text = value;
                // Invalidate();
            }
        }

        private Color borderColor;
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                
                Invalidate(); // 페인팅 다시 하기
            }
        }

        private Size boxSize;

        public Size BoxSize
        {
            get => boxSize;
            set
            {
                boxSize = value;
                //Invalidate();
            }
        }

        /// <summary>
        /// 생성자
        /// 그룹 박스 보더 와 타이들 그리기 및
        /// 색상 폰트 등등의 속성 설정하기
        /// </summary>
        /// <param name="_text">타이틀</param>
        /// <param name="color">전반적인 색깔</param>
        /// <param name="width">박스 폭</param>
        /// <param name="height">박스 넓이</param>
        public VivGroupBox(string _text, Color color, int width, int height)
        {
            DoubleBuffered = true;

            Paint += (s, e) => DrawGroupBox(this, e.Graphics);

            Text = _text;
            BorderColor = color;
            Size = new Size(width, height);
        }

        /// <summary>
        /// 박스 및 타이틀 그리기
        /// </summary>
        /// <param name="box"></param>
        /// <param name="g"></param>
        private void DrawGroupBox(VivGroupBox box, Graphics g)
        {
            if (box == null) return;

            // 문자 그릴 브러쉬
            Brush textBrush = new SolidBrush(BorderColor);

            // 테두리 그릴 브러쉬 및 펜
            Brush borderBrush = new SolidBrush(BorderColor);
            Pen borderPen = new Pen(borderBrush, 3);

            // 타이들 폭
            SizeF strSize = g.MeasureString(Text, box.Font, boxSize);

            // 제목을 정 중앙에 모시기
            var left = (Width - strSize.Width) / 2;

            // 안티알리아싱 및 하이 컬리티 그리픽스
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            Rectangle rect = new Rectangle(box.ClientRectangle.X + 2,
                                           box.ClientRectangle.Y + (int)(strSize.Height / 2 - 1),
                                           box.ClientRectangle.Width - 4,
                                           box.ClientRectangle.Height - (int)(strSize.Height / 2) - 1);

            // Clear text and border
            g.Clear(BackColor);

            // Draw text
            g.DrawString(Text, box.Font, textBrush, left, 0);

            // Ans 박스 그리기 (문의에 대한 답변)

            // 왼쪽 선
            g.DrawLine(borderPen, rect.Location, new Point(rect.X, rect.Y + rect.Height));

            // 우측선
            g.DrawLine(borderPen, new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height));

            // 바닥선
            g.DrawLine(borderPen, new Point(rect.X, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));

            // 박스 제목이 들어갈 부분을 공백으로 모시기

            //Top1 (제목 왼쪽 까지 선)
            g.DrawLine(borderPen, new Point(rect.X, rect.Y), new Point(rect.X + (int)left, rect.Y));

            //Top2 (제목 오른쪽에서 끝까지 선)
            g.DrawLine(borderPen, new Point(rect.X + (int)left + (int)(strSize.Width) - 4, rect.Y), new Point(rect.X + rect.Width, rect.Y));

        }
    }
}
