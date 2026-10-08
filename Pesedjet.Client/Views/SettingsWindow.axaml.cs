using Avalonia.Controls;
using Avalonia.Interactivity;
using Pesedjet.Client.ViewModels;

namespace Pesedjet.Client.Views;

public partial class SettingsWindow : Window
{
    public SettingsWindow()
    {
        InitializeComponent();
        DataContext = new SettingsViewModel();
    }

    //TO DO: sender en estandar y e
    private void CloseButtonClick(object?  sender, RoutedEventArgs e)
    {
        this.Close();
    }
}