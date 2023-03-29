using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;
using BankManager.Models;
using BankManager.Views;
using DidiSoft.OpenSsl.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReactiveUI.Fody.Helpers;

namespace BankManager.ViewModels;

public class LoginWindowViewModel
{
    private ILogger<LoginWindowViewModel> m_logger;
    private readonly IServiceProvider m_serviceProvider;
    private readonly LoginWindowModel m_model;
    public LoginWindowViewModel(ILogger<LoginWindowViewModel> p_logger, IServiceProvider p_serviceProvider, LoginWindowModel p_model)
    {
        m_logger = p_logger;
        m_serviceProvider = p_serviceProvider;
        m_model = p_model;
        m_logger.LogDebug("Initializing LoginWindowViewModel");
    }
    
    [Reactive] public string HeaderTitle { get; set; } = "Login";
    [Reactive] public string UserName { get; set; }
    [Reactive] public string Password { get; set; }
    [Reactive] public bool   RememberMe { get; set; }
    [Reactive] public string ValidationMessage { get; set; }
    public int AttemptedLoginCount { get; set; }

    public void ClickCloseCommandCenter()
    {
        m_model.CloseApplication();
    }
    
    public async Task ClickLogin(Window p_loginWindow)
    {
        try
        {
            ValidationMessage = string.Empty;
            await Task.Delay(m_model.GetLoginDelayTime(AttemptedLoginCount));
            await m_model.AttemptLogin(UserName, Password, RememberMe);
            m_logger.LogDebug("Login was successful for {Username}", UserName);

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                var mainWindow = m_serviceProvider.GetService<MainWindowView>();
                mainWindow!.Show();
                p_loginWindow.Close();
            });

        } catch (Exception e)
        {
            AttemptedLoginCount++;
            ValidationMessage = $"Error attempting to login: {e.Message}";
        }
    }
}