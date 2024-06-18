
using System.Text;
using System.Windows;
using System.Windows.Controls;


namespace WpfHangul
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // 초성, 중성, 종성 테이블.
        private readonly string firstLetters = "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ";
        private readonly string middleLetters = "ㅏㅐㅑㅒㅓㅔㅕㅖㅗㅘㅙㅚㅛㅜㅝㅞㅟㅠㅡㅢㅣ";
        private readonly string lastLetters = " ㄱㄲㄳㄴㄵㄶㄷㄹㄺㄻㄼㄽㄾㄿㅀㅁㅂㅄㅅㅆㅇㅈㅊㅋㅌㅍㅎ";
        private readonly ushort hangulUniCodeBase = 0xAC00;
        // private readonly ushort hangulUniCodeLast = 0xD79F;

        public string CreateHangul(string? first, string? middle, string? last = null)
        {
            int idxFirst = default;
            int idxMiddle = default;
            int idxLast = default;

            int unicode;

            if (!string.IsNullOrEmpty(first))
                idxFirst = firstLetters.IndexOf(first);

            if (!string.IsNullOrEmpty(middle))
                idxMiddle = middleLetters.IndexOf(middle);

            if (!string.IsNullOrEmpty(last))
                idxLast = lastLetters.IndexOf(last);

            if (!string.IsNullOrEmpty(last))
                unicode = hangulUniCodeBase + (idxFirst * 21 + idxMiddle) * 28 + idxLast;
            else
                unicode = hangulUniCodeBase + (idxFirst * 21 + idxMiddle) * 28;


            // 코드값을 문자로 변환
            char hangul = Convert.ToChar(unicode);
            return hangul.ToString();
        }


        private byte[] temp = new byte[32];
        private void BtnWrite_Click(object sender, RoutedEventArgs e)
        {
            var str = Encoding.UTF32.GetChars(temp);

            if (string.IsNullOrEmpty(TxbText.Text)) return;
            Txb2.Text = $"{string.Join(string.Empty, str)} " +
                $"- {(char)(0x0000AC00 | Convert.ToUInt32(TxbText.Text, 16))}";

        }

        private void Txb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is not TextBox text) return;
            if (string.IsNullOrEmpty(text.Text)) return;
        }

        private void BtnGetByte_Click(object sender, RoutedEventArgs e)
        {
            string src = Txb3.Text;
            byte[] bytes = Encoding.UTF32.GetBytes(src);

            //if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
            temp = bytes;
            uint i = BitConverter.ToUInt32(bytes, 0);

            TxbText.Text = $"{i:x4}";
        }
    }
}


/*
 * 
        public void DividHangul(char letter)
        {
            int index1, index2, index3;

            ushort uTempCode = Convert.ToUInt16(letter);
            if ((uTempCode < hangulUniCodeBase) || (uTempCode > hangulUniCodeLast))
                return;

            int unicode = uTempCode - hangulUniCodeBase;
            index1 = unicode / (21 * 28);
            unicode %= (21 * 28);
            index2 = unicode / 28;
            unicode %= 28;
            index3 = unicode;

            string first = new(firstLetters[index1], 1);
            string middle = new(middleLetters[index2], 1);
            string last = new(lastLetters[index3], 1);

            CreateHangul(first, middle, last);
        }

*/

//var range = Enumerable.Range(0xAC00, count: 0xD7AF - 0xAC00);
//foreach (uint item in range.Take(500))
//{
//    char letter = (char)item;
//    char c = (char)(0x0000AC00 | (uint)letter); // h
//    Debug.WriteLine($"{item:x4} - {c} - {letter}");
//}


// 한글음절: AC00 ~ D7AF
// 11184개 문자를 표시하며, 완성형 한글처럼 모든 문자가 정의되어 있다. 코드는 0xAC00부터 0xD7A3 까지 사용하며, 나머지는 비어있고,
// 이후 0xD7FF까지는 자모 확장을 위해 예약된 영역이다