using MmaFIghter.MVVM.Views;
using MmaFIghter.Services;
using MmaFIghter.MVVM.ViewModels;
using MmaFIghter.MVVM.Models;

namespace MmaFIghter
{
    public partial class App : Application
    {
        public int UserId { get; set; }

        public App()
        {
            InitializeComponent();
            var authService = new AuthService(new AppDbContext());
            var loginPageViewModel = new LoginPageViewModel();
            var dbContext = new AppDbContext();

            // Pass the AppDbContext to the FavouriteService constructor
            var favouriteService = new FavouriteService(dbContext);

            // Pass the AuthService and LoginPageViewModel to MainPage constructor
            MainPage = new NavigationPage(new MainPage(authService, loginPageViewModel, favouriteService, UserId));
        }
    }
}
