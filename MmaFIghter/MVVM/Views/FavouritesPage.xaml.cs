using System;
using System.Collections.ObjectModel;
using MmaFIghter.MVVM.Models;
using MmaFIghter.Services;
using Microsoft.Maui.Controls;
using MmaFIghter.MVVM.ViewModels;

namespace MmaFIghter.MVVM.Views
{
    public partial class FavouritesPage : ContentPage
    {
        private readonly FavouriteService _favouriteService;
        private readonly int _userId;
        private readonly SearchViewModel _searchViewModel;

        public ObservableCollection<FighterModel> FavouriteFighters { get; set; }

        public FavouritesPage(FavouriteService favouriteService, int userId)
        {
            InitializeComponent();
            _favouriteService = favouriteService;
            _searchViewModel = new SearchViewModel();
            FavouriteFighters = new ObservableCollection<FighterModel>();
            BindingContext = this; // Set the BindingContext to the current instance of FavouritesPage
            // Set the BindingContext for the ListView
            FavouritesListView.BindingContext = this;
            // Load favorite fighters when the page is created
            LoadFavoriteFighters();
            _userId = userId;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Load favorite fighters when the page appears (e.g., when navigating back from another page)
            LoadFavoriteFighters();
        }

        private void LoadFavoriteFighters()
        {
            FavouriteFighters.Clear();
            var favoriteFighters = _favouriteService.GetFavouriteFighters(_userId);

            // Add the retrieved fighters to the ObservableCollection
            foreach (var favorite in favoriteFighters)
            {
                FavouriteFighters.Add(favorite);
            }
        }

    }
}
