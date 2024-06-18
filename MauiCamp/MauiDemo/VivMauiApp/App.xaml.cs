namespace VivMauiApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var navPage = new NavigationPage(new MainPage())
            {
                BarBackground = Colors.Purple,
                BarTextColor = Colors.White
            };
            // MainPage = new AppShell();
            MainPage = navPage;
        }
    }
}
