using System;
using BankManager.Services;
using DevExpress.Xpo.Logger;
using Microsoft.Extensions.Logging;

namespace BankManager.Models;

public class MainWindowModel
{
    private readonly ILogger<MainWindowModel> m_logger;
    private readonly UserService m_userService;

    public MainWindowModel(ILogger<MainWindowModel> p_logger, UserService p_userService)
    {
        m_logger = p_logger;
        m_userService = p_userService;
    }

    public void CloseApplication()
    {
        Environment.Exit(0);
    }
}