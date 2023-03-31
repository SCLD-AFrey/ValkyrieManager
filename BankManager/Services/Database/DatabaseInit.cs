using DevExpress.Xpo;
using Microsoft.Extensions.Logging;
using ValkyrieData.Banking;

namespace BankManager.Services.Database;

public class DatabaseInit
{
    private readonly ILogger<DatabaseInit> m_logger;
    private readonly DatabaseInterface m_databaseInterface;
    private readonly EncryptionService m_encryptionService;

    public DatabaseInit(ILogger<DatabaseInit> p_logger, DatabaseInterface p_databaseInterface, EncryptionService p_encryptionService)
    {
        m_logger = p_logger;
        m_databaseInterface = p_databaseInterface;
        m_encryptionService = p_encryptionService;
    }
    
    public void Init()
    {
        m_logger.LogInformation("DatabaseInit.Init() called");
        var uow = m_databaseInterface.ProvisionUnitOfWork();
        using (var users = new XPCollection<User>(uow))
        {
            if (users.Count == 0)
            {
                users.Add(new User(uow)
                {
                    IsRoot = true,
                    UserName = "root",
                    Password = m_encryptionService.GeneratePasswordHash("password", out var salt),
                    PassSalt = salt
                });
                users.Add(new User(uow)
                {
                    IsRoot = false,
                    UserName = "afrey",
                    Password = m_encryptionService.GeneratePasswordHash("password", out salt),
                    PassSalt = salt
                });
            }
            uow.CommitChanges();
        }

        using (var categories = new XPCollection<TransactionCategory>(uow))
        {
            if (categories.Count == 0)
            {
                categories.Add(new TransactionCategory(uow)
                {
                    Title = "Food", IsWithdrawal = true
                });
                categories.Add(new TransactionCategory(uow)
                {
                    Title = "Transport", IsWithdrawal = true
                });
                categories.Add(new TransactionCategory(uow)
                {
                    Title = "Entertainment", IsWithdrawal = true
                });
                categories.Add(new TransactionCategory(uow)
                {
                    Title = "Utilities", IsWithdrawal = true
                });
                categories.Add(new TransactionCategory(uow)
                {
                    Title = "Miscellaneous", IsWithdrawal = true, IsDeposit = true, IsLocked = true
                });
                categories.Add(new TransactionCategory(uow)
                {
                    Title = "Salary", IsDeposit = true, IsLocked = true
                });
                categories.Add(new TransactionCategory(uow)
                {
                    Title = "Income", IsDeposit = true, IsLocked = true
                });
            }
            uow.CommitChanges();
        }
    }
}