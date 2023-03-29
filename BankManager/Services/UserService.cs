using System.Linq;
using System.Threading.Tasks;
using BankManager.Models;
using BankManager.Models.Exceptions;
using BankManager.Services.Database;
using DevExpress.Xpo;
using Microsoft.Extensions.Logging;
using ValkyrieData.Banking;

namespace BankManager.Services;

public class UserService
{
    private readonly ILogger<UserService> m_logger;
    private readonly FileService m_fileService;
    private readonly EncryptionService m_encryptionService;
    private readonly SettingsService m_settingsService;
    private readonly DatabaseInterface m_databaseInterface;
    private readonly HistoryService m_historyService;
    
    public UserService(ILogger<UserService> p_logger, FileService p_fileService, EncryptionService p_encryptionService, SettingsService p_settingsService, DatabaseInterface p_databaseInterface, HistoryService p_historyService)
    {
        m_logger = p_logger;
        m_fileService = p_fileService;
        m_encryptionService = p_encryptionService;
        m_settingsService = p_settingsService;
        m_databaseInterface = p_databaseInterface;
        m_historyService = p_historyService;
        m_logger.LogDebug("Initializing UserService");
    }

    private User? CurrentUser { get; set; } = null!;
    
    public void SetCurrentUser(UserInfo p_user)
    {
        m_logger.LogDebug("Setting current user to user '{NewCurrentUser}'", p_user.UserName);
        CurrentUser = GetUserInfo(p_user.Oid);
        m_settingsService.UserSettings.LastLogin = p_user;
        m_settingsService.SaveUserSettings();
        //m_historyService.AddUserHistory(CurrentUser, "Logged in");
    }

    public User? GetCurrentUser()
    {
        return CurrentUser;
    }
    
    public void Init()
    {
        UnitOfWork uow = m_databaseInterface.ProvisionUnitOfWork();
        var rootUser = uow.Query<User>().FirstOrDefault(p_x => p_x.IsRoot);
        if(rootUser == null) CreateRootUser();
        if (m_settingsService.ClientSettings.AutoLogin && m_settingsService.UserSettings.LastLogin != null)
        {
            m_logger.LogDebug("Auto-logging in as user '{LastLogin}'", m_settingsService.UserSettings.LastLogin);
            CurrentUser = GetUserInfo(m_settingsService.UserSettings.LastLogin.Oid);
            //m_historyService.AddUserHistory(CurrentUser, "Auto-logged in");
        }
        else
        {
            m_logger.LogDebug("No auto-login");
            CurrentUser = null;
        }
    }
    public User GetUserInfo(int p_oid)
    {
        var uow = m_databaseInterface.ProvisionUnitOfWork();
        return uow.GetObjectByKey<User>(p_oid);
    }

    public async Task AttemptLoginAsync(LoginObject p_loginObject)
    {
        UnitOfWork uow = m_databaseInterface.ProvisionUnitOfWork();
        var user = uow.Query<User>().FirstOrDefault(p_x => p_x.UserName == p_loginObject.UserName);
        if (user == null)
        {
            m_logger.LogDebug("User '{UserName}' does not exist", p_loginObject.UserName);
            throw new InvalidLoginException($"User '{p_loginObject.UserName}' does not exist");
        }

        if (m_encryptionService.VerifyPassword(p_loginObject.Password, user.Password, user.PassSalt))
        {
            m_logger.LogDebug("User '{UserName}' successfully logged in", p_loginObject.UserName);
            CurrentUser = user;
            m_settingsService.UserSettings.LastLogin = new UserInfo()
            {
                Oid = user.Oid,
                UserName = user.UserName
            };
            m_settingsService.ClientSettings.AutoLogin = p_loginObject.RememberMe;
            m_settingsService.SaveSettings();
        }
        else
        {
            m_logger.LogDebug("User '{UserName}' failed to log in", p_loginObject.UserName);
            //m_historyService.AddUserHistory(user, "Failed Login");
            throw new InvalidLoginException($"User '{p_loginObject.UserName}' failed to log in");
        }
    }

    private void CreateRootUser()
    {
        UnitOfWork uow = m_databaseInterface.ProvisionUnitOfWork();
        var root = new User(uow)
        {
            IsRoot = true,
            UserName = "root",
            Password = m_encryptionService.GeneratePasswordHash("password", out var salt),
            PassSalt = salt
        };
        uow.CommitChanges();
        //m_historyService.AddUserHistory(root, "User Created Automatically");
    }
}