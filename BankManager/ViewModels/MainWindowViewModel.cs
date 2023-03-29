using Microsoft.Extensions.Logging;

namespace BankManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ILogger<MainWindowViewModel> m_logger;
    public MainWindowViewModel(ILogger<MainWindowViewModel> p_logger)
    {
        m_logger = p_logger;
        m_logger.LogDebug("Initializing MainWindowViewModel");
    }
    public string Greeting => "MainWindowViewModel";
}