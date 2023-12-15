namespace Sockets
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Controls.Add(new Label
            {
                Text = "\u2734",
                Font = new Font(Font.FontFamily, 42f, FontStyle.Regular),
                Dock = DockStyle.Fill,
            });
        }
    }
}
