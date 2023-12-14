using System;
using System.Windows.Forms;

namespace KeyHook
{
    public partial class MainForm : Form
    {
        private readonly KeyboardHook hook;

        public MainForm()
        {
            InitializeComponent();

            hook = new KeyboardHook();

            hook.KeyPressed += Hook_KeyPressed;

            Grp_Register.Resize += (s, e) =>
            {
                GroupBox box = s as GroupBox;

                Grp_Register.Height = 90;

                foreach (Control item in box.Controls)
                {
                    if (item is Button btn) btn.Width = box.Width / box.Controls.Count;
                }
            };

            Width = 1024;
            Height = 768;

            WindowState = FormWindowState.Normal;

            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            Lbx_Status.Items.Add($"{e.Modifier} {e.Key}");

            Lbl_Status.Text = $"{e.Modifier} {e.Key}\n";
        }

        private void Btn_Add_Click(object sender, EventArgs e)
        {
            if (!(sender is Button btn)) return;

            switch (btn.Tag as string)
            {
                case "A": hook.RegisterHotKey(KeyModifiers.Control | KeyModifiers.Shift, Keys.A); break;
                case "B": hook.RegisterHotKey(KeyModifiers.Control | KeyModifiers.Shift, Keys.B); break;
                case "C": hook.RegisterHotKey(KeyModifiers.Control | KeyModifiers.Shift, Keys.C); break;
                case "D": hook.RegisterHotKey(KeyModifiers.Control | KeyModifiers.Shift, Keys.D); break;
                case "Z": hook.Unregister(); break;

                default:
                    {
                        hook.Unregister();
                        Lbx_Status.Items.Clear();
                        Lbl_Status.Text = string.Empty;
                    }
                    break;
            }
        }
    }
}
