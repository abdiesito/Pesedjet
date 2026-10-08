using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pesedjet.Client.Models;
using Pesedjet.Client.Utilities.Navigation;

namespace Pesedjet.Client.ViewModels;

public partial class AccessMenuViewModel : ViewModelBase
{
    private readonly LocalPlayerService _playerService = new();
    private readonly INavigationService _navigationService;

    public AccessMenuViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [ObservableProperty] 
    private string _username = string.Empty;
    
    [ObservableProperty] 
    private string _password = string.Empty;
    
    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(HasStatusMessage))] 
    private string _statusMessage = string.Empty;
    
    public bool HasStatusMessage => !string.IsNullOrEmpty(StatusMessage);

    [RelayCommand]
    public async Task LoginAsync()
    {
        StatusMessage = "Conectando a SQL Server...";

        try
        {
            var player = await _playerService.AuthenticatePlayerAsync(Username, Password);
            
            if (player != null && player.HashedPassword == Password)
            {
                StatusMessage = $"¡Acceso concedido, {player.Username}! Nivel/Exp: {player.Profile?.TotalExp ?? 0}";
                // TODO: Navegar al Menú Principal
            }
            else
            {
                StatusMessage = "Credenciales incorrectas o el usuario no existe.";
            }
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error de conexión: {ex.Message}";
        }
    }
    
    [RelayCommand]
    private void NavigateToPlayAsGuest()
    {
        _navigationService.NavigateTo(new CreateMatchViewModel(_navigationService));
    }
    
}