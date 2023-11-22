using Microsoft.EntityFrameworkCore;
using MmaFIghter.MVVM.Models;
using System;
using System.Linq;

namespace MmaFIghter.Services
{
    public class AuthService
    {
        private readonly AppDbContext _dbContext;

        public AuthService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated(); // Create the database if it doesn't exist
        }

        public string Login(string username, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                return "Login successful";
            }

            return "Invalid username or password";
        }

        public string Signup(string username, string password)
        {
            if (_dbContext.Users.Any(u => u.Username == username))
            {
                return "Username already exists";
            }

            _dbContext.Users.Add(new UserModel { Username = username, Password = password });
            _dbContext.SaveChanges();

            return "Signup successful";
        }

        public string ResetPassword(string username, string newPassword)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);

            if (user != null)
            {
                user.Password = newPassword;
                _dbContext.SaveChanges();
                return "Password reset successful";
            }

            return "User not found";
        }

        public bool ChangePassword(string username, string currentPassword, string newPassword)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == currentPassword);

            if (user != null)
            {
                user.Password = newPassword;
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public bool UserExists(string username)
        {
            return _dbContext.Users.Any(u => u.Username == username);
        }

        public void Logout()
        {
            Xamarin.Essentials.SecureStorage.Remove("AuthToken");
        }
    }
}
