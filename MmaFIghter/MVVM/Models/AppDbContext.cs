using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace MmaFIghter.MVVM.Models
{
    public class AppDbContext : DbContext
{
    public DbSet<UserModel> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var databaseFolderPath = Path.Combine(folderPath, "Database");

            // Ensure the "Database" folder exists
            if (!Directory.Exists(databaseFolderPath))
            {
                Directory.CreateDirectory(databaseFolderPath);
            }

            var dbPath = Path.Combine(databaseFolderPath, "app.db");

            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }

}
