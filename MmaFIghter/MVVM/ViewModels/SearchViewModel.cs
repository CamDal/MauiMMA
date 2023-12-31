﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using MmaFIghter.MVVM.Models;
using Newtonsoft.Json;
using MmaFIghter.Services;

namespace MmaFIghter.MVVM.ViewModels
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        private int userId;
        private FavouriteService favouriteService;


        public ICommand SearchCommand { get; private set; }
        public ObservableCollection<FighterModel> Fighters { get; private set; }

        public ObservableCollection<string> ImageItems { get; private set; }

        public FavouriteService FavouriteService
        {
            get => favouriteService;
            set
            {
                if (favouriteService != value)
                {
                    favouriteService = value;
                    OnPropertyChanged(nameof(FavouriteService));
                }
            }
        }

        public int UserId
        {
            get => userId;
            set
            {
                if (userId != value)
                {
                    userId = value;
                    OnPropertyChanged(nameof(UserId));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public SearchViewModel()
        {
            // Default constructor without parameters
            Fighters = new ObservableCollection<FighterModel>();
            SearchCommand = new Command<string>(async fighterName =>
            {
                Console.WriteLine("SearchCommand executed.");
                await SearchFighter(fighterName, userId, favouriteService);
            });

            ImageItems = new ObservableCollection<string>
        {
            "ufc296.jpeg",
            "ufc5.jpg",
            "alexjiri.jpg",
            "leonusman.jpg",
            "seanizzy.jpg",
            "francis.jpg",
            "goats.jpg",
        };
        }

        public SearchViewModel(FavouriteService favouriteService, int userId) : this()
        {
            // Parameterized constructor
            this.favouriteService = favouriteService;
            this.userId = userId;  // Initialize userId
        }

        private async Task SearchFighter(string fighterName, int userId, FavouriteService favouriteService)
        {
            try
            {
                if (string.IsNullOrEmpty(fighterName))
                {
                    System.Console.WriteLine("Please enter a fighter name.");
                    return;
                }

                // Split the fighterName into first and last names (assuming space separates first and last names)
                var names = fighterName.Split(' ');

                if (names.Length < 2)
                {
                    Console.WriteLine("Please enter both first and last names.");
                    return;
                }

                var firstName = names[0];
                var lastName = names[1];

                var apiKey = "2831|kUDaoAm1kO6QcKhnI3U0270aYVv9uMpHVU8dPWCN";
                var apiUrl = $"https://zylalabs.com/api/2003/ufc+fighters+data+api/1770/get+information+by+fighters?first_name={firstName}&last_name={lastName}&apiKey={apiKey}";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                    var response = await client.GetStringAsync(apiUrl);
                    Console.WriteLine(response); // Print the API response to the console

                    // Deserialize directly into a FighterModel
                    var fighter = JsonConvert.DeserializeObject<FighterModel>(response);

                    // Set the image URL first
                    fighter.ImageUrl = await GetFighterImageUrl(firstName, lastName);

                    Fighters.Clear();
                    Fighters.Add(fighter);

                    if (Fighters.Any())
                    {
                        var selectedFighter = Fighters.First();
                        await NavigationService.NavigateToStatsPage(selectedFighter, userId, favouriteService);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or display the exception message
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        protected virtual async void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            await Task.CompletedTask;
        }

        private async Task<string> GetFighterImageUrl(string firstName, string lastName)
        {
            try
            {
                var apiKey = "d7b2fdc0-8846-11ee-9fd6-f563d1e26248";
                var apiUrl = $"https://app.zenserp.com/api/v2/search?apikey={apiKey}&q={firstName}+{lastName}&tbm=isch";

                using (var client = new HttpClient())
                {
                    var response = await client.GetStringAsync(apiUrl);
                    var responseObject = JsonConvert.DeserializeObject<dynamic>(response);

                    // Extract the image URL from the response
                    var imageUrl = responseObject.image_results[0].sourceUrl;
                    Console.WriteLine($"Image URL: {imageUrl}");
                    return imageUrl;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching image: {ex.Message}");
                return null;
            }
        }
    }
}
