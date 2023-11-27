using System.Linq;
using MmaFIghter.MVVM.Models;

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

        public void ToggleFavorite(int userId, FighterModel fighter)
        {
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
                _dbContext.Favourites.Add(new Favourite { UserId = userId, FighterFirstName = fighter.first_name, FighterLastName = fighter.last_name });
            }

            // Save changes to the database
            _dbContext.SaveChanges();
        }
    }
}
