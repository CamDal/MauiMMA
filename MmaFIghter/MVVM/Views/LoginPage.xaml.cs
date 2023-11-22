using Microsoft.Maui.Controls;
using MmaFIghter.MVVM.Views;
using MmaFIghter.Services;
using MmaFIghter.MVVM.ViewModels;

namespace MmaFIghter.MVVM.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly AuthService _authService;
        private readonly LoginPageViewModel _loginPageViewModel;

        public LoginPage(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
            BindingContext = new LoginPageViewModel();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            if(BindingContext == null)
            {
                // Log or debug the issue
                Console.WriteLine("BindingContext is null in OnLoginButtonClicked");
                return;
            }

            var viewModel = (LoginPageViewModel)BindingContext;
            string username = viewModel.Username;
            string password = viewModel.Password;

            string result = _authService.Login(username, password);
            await DisplayAlert("Login Result", result, "OK");

            // Assuming you want to navigate to the next page after a successful login
            if (result == "Login successful")
            {
                await Navigation.PushAsync(new MainPage(_authService, _loginPageViewModel));
            }
        }

        private async void OnSignupButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignupPage(_authService, _loginPageViewModel));
        }

        private async void OnForgotPasswordButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResetPasswordPage(_authService));
        }
    }
}
