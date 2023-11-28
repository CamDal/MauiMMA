using Microsoft.Maui.Controls;
using MmaFIghter.Services;
using MmaFIghter.MVVM.Models;
using MmaFIghter.MVVM.ViewModels;
using System;

namespace MmaFIghter.MVVM.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly AuthService _authService;
        private readonly LoginPageViewModel _loginPageViewModel;
        private readonly FavouriteService _favouriteService;

        public LoginPage(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
            _loginPageViewModel = new LoginPageViewModel(); // Initialize the view model
            var dbContext = new AppDbContext();
            _favouriteService = new FavouriteService(dbContext);
            BindingContext = _loginPageViewModel;
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (BindingContext == null)
                {
                    // Log or debug the issue
                    Console.WriteLine("BindingContext is null in OnLoginButtonClicked");
                    return;
                }

                var viewModel = (LoginPageViewModel)BindingContext;

                if (string.IsNullOrWhiteSpace(viewModel.Username) || string.IsNullOrWhiteSpace(viewModel.Password))
                {
                    await DisplayAlert("Error", "Username and password are required fields.", "OK");
                    return;
                }

                string result = _authService.Login(viewModel.Username, viewModel.Password);
                await DisplayAlert("Login Result", result, "OK");

                // Assuming you want to navigate to the next page after a successful login
                if (result == "Login successful")
                {
                    _authService.SetAuthentication(true);
                    int userId = _authService.GetUserId(viewModel.Username);
                    ((App)Application.Current).UserId = userId;
                    Console.WriteLine($"User logged in with UserId: {userId}");
                    await Navigation.PushAsync(new MainPage(_authService, _loginPageViewModel, _favouriteService, userId));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
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
