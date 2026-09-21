using CommunityToolkit.Mvvm.ComponentModel;
using Pesedjet.Client.Services;

namespace Pesedjet.Client.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isSoundEnabled = true;

    [ObservableProperty]
    private bool _isMusicEnabled = true;
    
    [ObservableProperty]
    private int _selectedLanguageIndex = 0; 
    
    partial void OnSelectedLanguageIndexChanged(int value)
    {
        if (value == 0)
        {
            LocalizationManager.Instance.ChangeLanguage("es-MX");
        }
        else if (value == 1)
        {
            LocalizationManager.Instance.ChangeLanguage("en-US");
        }
    }
}