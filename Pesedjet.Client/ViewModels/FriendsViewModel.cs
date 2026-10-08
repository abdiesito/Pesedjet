using CommunityToolkit.Mvvm.Input;
using Pesedjet.Client.Utilities.Navigation;

namespace Pesedjet.Client.ViewModels;

public partial class FriendsViewModel : ViewModelBase
{
    
    private readonly INavigator _navigator;

    public FriendsViewModel(INavigator navigator)
    {
        _navigator = navigator;
    }
    
    [RelayCommand]
    private void GoBack()
    {
        _navigator.GoBack();
    }
}