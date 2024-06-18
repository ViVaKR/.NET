using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// UserControl1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public EventHandler MainControls;

        private bool isLoggedIn;
        public bool IsLoggedIn
        {
            get => isLoggedIn;
            set
            {
                isLoggedIn = value;
                Btn_Login.IsEnabled = IsLoggedIn;

                MainControls?.Invoke(this, EventArgs.Empty);
            }
        }

        public UserControl1()
        {
            InitializeComponent();

            MainControls += ((MainWindow)Parent).ControlManager;

            Btn_Login.Content = "Login";
            Txt_CardID.Text = "FineThank";
            Txt_Pw.Text = "1234";

            Btn_Login.Click +=(s,e)=> MessageBox.Show("로그인 성공");
        }

        internal void LoginTextChanged(object sender, TextChangedEventArgs e)
        {
            string id = Txt_CardID.Text.Trim();
            string pw = Txt_Pw.Text.Trim();

            IsLoggedIn = (id.Equals("AndYou")) && (pw.Equals("1234"));
        }
    }
}
