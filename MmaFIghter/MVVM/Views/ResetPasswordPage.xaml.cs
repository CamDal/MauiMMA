using Microsoft.Maui.Controls;
using MmaFIghter.Services;

namespace MmaFIghter.MVVM.Views
{
    public partial class ResetPasswordPage : ContentPage
    {
        private readonly AuthService _authService;

        public ResetPasswordPage(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private async void OnResetPasswordButtonClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string newPassword = NewPasswordEntry.Text;

            string result = _authService.ResetPassword(username, newPassword);
            await DisplayAlert("Reset Password Result", result, "OK");

            // Assuming you want to navigate back to the login page after resetting the password
            if (result == "Password reset successful")
            {
                await Navigation.PopAsync();
            }
        }

        private async void OnBackToLoginButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
