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
                Console.WriteLine("Please log in to mark fighters as favorites.");
                Console.WriteLine($"StatsPage Constructor - userId: {_userId}");
                favouriteButton.IsEnabled = false;
            }
            else
            {
                fighter.IsFavourite = IsFighterInFavorites(fighter);

                BindingContext = fighter;

                UpdateFavoriteButtonText(fighter.IsFavourite);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Check if the page is loaded to avoid displaying the alert on initial load
            if (_isPageLoaded)
            {
                // No logic needed
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

                await _favouriteService.ToggleFavoriteAsync(_userId, fighter);
               
                UpdateFavoriteButtonText(fighter.IsFavourite);
            }
        }

        private async void UpdateFavoriteButtonText(bool isFavourite)
        {
            favouriteButton.Text = isFavourite ? "Unfavourite" : "Favourite";

            if (_isPageLoaded)
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
