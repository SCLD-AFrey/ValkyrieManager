using System;
using BankManager.Models.MainApplication;
using Microsoft.Extensions.Logging;

namespace BankManager.ViewModels.MainApplication;

public class HomeViewModel : ViewModelBase
{
    
    private ILogger<HomeViewModel> m_logger;
    private readonly IServiceProvider m_serviceProvider;
    private readonly HomeModel m_model;

    public HomeViewModel(ILogger<HomeViewModel> p_logger, IServiceProvider p_serviceProvider, HomeModel p_model)
    {
        m_logger = p_logger;
        m_serviceProvider = p_serviceProvider;
        m_model = p_model;
    }
}