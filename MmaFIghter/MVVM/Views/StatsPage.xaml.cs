using Microcharts;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using MmaFIghter.MVVM.Models;
using MmaFIghter.Services;
using MmaFIghter.MVVM.Views;
using MmaFIghter.MVVM.ViewModels;

namespace MmaFIghter.MVVM.Views
{
    public partial class StatsPage : ContentPage
    {
        private readonly int _userId;
        private readonly FavouriteService _favouriteService;
        private bool _isPageLoaded = false;

        public StatsPage(FighterModel fighter, int userId, FavouriteService favouriteService)
        {
            InitializeComponent();
            _userId = userId;
            _favouriteService = favouriteService;

            // Check if the user is logged in
            var isAuthenticated = Xamarin.Essentials.SecureStorage.GetAsync("IsAuthenticated").Result == "true";
            if (!isAuthenticated)
            {
                // Optionally, you can display a message to prompt the user to log in
                Console.WriteLine("Please log in to mark fighters as favorites.");
                Console.WriteLine($"StatsPage Constructor - userId: {_userId}");
                favouriteButton.IsEnabled = false;  // Disable the button if not logged in
            }
            else
            {
                // Initialize IsFavourite property based on user's favorites
                fighter.IsFavourite = IsFighterInFavorites(fighter);

                // Set BindingContext after initializing IsFavourite
                BindingContext = fighter;

                // Update the favorite button text
                UpdateFavoriteButtonText(fighter.IsFavourite);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Check if the page is loaded to avoid displaying the alert on initial load
            if (_isPageLoaded)
            {
                // Page is loaded, perform additional logic if needed
            }
            else
            {
                _isPageLoaded = true;
            }
        }

        private bool IsFighterInFavorites(FighterModel fighter)
        {
            // Get the list of favorite fighters for the current user
            var favoriteFighters = _favouriteService.GetFavouriteFighters(_userId);

            // Check if the current fighter is in the list of favorites
            return favoriteFighters.Any(f => f.first_name == fighter.first_name && f.last_name == fighter.last_name);
        }

        private async void OnFavouriteClicked(object sender, EventArgs e)
        {
            // Check if the user is logged in
            if (_userId <= 0)
            {
                await DisplayAlert("Error", "Please Login to Favourite.", "OK");
                return;
            }

            if (BindingContext is FighterModel fighter)
            {
                fighter.IsFavourite = !fighter.IsFavourite;

                // Save the favorite state to the database
                await _favouriteService.ToggleFavoriteAsync(_userId, fighter);

                // Update the favorite button text
                UpdateFavoriteButtonText(fighter.IsFavourite);
            }
        }

        private async void UpdateFavoriteButtonText(bool isFavourite)
        {
            favouriteButton.Text = isFavourite ? "Unfavourite" : "Favourite";

            if (_isPageLoaded) // Show alert only when the page is loaded
            {
                if (isFavourite)
                {
                    await DisplayAlert("Favourite", "Fighter marked as favourite.", "OK");
                }
                else
                {
                    await DisplayAlert("Unfavourite", "Fighter removed from favourites.", "OK");
                }
            }
        }
    }
}
