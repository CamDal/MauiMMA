using MmaFIghter.MVVM.Models;

namespace MmaFIghter.MVVM.Views;

public partial class StatsPage : ContentPage
{
    public StatsPage(FighterModel selectedFighter)
    {
        InitializeComponent();
        BindingContext = selectedFighter;
    }
}
