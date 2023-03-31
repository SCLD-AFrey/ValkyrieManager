using System;
using Microsoft.Extensions.Logging;

namespace BankManager.Models;

public class MainWindowModel
{
    private readonly ILogger<MainWindowModel> m_logger;

    public MainWindowModel(ILogger<MainWindowModel> p_logger)
    {
        m_logger = p_logger;
    }

    public void CloseApplication()
    {
        Environment.Exit(0);
    }
}