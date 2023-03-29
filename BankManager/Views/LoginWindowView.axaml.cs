using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BankManager.ViewModels;

namespace BankManager.Views;

public partial class LoginWindowView : Window
{
#pragma warning disable CS8618
    public LoginWindowView() {  }
#pragma warning restore CS8618
    
    public LoginWindowView(LoginWindowViewModel p_viewModel)
    {
        DataContext = p_viewModel;
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }
    public void InitializeComponent() => AvaloniaXamlLoader.Load(this);
}