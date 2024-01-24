
using System.Drawing;
using System.Windows.Forms;

namespace FormControls
{
    public partial class MainForm : Form
    {

        private Panel panel;

        public MainForm()
        {
            InitializeComponent();

            Width = 2048;
            Height = 1600;
            StartPosition = FormStartPosition.CenterScreen;

            Load += (s, e) =>
            {
                Controls.Add(panel = new Panel
                {
                    Dock = DockStyle.Fill,
                    Width = 1024,
                    Height = 1024,
                    BackColor = Color.DarkOrchid,
                    Padding = new Padding(100),
                    BorderStyle = BorderStyle.FixedSingle
                    
                });

                panel.Controls.Add(new Label
                {
                    Dock=  DockStyle.Fill,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.Beige,
                    ForeColor = Color.DarkKhaki,
                    Font = new Font(Font.FontFamily, 78f, FontStyle.Bold),
                    Text = "안녕하세요 반갑습니다."
                });
            };
        }
    }
}
