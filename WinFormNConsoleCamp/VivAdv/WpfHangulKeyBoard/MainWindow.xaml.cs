
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace WpfHangulKeyBoard
{
    public partial class MainWindow : Window
    {
        private readonly List<string> charList = [];
        private readonly string first = "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ";
        private readonly string middle = "ㅏㅐㅑㅒㅓㅔㅕㅖㅗㅘㅙㅚㅛㅜㅝㅞㅟㅠㅡㅢㅣ";
        private readonly string last = " ㄱㄲㄳㄴㄵㄶㄷㄹㄺㄻㄼㄽㄾㄿㅀㅁㅂㅄㅅㅆㅇㅈㅊㅋㅌㅍㅎ";
        private readonly uint codeBase = 0xAC00;
        private readonly IEnumerable<Button> buttons;

        public MainWindow()
        {
            InitializeComponent();

            charList = [];

            buttons = Grid_Main.Children.OfType<Button>();

            foreach (var button in buttons)
            {
                if (button.Tag == null) continue;

                button.Content = button.Tag.ToString();
            }

        }


        private void BuildCharater(uint? c)
        {
            if (c == null) return;
            TbResult.Text += ((char)c).ToString();

        }

        private readonly Dictionary<int, char> letters = new(4);
        private readonly Dictionary<int, string> text = [];
        public void CreateHangul()
        {
            int idx_0 = first.IndexOf(letters[0]);

            int idx_1 = middle.IndexOf(letters[1]);

            int idx_2 = last.IndexOf(letters[2]);

            // 모음의 갯수가 21 개이므로
            // 초성에 21 개를 곱하여 모든 초성을 바탕으로
            // 중성을의 인덱스를 더한후
            // 마지막으로 종성의 갯수(공백 포함 28개)을 사이클링 한 후
            // 종성 인덱스를 더하면 
            // 완성형 문자를 조합 할 수 있음..
            long unicode = codeBase + (idx_0 * 21 + idx_1) * 28 + idx_2;

            char hangul = Convert.ToChar(unicode);

            text.Add(text.Count, hangul.ToString());
            TbResult.Text = $"{string.Join(string.Empty, text.Values)}";
            letters.Clear();

        }


        private void Button_Click(object? sender, RoutedEventArgs e)
        {
            if (sender is not Button btn) return;

            char tag = btn.Tag != null ? Convert.ToChar(btn.Tag) : (char)0x0020;

            letters.Add(letters.Count, tag);

            switch (letters.Count)
            {
                case 0:
                    {
                    }
                    break;
                case 1:
                    {
                    }
                    break;
                case 2:
                    {
                    }
                    break;
                case 3:
                    {
                    }
                    break;
                case 4:
                    {
                        var c = letters.TakeLast(2);
                        var str = string.Join(string.Empty, c.Select(x => x.Value));
                        var last = Enum.TryParse(str.ToString(), out LastLetter l);
                    }
                    break;
                default: break;
            }

            CreateHangul();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            letters.Clear();
            TbResult.Text = string.Empty;
        }
    }
}