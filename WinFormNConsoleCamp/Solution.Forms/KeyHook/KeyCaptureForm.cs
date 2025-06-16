using System.Diagnostics;
using System.Windows.Forms;

namespace KeyHook
{
    public partial class KeyCaptureForm : Form, IMessageFilter
    {
        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;
        const int WM_SYSKEYDOWN = 0x104;
        private Keys lastKeyPressed = Keys.None;

        public KeyCaptureForm()
        {
            InitializeComponent();

            Application.AddMessageFilter(this);
            FormClosing += (s, e) => Application.RemoveMessageFilter(this);
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_KEYUP)
            {
                Debug.WriteLine($"Filter -> {lastKeyPressed}");
            }

            return false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == WM_KEYDOWN || msg.Msg == WM_SYSKEYDOWN)
            {
                lastKeyPressed = keyData;
                label1.Text += $"{keyData}\n";
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
