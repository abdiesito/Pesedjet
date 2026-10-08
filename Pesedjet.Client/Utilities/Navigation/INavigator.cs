using Pesedjet.Client.ViewModels;

namespace Pesedjet.Client.Utilities.Navigation;

public interface INavigator
{
    ViewModelBase CurrentViewModel { get; }
    bool CanGoBack { get; }
    
    void NavigateTo(ViewModelBase viewModel);
    void GoBack();
    void NavigateAndClear(ViewModelBase viewModel);
}