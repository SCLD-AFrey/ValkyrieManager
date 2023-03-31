using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BankManager.ViewModels.MainApplication;

namespace BankManager.Views.MainApplication;

public partial class SettingsView : UserControl
{
#pragma warning disable CS8618
    public SettingsView() {  }
#pragma warning restore CS8618
    public SettingsView(SettingsViewModel p_viewModel)
    {
        DataContext = p_viewModel;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}