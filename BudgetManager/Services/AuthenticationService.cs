using System;
using System.Linq;
using System.Threading.Tasks;
using BudgetManager.Models;
using BudgetManager.Services.Database;
using BudgetTracker.Services;
using DevExpress.Xpo;
using DevExpress.Xpo.Helpers;
using Microsoft.Extensions.Logging;
using ValkyrieData.Transactions;

namespace BudgetManager.Services;

public class AuthenticationService
{
    private readonly ILogger<AuthenticationService> m_logger;
    private readonly UsersService m_usersService;
    private readonly EncryptionService m_encryptionService;
    private readonly DatabaseInterface m_databaseInterface;

    public AuthenticationService(ILogger<AuthenticationService> p_logger, EncryptionService p_encryptionService, UsersService p_usersService, DatabaseInterface p_databaseInterface)
    {
        m_logger = p_logger;
        m_encryptionService = p_encryptionService;
        m_usersService = p_usersService;
        m_databaseInterface = p_databaseInterface;
        m_logger.LogDebug("Initializing AuthenticationService");
    }

    public async Task AttemptLoginAsync(LoginCredentials p_loginObject)
    {
        if(string.IsNullOrEmpty(p_loginObject.Username) || string.IsNullOrEmpty(p_loginObject.Password))
        { 
            throw new Exception("Username or password is empty");
        }
        
        using var unitOfWork = m_databaseInterface.ProvisionUnitOfWork();
        var user = unitOfWork.Query<User>().FirstOrDefault(p_x => string.Equals(p_x.Username, p_loginObject.Username, StringComparison.CurrentCultureIgnoreCase));
        if (user == null)
        {
            throw new Exception("Username does not exist");
        }

        if (!m_encryptionService.VerifyPassword(p_loginObject.Password, user.Password, user.Salt))
        {
            throw new Exception("Password is incorrect");
        }
        m_usersService.SetCurrentUser(user);
    }
    public async Task GetPostLoginDataAsync()
    {
        //Get post login data
    }
}