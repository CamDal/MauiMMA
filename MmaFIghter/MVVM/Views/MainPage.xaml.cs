using MmaFIghter.MVVM.ViewModels;
using MmaFIghter.MVVM.Views;
using MmaFIghter.Services;

namespace MmaFIghter.MVVM.Views;

public partial class MainPage : ContentPage
{
    private readonly AuthService _authService;
    private readonly LoginPageViewModel _loginPageViewModel;

    public MainPage(AuthService authService, LoginPageViewModel loginPageViewModel)
    {
        InitializeComponent();
        _authService = authService;
        _loginPageViewModel = loginPageViewModel;
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

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage(_authService));
    }

    private async void OnSignupButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignupPage(_authService, _loginPageViewModel));
    }
}