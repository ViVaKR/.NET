using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Com.ViVaKR.Forms
{
    public partial class CalcForm : Form
    {
        public CalcForm()
        {
            InitializeComponent();
            Load += CalcForm_Load;
            buttons = new List<Button>();
        }

        // 계산 식 목록
        private readonly List<char> expr = new List<char>();

        // 메모리 목록
        private readonly List<string> memory = new List<string>();

        // 버튼 목록
        private readonly List<Button> buttons;

        // 폼 로드 이벤트
        private void CalcForm_Load(object sender, EventArgs e)
        {
            // 테이블 패널 (5 * 5) 안에 버튼 배치된 것을 버튼 목록화
            buttons.AddRange(TblPanel.Controls.OfType<Button>());

            foreach (Button button in buttons)
            {
                if (string.IsNullOrEmpty(button.Text)) continue;

                // 버튼 이름, 태그 를 버튼 텍스트로 지정
                button.Name = (button.Tag = button.Text).ToString();
                button.Font = new System.Drawing.Font(Font.FontFamily, 48, System.Drawing.FontStyle.Bold);

                // 모든 버튼 클릭 이벤트를 하나로 모음
                button.Click += Button_Click;
            }
        }

        /// <summary>
        /// 버튼의 태그로 각각의 역할 분장 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, EventArgs e)
        {
            if (!(sender is Button btn)) return;

            if (label1.Text.Equals("0")) // 0 만 있으면 지우기
                label1.Text = string.Empty;

            switch (btn.Tag.ToString())
            {
                case "C": // 문자 하나 지우기
                    expr.RemoveAt(expr.Count() - 1);
                    break;

                case "AC": // 모두 지우기
                    expr.Clear();
                    label1.Text = 0.ToString();
                    return;

                case "M": // 메모리 에 임시 저장하기
                    memory.Add(string.Join(string.Empty, expr));
                    MessageBox.Show($"기억됨 {string.Join(string.Empty, memory)}");
                    break;

                case "=": // 는이면 계산하기
                    DataTable dt = new DataTable();
                    var result = dt.Compute(string.Join(string.Empty, expr), string.Empty);
                    label1.Text = $"{string.Join(string.Empty, expr)}={result}";
                    return;

                default: // 숫자 버튼과 오퍼레이팅 버튼을 순서대로 저장하기
                    expr.Add(Convert.ToChar(btn.Tag));
                    break;
            }

            // 저장 된 것을 표시하기
            label1.Text = string.Join(string.Empty, expr);
        }
    }
}