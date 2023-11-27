using System;

namespace MmaFIghter.MVVM.Models
{
    public class FighterModel
    {
        private string _record;

        public string SApM { get; set; }
        public string SLpM { get; set; }
        public string StrAcc { get; set; }
        public string StrDef { get; set; }
        public string SubAvg { get; set; }
        public string TDAcc { get; set; }
        public string TDAvg { get; set; }
        public string TDDef { get; set; }
        public string date_of_birth { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string ImageUrl { get; set; }

        public bool IsFavourite { get; set; }

        public int Wins { get; private set; }
        public int Losses { get; private set; }
        public int Draws { get; private set; }

        public string Record
        {
            get => _record;
            set
            {
                _record = value;
                UpdateRecordValues();
            }
        }

        private void UpdateRecordValues()
        {
            if (!string.IsNullOrEmpty(Record))
            {
                var parts = Record.Split('-');
                if (parts.Length == 3 && int.TryParse(parts[0], out int wins) && int.TryParse(parts[1], out int losses) && int.TryParse(parts[2], out int draws))
                {
                    Wins = wins;
                    Losses = losses;
                    Draws = draws;
                }
                else
                {
                    Console.WriteLine($"Failed to parse Record: {Record}");
                    Console.WriteLine($"Split parts: {string.Join(", ", parts)}");
                }
            }
            else
            {
                Console.WriteLine("Record is null or empty. Default values set.");
                Wins = 0;
                Losses = 0;
                Draws = 0;
            }
        }
    }
}
