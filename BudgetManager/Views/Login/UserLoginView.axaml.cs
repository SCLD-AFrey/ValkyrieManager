using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BudgetManager.ViewModels.Login;
using Microsoft.Extensions.Logging;

namespace BudgetManager.Views.Login;

public partial class UserLoginView : Window
{
#pragma warning disable CS8618
    public UserLoginView() { }
#pragma warning restore CS8618
    
    public UserLoginView(UserLoginViewModel p_viewModel, ILogger<UserLoginView> p_logger)
    {        
        p_logger.LogDebug("Creating UserLoginView");
        InitializeComponent();
        DataContext = p_viewModel;
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}