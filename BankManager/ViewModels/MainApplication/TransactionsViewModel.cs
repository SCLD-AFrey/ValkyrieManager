using System;
using BankManager.Models.MainApplication;
using Microsoft.Extensions.Logging;
using ReactiveUI.Fody.Helpers;

namespace BankManager.ViewModels.MainApplication;

public class TransactionsViewModel : ViewModelBase
{
    private ILogger<TransactionsViewModel> m_logger;
    private readonly IServiceProvider m_serviceProvider;
    private readonly TransactionsModel m_model;

    public TransactionsViewModel(ILogger<TransactionsViewModel> p_logger, IServiceProvider p_serviceProvider, TransactionsModel p_model)
    {
        m_logger = p_logger;
        m_serviceProvider = p_serviceProvider;
        m_model = p_model;
    }

    [Reactive] public string PageHeaderText { get; set; } = "Transactions";
}