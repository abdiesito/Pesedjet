using CommunityToolkit.Mvvm.ComponentModel;
using Pesedjet.Client.Utilities.Navigation;

namespace Pesedjet.Client.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public INavigationService NavigationService { get; }

    public MainWindowViewModel()
    {
        NavigationService = new NavigationService();
        NavigationService.NavigateTo(new AccessMenuViewModel(NavigationService));
    }
}
