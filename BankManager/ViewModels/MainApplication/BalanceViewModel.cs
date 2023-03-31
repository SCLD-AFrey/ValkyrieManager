using System;
using BankManager.Models.MainApplication;
using Microsoft.Extensions.Logging;
using ReactiveUI.Fody.Helpers;

namespace BankManager.ViewModels.MainApplication;

public class BalanceViewModel : ViewModelBase
{
    private ILogger<BalanceViewModel> m_logger;
    private readonly IServiceProvider m_serviceProvider;
    private readonly BalanceModel m_model;

    public BalanceViewModel(ILogger<BalanceViewModel> p_logger, IServiceProvider p_serviceProvider, BalanceModel p_model)
    {
        m_logger = p_logger;
        m_serviceProvider = p_serviceProvider;
        m_model = p_model;
    }

    [Reactive] public string PageHeaderText { get; set; } = "Balances";

}