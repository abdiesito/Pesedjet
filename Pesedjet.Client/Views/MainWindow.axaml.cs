using Avalonia.Controls;

namespace Pesedjet.Client.Views;

public partial class MainWindow : Window
{
    private const int MIN_WIDTH = 800;
    private const int MIN_HEIGHT = 600;
    
    public MainWindow()
    {
        InitializeComponent();

        this.MinWidth = MIN_WIDTH;
        this.MinHeight = MIN_HEIGHT;

    }
}