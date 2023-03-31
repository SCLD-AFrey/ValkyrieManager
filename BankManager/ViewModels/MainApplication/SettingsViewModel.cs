using System;
using BankManager.Models.MainApplication;
using Microsoft.Extensions.Logging;
using ReactiveUI.Fody.Helpers;

namespace BankManager.ViewModels.MainApplication;

public class SettingsViewModel : ViewModelBase
{
    private ILogger<SettingsViewModel> m_logger;
    private readonly IServiceProvider m_serviceProvider;
    private readonly SettingsModel m_model;

    public SettingsViewModel(ILogger<SettingsViewModel> p_logger, IServiceProvider p_serviceProvider, SettingsModel p_model)
    {
        m_logger = p_logger;
        m_serviceProvider = p_serviceProvider;
        m_model = p_model;
    }

    [Reactive] public string PageHeaderText { get; set; } = "Settings";
}