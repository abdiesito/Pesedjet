using CommunityToolkit.Mvvm.ComponentModel;

namespace Pesedjet.Client.ViewModels;

public partial class MatchViewModel : ViewModelBase
{
    [ObservableProperty]
    private int _selectedPlayers = 2;

    [ObservableProperty]
    private string _selectedType = "Normal";

    [ObservableProperty]
    private string _selectedPrivacy = "Public";
}