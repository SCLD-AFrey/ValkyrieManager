using System;
using System.Threading.Tasks;
using BudgetManager.Services;
using BudgetTracker.Services;
using Microsoft.Extensions.Logging;

namespace BudgetManager.Models.BackingModels.Login;

public class UserLoginModel
{
    
    private readonly ILogger<UserLoginModel>          m_logger;
    private readonly AuthenticationService            m_authenticationService;
    private readonly FilesService                    m_filesService;
    private readonly UsersService                    m_usersService;

    public UserLoginModel(ILogger<UserLoginModel> p_logger, AuthenticationService p_authenticationService, FilesService p_filesService, UsersService p_usersService)
    {
        m_logger = p_logger;
        m_authenticationService = p_authenticationService;
        m_filesService = p_filesService;
        m_usersService = p_usersService;

        m_logger.LogDebug("Instantiating UserLoginModel");
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

    public async Task SaveClientConfig(UserInfo p_userInfo)
    {
        //Save client config
    }

    public void CloseCommandCenter()
    {
        Environment.Exit(0);
    }

    public async Task AttemptLogin(LoginCredentials p_loginObject)
    {
        await m_authenticationService.AttemptLoginAsync(p_loginObject);
    }

    public async Task GetPostLoginData()
    {
        await m_authenticationService.GetPostLoginDataAsync();
    }

    public async Task ChangePassword(string p_userPassword, string p_newPassword)
    {
        await m_usersService.ChangePassword(p_userPassword, p_newPassword);
    }
}