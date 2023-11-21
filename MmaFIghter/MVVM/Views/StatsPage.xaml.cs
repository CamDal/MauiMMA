using Microcharts;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using MmaFIghter.MVVM.Models;
using MmaFIghter.Services;

namespace MmaFIghter.MVVM.Views
{
    public partial class StatsPage : ContentPage
    {
        public StatsPage(FighterModel fighter)
        {
            InitializeComponent();
            BindingContext = fighter;
            Resources.Add("WidthConverter", new WidthConverter());
        }
    }
}
