using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using MmaFIghter.MVVM.Models;
using Newtonsoft.Json;
using MmaFIghter.Services;
using OxyPlot;
using OxyPlot.Series;

namespace MmaFIghter.MVVM.ViewModels
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        public ICommand SearchCommand { get; private set; }
        public ObservableCollection<FighterModel> Fighters { get; private set; }
        public PlotModel PlotModel { get; private set; }

        public SearchViewModel()
        {
            Fighters = new ObservableCollection<FighterModel>();
            PlotModel = new PlotModel(); // Initialize PlotModel

            SearchCommand = new Command<string>(async (fighterName) =>
            {
                Console.WriteLine("SearchCommand executed.");
                await SearchFighter(fighterName);
            });
        }

        private async Task SearchFighter(string fighterName)
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

                var apiKey = "2752|NF7cXgtSXxCA3EB4ugXtxWP73eFTkNgRXhwEF15P";
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
                        await NavigationService.NavigateToStatsPage(selectedFighter);
                        UpdatePlotModel(selectedFighter); // Call the method to update the plot
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or display the exception message
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private async Task<string> GetFighterImageUrl(string firstName, string lastName)
        {
            try
            {
                var apiKey = "c7caa4d0-8757-11ee-94e8-1907c88c972c";
                var apiUrl = $"https://app.zenserp.com/api/v2/search?apikey={apiKey}&q={firstName}+{lastName}&tbm=isch";

                using (var client = new HttpClient())
                {
                    var response = await client.GetStringAsync(apiUrl);
                    var responseObject = JsonConvert.DeserializeObject<dynamic>(response);

                    // Extract the image URL from the response
                    var imageUrl = responseObject.image_results[0].sourceUrl;

                    return imageUrl;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching image: {ex.Message}");
                return null;
            }
        }

        private void UpdatePlotModel(FighterModel fighter)
        {
            var pieSeries = new PieSeries
            {
                StrokeThickness = 2.0,
                InsideLabelPosition = 0.5,
                AngleSpan = 360,
                StartAngle = 0
            };

            pieSeries.Slices.Add(new PieSlice("Wins", fighter.Wins) { IsExploded = true });
            pieSeries.Slices.Add(new PieSlice("Losses", fighter.Losses));
            pieSeries.Slices.Add(new PieSlice("No Contest", fighter.NoContest));

            PlotModel.Series.Clear();
            PlotModel.Series.Add(pieSeries);

            PlotModel.InvalidatePlot(true);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
