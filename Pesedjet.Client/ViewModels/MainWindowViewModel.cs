using CommunityToolkit.Mvvm.ComponentModel;

namespace Pesedjet.Client.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentViewModel = new MenuViewModel();
}
