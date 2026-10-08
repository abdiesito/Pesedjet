using CommunityToolkit.Mvvm.Input;
using Pesedjet.Client.Utilities.Navigation;

namespace Pesedjet.Client.ViewModels;

public partial class FriendsViewModel : ViewModelBase
{
    
    private readonly INavigationService _navigationService;

    public FriendsViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
    
    [RelayCommand]
    private void GoBack()
    {
        _navigationService.GoBack();
    }
}