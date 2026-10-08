using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pesedjet.Client.Utilities.Navigation;

namespace Pesedjet.Client.ViewModels;

public partial class MenuViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    
    public MenuViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
    
    [RelayCommand]
    private void NavigateToFriends()
    {
        _navigationService.NavigateTo(new FriendsViewModel(_navigationService));
    }
    
    [ObservableProperty]
    private string _gameTagUser = "#FaraonUri";

    [ObservableProperty]
    private int _level = 10;

    [ObservableProperty]
    private string _rank = "Rango Oro";

    [ObservableProperty]
    private int _pendingFriendsRequests = 1;

    [ObservableProperty]
    private int _pendingRoomInvitations = 2;
}