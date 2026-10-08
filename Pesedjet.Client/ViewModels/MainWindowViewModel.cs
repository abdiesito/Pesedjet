using Pesedjet.Client.Utilities.Navigation;

namespace Pesedjet.Client.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public INavigator Navigator { get; }

    public MainWindowViewModel()
    {
        Navigator = new Navigator();
        Navigator.NavigateTo(new RegisterViewModel(Navigator));
    }
}
