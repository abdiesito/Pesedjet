using CommunityToolkit.Mvvm.Input;
using Pesedjet.Client.Utilities.Navigation;

namespace Pesedjet.Client.ViewModels;

public partial class CreateMatchViewModel : ViewModelBase
{
    private readonly INavigator _navigator;

    public CreateMatchViewModel(INavigator navigator)
    {
        _navigator = navigator;
    }
 
    [RelayCommand]
    private void GoBack()
    {
        _navigator.GoBack();
    }
}