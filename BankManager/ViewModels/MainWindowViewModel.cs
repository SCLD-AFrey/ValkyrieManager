using System;
using System.Text;
using System.Windows.Input;
using Avalonia.Collections;
using BankManager.Models;
using BankManager.Models.Loggins;
using BankManager.Views.MainApplication;
using MessageBox.Avalonia.ViewModels.Commands;
using Microsoft.Extensions.Logging;
using ReactiveUI.Fody.Helpers;
using TextCopy;

namespace BankManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly ILogger<MainWindowViewModel> m_logger;
    private readonly IServiceProvider m_serviceProvider;
    private readonly MainWindowModel m_model;
    
    
    public ICommand ExitCommand { get; set; }
    public ICommand OpenHomeViewCommand { get; set; }
    public ICommand OpenTransactionsViewCommand { get; set; }
    public ICommand OpenBalancesViewCommand { get; set; }
    public ICommand OpenSettingsViewCommand { get; set; }
    
    [Reactive] public object? CurrentView { get; set; }
    [Reactive] public AvaloniaList<ConsoleLogMessage> Messages         { get; set; } = new ();
    [Reactive] public AvaloniaList<ConsoleLogMessage> SelectedMessages { get; set; } = new ();
    [Reactive] public string                          StatusUsername    { get; set; }
    
    
    public MainWindowViewModel(ILogger<MainWindowViewModel> p_logger, MainWindowModel p_model, IServiceProvider p_serviceProvider)
    {
        m_logger = p_logger;
        m_model = p_model;
        m_serviceProvider = p_serviceProvider;
        m_logger.LogDebug("Initializing MainWindowViewModel");

        ExitCommand = new RelayCommand(Exit);
        OpenHomeViewCommand = new RelayCommand(OpenHomeView);
        OpenTransactionsViewCommand = new RelayCommand(OpenTransactionsView);
        OpenBalancesViewCommand = new RelayCommand(OpenBalancesView);
        OpenSettingsViewCommand = new RelayCommand(OpenSettingsView);
        
        CurrentView = m_serviceProvider.GetService(typeof(HomeView));
    }

    private void Exit(object p_obj)
    {
        m_model.CloseApplication();
    }

    
    private void OpenHomeView(object p_obj)
    {
        CurrentView = m_serviceProvider.GetService(typeof(HomeView));
    }
    private void OpenTransactionsView(object p_obj)
    {
        CurrentView = m_serviceProvider.GetService(typeof(TransactionsView));
    }
    private void OpenBalancesView(object p_obj)
    {
        CurrentView = m_serviceProvider.GetService(typeof(BalanceView));
    }
    private void OpenSettingsView(object p_obj)
    {
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