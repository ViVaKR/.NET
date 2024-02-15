using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfCardGame
{

    public partial class MainWindow : Window
    {
        /// <summary>
        /// 오른 쪽 덱 목록
        /// </summary>
        public Dictionary<int, Card> RightCards { get; set; }

        /// <summary>
        /// 왼쪽 덱 목록
        /// </summary>
        public Dictionary<int, Card> LeftCards { get; set; }

        // 더블클릭시 해당 카드 인덱스 번호 (딕셔너리)
        private int? selectedIdx;

        // 카드를 섞을 횟수 (많을 수록 잘 섞임)
        private const int ShuffleCount = 3;

        public MainWindow()
        {
            InitializeComponent();

            Width = 1024;
            Height = 768;

            RightCards = [];
            LeftCards = [];
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // 폼 로드 이벤트
            Loaded += async (s, e) =>
            {
                // 순번대로 가즈런히 카드 만들기
                await ResetDeckAsync();

                // 카드 섞어 놓기
                await ShuffleCard(ShuffleCount);

                // 폼 사이즈를 기반으로 한 덱 사이징
                LeftDeckListBox.Height = RightDeckListBox.Height = Height - 200;
                LeftDeckListBox.Width = RightDeckListBox.Width = Width / 2 - 24;
            };



        }

        /// <summary>
        /// 카드 초기화, 새로운 반듯한 신카드
        /// </summary>
        /// <returns></returns>
        private async Task ResetDeckAsync()
        {
            LeftCards.Clear();
            RightCards.Clear();

            var list = (
               from suit in Enum.GetValues(typeof(Suits)).OfType<Suits>()
               from value in Enum.GetValues(typeof(Values)).OfType<Values>()
               select new Card(suit, value)).ToArray();

            for (var i = 0; i < list?.ToArray().Length; i++)
                LeftCards.Add(i, list[i]);

            // 리스트 박스에 아이템 넣기
            SetItems();

            // 타스크 종료 알림
            await Task.CompletedTask;
        }

        /// <summary>
        /// 카드 섞기
        /// </summary>
        /// <param name="suffleCount">섞은 카드를 반복하여 섞는 횟수</param>
        /// <returns></returns>
        private async Task ShuffleCard(int suffleCount)
        {
            
            for (var i = 0; i < suffleCount; i++)
            {
                var r = new Random();

                for (var n = LeftCards.Count - 1; n >= 0; n--)
                {
                    var k = r.Next(n + 1);
                    (LeftCards[n], LeftCards[k]) = (LeftCards[k], LeftCards[n]);
                }
            }

            SetItems();

            await Task.CompletedTask;
        }

        /// <summary>
        /// ListBox Left/Right SetItems
        /// </summary>
        private void SetItems()
        {
            LeftDeckListBox.ItemsSource = LeftCards.Select(x => $"{x.Key,2}.\t[ {(char)x.Value.Suit} {x.Value.Suit} {(int)x.Value.Value} ]").ToList();
            RightDeckListBox.ItemsSource = RightCards.Select(x => $"{x.Key,2}.\t[ {(char)x.Value.Suit} {x.Value.Suit} {(int)x.Value.Value} ]").ToList();
        }

        private async void ReSetCard_Click(object sender, RoutedEventArgs e)
        {
            BtnShuffle.IsEnabled = !(BtnReset.IsEnabled = false);

            await ResetDeckAsync();
            
        }


        private async void ShuffleCard_Click(object sender, RoutedEventArgs e)
        {
            BtnReset.IsEnabled = !(BtnShuffle.IsEnabled = false);
            await ShuffleCard(ShuffleCount);
            
        }



        private void LeftDeckListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is not ListBox list) return;

            var item = list.SelectedItem;

            if (item == null) return;

            // ListBox 가 MultiColumn 이 아니므로 앞에 딕셔너리의 키값을 Split 하여 추출
            selectedIdx = Convert.ToInt32(item?.ToString()?.Split('.', StringSplitOptions.RemoveEmptyEntries)[0]);
            if (selectedIdx == null) return;

            // 왼쪽 덱에서 해당 카드의 키값으로 추출
            KeyValuePair<int, Card>? s = LeftCards.FirstOrDefault(x => x.Key == selectedIdx);
            if (s == null) return;

            // 왼쪽 덱 목록 에서는 삭제
            LeftCards.Remove(s.Value.Key);

            // 오른쪽 덱 목록 에는 추가
            RightCards.Add(s.Value.Key, s.Value.Value);

            // 목록을 기반으로 리스트 박스 아이템 재설정
            SetItems();

            // 왼쪽 덱의 카드 수가 52장이 안될때 => Shuffle Button Disabled
            BtnReset.IsEnabled = !(BtnShuffle.IsEnabled = RightCards.Count == 0);
        }

    }
}