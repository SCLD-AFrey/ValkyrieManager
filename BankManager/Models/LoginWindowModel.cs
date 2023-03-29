using System;
using System.Threading.Tasks;
using BankManager.Services;
using Microsoft.Extensions.Logging;

namespace BankManager.Models;

public class LoginWindowModel
{
    private readonly ILogger<LoginWindowModel>          m_logger;
    private readonly UserService                       m_userService;
    private readonly SettingsService              m_settingsService;


    public LoginWindowModel(ILogger<LoginWindowModel> p_logger, SettingsService p_settingsService, UserService p_userService)
    {
        m_logger = p_logger;
        m_settingsService = p_settingsService;
        m_userService = p_userService;
        m_logger.LogDebug("Initializing LoginWindowModel");
    }

    public int GetLoginDelayTime(int p_attemptedLoginCount)
    {
        return p_attemptedLoginCount switch
        {
            >= 3 and < 6 => 3000,
            >= 6 and < 8 => 6000,
            >= 8         => 10000,
            _            => 0
        };
    }

    public void CloseApplication()
    {
        Environment.Exit(0);
    }

    public async Task AttemptLogin(string p_userName, string p_password, bool p_rememberMe)
    {
        await m_userService.AttemptLoginAsync(new LoginObject()
        {
            UserName = p_userName,
            Password = p_password,
            RememberMe = p_rememberMe
        });
    }
}