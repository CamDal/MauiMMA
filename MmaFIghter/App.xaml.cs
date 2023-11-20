using MmaFIghter.MVVM.Views;
using MmaFIghter.Services;

namespace MmaFIghter;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        MainPage = new NavigationPage(new MainPage());
    }
}
