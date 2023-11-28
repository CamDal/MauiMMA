using MmaFIghter.Services;
using MmaFIghter.MVVM.ViewModels;
using System;

namespace MmaFIghter.MVVM.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly AuthService _authService;
        private readonly LoginPageViewModel _loginPageViewModel;
        private readonly FavouriteService _favouriteService;

        public MainPage(AuthService authService, LoginPageViewModel loginPageViewModel, FavouriteService favouriteService, int userId)
        {
            InitializeComponent();
            _authService = authService;
            _loginPageViewModel = loginPageViewModel;
            _favouriteService = favouriteService;
            BindingContext = new SearchViewModel(favouriteService, userId);
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

        // Add this method for navigating to the FavouritesPage
        private async void OnFavouritesButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FavouritesPage(_favouriteService, ((App)Application.Current).UserId));
        }
    }
}
