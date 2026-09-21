using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace Pesedjet.Client.Views;

public partial class MenuView : UserControl
{
    public MenuView()
    {
        InitializeComponent();
    }

    private async void OpenSettingsClick(object? sender, RoutedEventArgs e)
    {
        var settingsModal = new SettingsWindow();
        
        // Buscamos la MainWindow que contiene este UserControl
        var topLevel = this.GetVisualRoot() as Window;
        if (topLevel != null)
        {
            // Bloquea la MainWindow hasta que SettingsWindow se cierre
            await settingsModal.ShowDialog(topLevel); 
        }
    }
}