namespace DnevniCitati;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new NavigationPage(new MainPage())
        {
            BarBackgroundColor = Color.FromArgb("#315C4B"),
            BarTextColor = Colors.White
        };
    }
}
