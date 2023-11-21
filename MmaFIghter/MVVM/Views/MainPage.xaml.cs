using MmaFIghter.MVVM.ViewModels;

namespace MmaFIghter.MVVM.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        BindingContext = new SearchViewModel();
    }

    private async void OnFormButtonClicked(object sender, EventArgs e)
    {
        // Implement navigation logic here
        // For example, open a link
        string url = "https://forms.gle/cpg2rNodTNPztm1C8";

        // Open the link in the default system browser
        await Launcher.OpenAsync(new Uri(url));
    }
}