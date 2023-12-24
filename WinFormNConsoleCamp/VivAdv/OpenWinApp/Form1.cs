using System.Diagnostics;
using System.Runtime.InteropServices;

namespace OpenWinApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Button button;
            Controls.Add(button = new Button
            {
                Dock = DockStyle.Bottom,
                Text = "Open",
                Height = 100
            });

            button.Click += Button_Click;
            
        }

        private void Button_Click(object? sender, EventArgs e)
        {
            OpenApp.Run();
        }


    }
}
