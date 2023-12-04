using System.Linq;
using MmaFIghter.MVVM.Models;
using MmaFIghter.MVVM.ViewModels;
namespace MmaFIghter.Services
{
    public class FavouriteService
    {
        private readonly AppDbContext _dbContext;

        public FavouriteService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public FavouriteService()
        {
            _dbContext = new AppDbContext();
        }

        public List<FighterModel> GetFavouriteFighters(int userId)
        {
            return _dbContext.Favourites
                .Where(f => f.UserId == userId)
                .Select(f => new FighterModel
                {
                    first_name = f.FighterFirstName,
                    last_name = f.FighterLastName,
                })
                .ToList();
        }

        public async Task ToggleFavoriteAsync(int userId, FighterModel fighter)
        {

            if (userId <= 0)
            {
                Console.WriteLine("User is not logged in. Cannot toggle favorite.");
                return;
            }

            // Check if the fighter is already in the user's favorites
            var existingFavorite = _dbContext.Favourites
                .FirstOrDefault(f => f.UserId == userId && f.FighterFirstName == fighter.first_name && f.FighterLastName == fighter.last_name);

            if (existingFavorite != null)
            {
                // Remove from favorites
                _dbContext.Favourites.Remove(existingFavorite);
            }
            else
            {
                // Add to favorites
                _dbContext.Favourites.Add(new Favourite { UserId = userId, FighterFirstName = fighter.first_name, FighterLastName = fighter.last_name});
            }

            await _dbContext.SaveChangesAsync();
        }

        public List<Favourite> GetFavorites(int userId)
        {
            // Retrieve favorite fighters based on user ID
            return _dbContext.Favourites
                .Where(f => f.UserId == userId)
                .ToList();
        }
    }
}
