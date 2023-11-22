using Microsoft.Maui.Controls;
using MmaFIghter.Services;
using MmaFIghter.MVVM.ViewModels;

namespace MmaFIghter.MVVM.Views
{
    public partial class SignupPage : ContentPage
    {
        private readonly AuthService _authService;
        private readonly LoginPageViewModel _viewModel;

        public SignupPage(AuthService authService, LoginPageViewModel viewModel)
        {
            InitializeComponent();
            _authService = authService;
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        private async void OnSignupButtonClicked(object sender, EventArgs e)
        {
            try
            {
                string result = _authService.Signup(_viewModel.Username, _viewModel.Password);
                await DisplayAlert("Signup Result", result, "OK");

                if (result == "Signup successful")
                {
                    await Navigation.PushAsync(new LoginPage(_authService));
                }
                else
                {
                    await DisplayAlert("Signup Error", result, "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private async void OnBackToLoginButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
