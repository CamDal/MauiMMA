// NavigationService.cs
using Xamarin.Essentials;
using MmaFIghter.MVVM.Models;
using MmaFIghter.MVVM.Views;

namespace MmaFIghter.Services
{
    public class NavigationService
    {
        public static async Task NavigateToStatsPage(FighterModel selectedFighter)
        {
            await Xamarin.Essentials.MainThread.InvokeOnMainThreadAsync(() =>
            {
                // Assume that StatsPage is the page where you display detailed fighter stats
                App.Current.MainPage.Navigation.PushAsync(new StatsPage(selectedFighter));
            });
        }
    }
}
