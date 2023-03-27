using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BudgetManager.ViewModels;

namespace BudgetManager.Views;

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
    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
}