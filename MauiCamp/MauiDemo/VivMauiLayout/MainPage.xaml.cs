namespace VivMauiLayout
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void HSLayoutBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HStackDemo());
        }

        private void GridBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GridDemo());
        }

        private void CalcBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CalcDemo());
        }

        private void FlexBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FlexDemo());
        }
    }

}
