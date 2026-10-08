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
        
        var topLevel = this.GetVisualRoot() as Window;
        if (topLevel != null)
        {
            await settingsModal.ShowDialog(topLevel); 
        }
    }
}