using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pesedjet.Client.Utilities.Navigation;

namespace Pesedjet.Client.ViewModels;

public partial class MenuViewModel : ViewModelBase
{
    private readonly INavigator _navigator;
    
    public MenuViewModel(INavigator navigator)
    {
        _navigator = navigator;
    }
    
    [RelayCommand]
    private void NavigateToFriends()
    {
        _navigator.NavigateTo(new FriendsViewModel(_navigator));
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