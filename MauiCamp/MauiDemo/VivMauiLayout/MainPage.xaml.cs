namespace VivMauiLayout
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
            
        }

        private void SLayoutBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HStackDemo());
        }
    }

}
