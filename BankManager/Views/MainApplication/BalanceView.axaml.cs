using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BankManager.ViewModels.MainApplication;

namespace BankManager.Views.MainApplication;

public partial class BalanceView : UserControl
{
#pragma warning disable CS8618
    public BalanceView() {  }
#pragma warning restore CS8618
    public BalanceView(BalanceViewModel p_viewModel)
    {
        DataContext = p_viewModel;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}