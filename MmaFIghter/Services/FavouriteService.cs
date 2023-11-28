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

        // Parameterless constructor
        public FavouriteService()
        {
            // Create a default AppDbContext if needed
            _dbContext = new AppDbContext();
        }

        public List<FighterModel> GetFavouriteFighters(int userId)
        {
            // Implement the logic to retrieve favorite fighters based on the userId
            // This is a placeholder and not a complete implementation

            // For example, query your database or service
            // Return a collection of FighterModel objects
            // Make sure to adjust this based on your actual data retrieval logic

            // This is a placeholder and not a complete implementation
            // You need to replace it with actual logic to get favorite fighters
            return _dbContext.Favourites
                .Where(f => f.UserId == userId)
                .Select(f => new FighterModel
                {
                    first_name = f.FighterFirstName,
                    last_name = f.FighterLastName,
                    // Map other properties as needed
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

            // Save changes to the database
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
