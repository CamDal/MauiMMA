using MmaFIghter.MVVM.Views;

namespace MmaFIghter;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        MainPage = new NavigationPage(new MainPage());
    }
}
