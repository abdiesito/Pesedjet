using CommunityToolkit.Mvvm.ComponentModel;

namespace Pesedjet.Client.ViewModels;

public partial class MenuViewModel : ViewModelBase
{
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