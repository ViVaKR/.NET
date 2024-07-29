using System.Text;

namespace CallTextData
{
    public partial class Form1 : Form
    {
        private readonly List<TextBox> textBoxs; // List of TextBoxes

        // 파일이 저장될 폴더 지정 : 바탕화면의 Files 폴더 아래에 저장 으로 데모 진행
        private readonly string rootFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Files");

        public Form1()
        {
            InitializeComponent();

            textBoxs = new List<TextBox>(10); // Initialize List of TextBoxes
            Directory.CreateDirectory(rootFolder); // Create Directory if not exists

            for (int i = 0; i < 10; i++) // Create 10 TextBoxes
                textBoxs.Add(new TextBox { Dock = DockStyle.Top, Height = 100, Multiline = true, Tag = i, Text = string.Empty });

            Controls.AddRange([.. textBoxs]); // Add TextBoxes to Form

            // 데이터 저장 버튼
            Button saveData = new() { Text = "Save To File", BackColor = Color.Beige, Dock = DockStyle.Bottom, Height = 100 };

            // 데이터 불러오기 버튼
            Button getData = new() { Text = "Bind Text", BackColor = Color.DeepSkyBlue, Dock = DockStyle.Bottom, Height = 100 };
            Controls.AddRange([saveData, getData]);

            // 저장 버튼 클릭 이벤트
            saveData.Click += async (s, e) => await SaveDataAsync();

            // 불러오기 버튼 클릭 이벤트
            getData.Click += async (s, e) => await GetDataAsync();
        }

        /// <summary>
        /// 데이터 불러오기
        /// </summary>
        /// <returns></returns>
        private async Task GetDataAsync()
        {
            foreach (var textBox in textBoxs)
            {
                textBox.Text = string.Empty;
                string file = Path.Combine(rootFolder, $@"userInput{textBox.Tag}.txt"); // 파일 경로

                if (!File.Exists(file)) continue; // 파일이 존재하지 않으면 다음 TextBox로 이동

                var data = await File.ReadAllTextAsync(file, Encoding.UTF8); // 파일 내용 읽기
                textBox.Text = data; // TextBox에 데이터 바인딩
            }
        }

        /// <summary>
        /// 데이터 저장
        /// </summary>
        /// <returns></returns>
        private async Task SaveDataAsync()
        {
            int i = 0;
            foreach (var textBox in textBoxs)
            {
                string file = Path.Combine(rootFolder, $@"userInput{textBox.Tag}.txt"); // 파일 저장 경로

                var fi = new FileInfo(file); // 파일 정보
                if(fi.Exists) fi.Delete(); // 파일이 존재하면 삭제
                
                // 파일에 데이터 저장
                await File.WriteAllTextAsync(fi.FullName, textBoxs[i++].Text, Encoding.UTF8);

                // 저장 후 TextBox 초기화
                textBox.Text = string.Empty;
            }
        }
    }
}
