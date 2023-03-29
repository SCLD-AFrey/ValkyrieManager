using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BankManager.ViewModels;

namespace BankManager.Views;

public partial class MainWindowView : Window
{
#pragma warning disable CS8618
    public MainWindowView() {  }
#pragma warning restore CS8618
    
    public MainWindowView(MainWindowViewModel p_viewModel)
    {
        DataContext = p_viewModel;
        InitializeComponent();
    }
    
    public void InitializeComponent() => AvaloniaXamlLoader.Load(this);
}