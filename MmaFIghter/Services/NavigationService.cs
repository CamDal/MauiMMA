using Xamarin.Essentials;
using MmaFIghter.MVVM.Models;
using MmaFIghter.MVVM.Views;

namespace MmaFIghter.Services
{
    public class NavigationService
    {
        public static async Task NavigateToStatsPage(FighterModel selectedFighter, int userId, FavouriteService favouriteService)
        {
            await Xamarin.Essentials.MainThread.InvokeOnMainThreadAsync(() =>
            {
                App.Current.MainPage.Navigation.PushAsync(new StatsPage(selectedFighter, userId, favouriteService));
            });
        }
    }
}
