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

        public StatsPage(FighterModel fighter, int userId, FavouriteService favouriteService)
        {
            InitializeComponent();
            BindingContext = fighter;
            _userId = userId;
            Resources.Add("WidthConverter", new WidthConverter());
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

            // Log the userId to check if it's assigned correctly
        }

        private void OnFavouriteClicked(object sender, EventArgs e)
        {
            // Check if the user is logged in
            if (_userId <= 0)
            {
                // Optionally, you can display a message to prompt the user to log in
                Console.WriteLine("Please log in to mark fighters as favorites.");
                Console.WriteLine($"StatsPage Constructor - userId: {_userId}");
                return;
            }

            if (BindingContext is FighterModel fighter)
            {
                fighter.IsFavourite = !fighter.IsFavourite;
                Console.WriteLine("Fighter marked as favorites.");
                // Save the favorite state to the database
                Task task = _favouriteService.ToggleFavoriteAsync(_userId, fighter);
            }
        }
    }


}
