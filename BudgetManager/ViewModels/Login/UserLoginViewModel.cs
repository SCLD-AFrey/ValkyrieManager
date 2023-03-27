using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Metadata;
using Avalonia.Threading;
using BudgetManager.Models;
using BudgetManager.Models.BackingModels.Login;
using BudgetManager.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Extensions;

namespace BudgetManager.ViewModels.Login;

public class UserLoginViewModel : ViewModelBase
{
    private readonly ILogger<UserLoginViewModel> m_logger;
    private readonly UserLoginModel m_model;
    private readonly IServiceProvider m_serviceProvider;
    [Reactive] public string? Username { get; set; }
    [Reactive] public string? UserPassword { get; set; }
    [Reactive] public bool IsBusy { get; set; }
    public int AttemptedLoginCount { get; set; }
    [Reactive] public string? ValidationError { get; set; }

    public UserLoginViewModel(ILogger<UserLoginViewModel> p_logger,
        IServiceProvider p_serviceProvider,
        UserLoginModel p_model)
    {
        m_logger = p_logger;
        m_serviceProvider = p_serviceProvider;
        m_model = p_model;


        m_logger.LogDebug("Instantiating UserLoginViewModel");

    }

    [DependsOn(nameof(IsBusy))]
    public bool CanClickLogin(object p_parameters)
    {
        return !IsBusy;
    }

    public void ClickCloseCommandCenter()
    {
        m_model.CloseCommandCenter();
    }

    public async Task ClickLogin(Window p_loginWindow)
    {
        try
        {
            ValidationError = string.Empty;
            IsBusy             = true;
            await Task.Delay(m_model.GetLoginDelayTime(AttemptedLoginCount));
            await m_model.AttemptLogin(new LoginCredentials(Username!, UserPassword!));
            m_logger.LogDebug("Login was successful for {Username}", Username);
            //Set save config here
            await m_model.GetPostLoginData();

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                var mainWindow = m_serviceProvider.GetRequiredService<MainWindowView>();
                mainWindow.Show();
                p_loginWindow.Close();
            });

        } catch (Exception e)
        {
            AttemptedLoginCount++;
            IsBusy          = false;
            ValidationError = $"Error attempting to login: {e.Message}";
        }
    }

}