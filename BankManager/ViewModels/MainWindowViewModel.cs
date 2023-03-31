using System;
using System.Text;
using System.Windows.Input;
using Avalonia.Collections;
using BankManager.Models;
using BankManager.Models.Loggins;
using BankManager.Services;
using BankManager.Views.MainApplication;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia.ViewModels.Commands;
using Microsoft.Extensions.Logging;
using ReactiveUI.Fody.Helpers;
using TextCopy;
using MessageBoxAvaloniaEnums = MessageBox.Avalonia.Enums;

namespace BankManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly ILogger<MainWindowViewModel> m_logger;
    private readonly IServiceProvider m_serviceProvider;
    private readonly MainWindowModel m_model;
    private readonly UserService m_userService;
    
    
    public ICommand ExitCommand { get; set; }
    public ICommand OpenHomeViewCommand { get; set; }
    public ICommand OpenTransactionsViewCommand { get; set; }
    public ICommand OpenBalancesViewCommand { get; set; }
    public ICommand OpenSettingsViewCommand { get; set; }
    
    [Reactive] public object? CurrentView { get; set; }
    [Reactive] public AvaloniaList<ConsoleLogMessage> Messages         { get; set; } = new ();
    [Reactive] public AvaloniaList<ConsoleLogMessage> SelectedMessages { get; set; } = new ();
    [Reactive] public string                          StatusUsername    { get; set; }
    
    
    public MainWindowViewModel(ILogger<MainWindowViewModel> p_logger, MainWindowModel p_model, IServiceProvider p_serviceProvider, UserService p_userService)
    {
        m_logger = p_logger;
        m_model = p_model;
        m_serviceProvider = p_serviceProvider;
        m_userService = p_userService;
        m_logger.LogDebug("Initializing MainWindowViewModel");

        ExitCommand = new RelayCommand(Exit);
        OpenHomeViewCommand = new RelayCommand(OpenHomeView);
        OpenTransactionsViewCommand = new RelayCommand(OpenTransactionsView);
        OpenBalancesViewCommand = new RelayCommand(OpenBalancesView);
        OpenSettingsViewCommand = new RelayCommand(OpenSettingsView);
        
        CurrentView = m_serviceProvider.GetService(typeof(HomeView));

        CollectionSink.SetCollection(Messages);
        m_logger.LogInformation("Logged in as {UserName}", m_userService.GetCurrentUser()!.UserName);
    }

    private async void Exit(object p_obj)
    {
        m_logger.LogInformation("Exiting Application");
        var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(
            new MessageBoxStandardParams
            {
                Icon = Icon.Question,
                ButtonDefinitions = MessageBoxAvaloniaEnums.ButtonEnum.YesNoCancel,
                ContentTitle = "Logout",
                ContentHeader = "Would you like to log out of the application?",
                ContentMessage = "Seriously, do you want to log out?"
            });
        var answer = await messageBoxStandardWindow.Show();

        switch (answer)
        {
            case ButtonResult.Yes:
                m_logger.LogInformation("User {UserName} logged out", m_userService.GetCurrentUser()!.UserName);
                m_userService.SetCurrentUser(new UserInfo());
                m_model.CloseApplication();
                break;
            case ButtonResult.No:
                m_model.CloseApplication();
                break;
            case ButtonResult.Cancel:
                m_logger.LogInformation("Exiting Application - Cancelled");
                break;
        }
    }

    
    private void OpenHomeView(object p_obj)
    {
        m_logger.LogInformation("Opening HomeView");
        CurrentView = m_serviceProvider.GetService(typeof(HomeView));
    }
    private void OpenTransactionsView(object p_obj)
    {
        m_logger.LogInformation("Opening TransactionsView");
        CurrentView = m_serviceProvider.GetService(typeof(TransactionsView));
    }
    private void OpenBalancesView(object p_obj)
    {
        m_logger.LogInformation("Opening BalanceView");
        CurrentView = m_serviceProvider.GetService(typeof(BalanceView));
    }
    private void OpenSettingsView(object p_obj)
    {
        m_logger.LogInformation("Opening SettingsView");
        CurrentView = m_serviceProvider.GetService(typeof(SettingsView));
    }
    
    public void CopyMessages()
    {
        var selectedText = new StringBuilder();

        foreach ( var message in SelectedMessages )
        {
            selectedText.AppendLine(message.Text);
        }
            
        ClipboardService.SetText(selectedText.ToString());
    }


}