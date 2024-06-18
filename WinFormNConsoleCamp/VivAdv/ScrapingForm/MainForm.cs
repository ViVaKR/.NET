using System.Globalization;

namespace ScrapingForm
{
    public partial class MainForm : Form
    {
        private GroupBox box;
        private Button buttonStatic;
        private Button buttonDynamic;
        private TextBox textBox;
        private DataGridView gridView;

        public MainForm()
        {
            InitializeComponent();

            //Width = 1600;
            //Height = 1400;
            //StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            //Controls.Add(gridView = new DataGridView
            //{
            //    Dock = DockStyle.Fill,
            //    BackgroundColor = Color.White

            //});


            Controls.Add(box = new GroupBox
            {
                Dock = DockStyle.Fill,
                Text = "Scrapping",
                Height = 500,
                Margin = new Padding(10)
            });

            box.BringToFront();
            box.Controls.AddRange(
            [
                textBox = new TextBox
                {
                    Dock = DockStyle.Fill,
                    Multiline = true,
                    Font = new Font(Font.FontFamily, 22f, FontStyle.Regular),
                    Text = "Hello World",
                    TextAlign = HorizontalAlignment.Center,
                    BorderStyle = BorderStyle.None,
                    ForeColor = Color.Gray,
                    ScrollBars = ScrollBars.Vertical
                },
                buttonStatic = new Button { Text = "Static", Dock = DockStyle.Left, Width = 150, Tag = 0 },
                buttonDynamic = new Button { Text = "Dynamic", Dock = DockStyle.Right, Width = 150, Tag = 1 },
            ]);

            buttonStatic.Click += Button_Click;
            buttonDynamic.Click += Button_Click;

        }

        private void Button_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btn) return;

            var tag = Convert.ToInt32(btn.Tag);

            switch (tag)
            {
                case 0: GetStaticWeb(); break;

                case 1: GetDynamicWeb(); break;
            }
        }

        private void GetDynamicWeb()
        {

        }

        private void GetStaticWeb()
        {
            StaticWebPage web = new StaticWebPage();
            textBox.Text = web.Get();
        }
    }
}
