using MmaFIghter.MVVM.ViewModels;

namespace MmaFIghter.MVVM.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        BindingContext = new SearchViewModel();
    }
}	