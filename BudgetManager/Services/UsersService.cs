using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ValkyrieData.Transactions;

namespace BudgetManager.Services;

public class UsersService
{
    private readonly ILogger<UsersService> m_logger;

    public UsersService(ILogger<UsersService> p_logger)
    {
        m_logger = p_logger;
        m_logger.LogDebug("Instantiating UsersService");
    }

    public void SetCurrentUser(User p_user)
    {
        m_logger.LogDebug("Setting current user to user '{NewCurrentUser}'", p_user.Username);
        
        CurrentUser = p_user;
    }
    
    public User?          CurrentUser                       { get; private set; }
    public DateTimeOffset LastSuccessfulLoginDate           { get; set; }
    public int            FailedLoginAttemptsSinceLastLogin { get; set; }

    public async Task ChangePassword(string p_userPassword, string p_newPassword)
    {
        //Change password
    }

}