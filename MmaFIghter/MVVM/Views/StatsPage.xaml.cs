using Microcharts;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using MmaFIghter.MVVM.Models;
using MmaFIghter.Services;
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
        }

        private void OnFavouriteClicked(object sender, EventArgs e)
        {
            if (BindingContext is FighterModel fighter)
            {
                fighter.IsFavourite = !fighter.IsFavourite;

                // Save the favorite state to the database
                _favouriteService.ToggleFavorite(_userId, fighter);
            }
        }
    }
}
