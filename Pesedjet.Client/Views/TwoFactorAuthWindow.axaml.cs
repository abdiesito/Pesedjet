using Avalonia.Controls;
using Pesedjet.Client.ViewModels;

namespace Pesedjet.Client.Views;

public partial class TwoFactorAuthWindow : Window
{
    public TwoFactorAuthWindow()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(System.EventArgs e)
    {
        base.OnDataContextChanged(e);

        if (DataContext is TwoFactorAuthViewModel viewModel)
        {
            viewModel.RequestClose += OnRequestClose;
        }
    }

    private void OnRequestClose()
    {
        Close();
    }
}