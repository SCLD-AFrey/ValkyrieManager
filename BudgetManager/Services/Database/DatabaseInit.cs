using System.IO;
using System.Threading.Tasks;
using BudgetTracker.Services;
using DevExpress.Xpo;
using Microsoft.Extensions.Logging;
using ValkyrieData.Transactions;

namespace BudgetManager.Services.Database;

public class DatabaseInit
{
    private readonly ILogger<DatabaseInit> m_logger;
    private readonly FilesService m_filesService;
    private readonly EncryptionService m_encryptionService;
    private readonly DatabaseInterface  m_databaseInterface;

    public DatabaseInit(ILogger<DatabaseInit> p_logger, FilesService p_filesService, EncryptionService p_encryptionService, DatabaseInterface p_databaseInterface)
    {
        m_logger = p_logger;
        m_filesService = p_filesService;
        m_encryptionService = p_encryptionService;
        m_databaseInterface = p_databaseInterface;
        m_logger.LogDebug("Initializing DatabaseInit");
    }
    public async Task DoFirstTimeSetup()
    {
        // APP DATA
        if (File.Exists(m_filesService.AppDataFilePath))
        {
            var fileInfo = new FileInfo(m_filesService.AppDataFilePath);
            if (fileInfo.Length == 0)
            {
                UnitOfWork uow = m_databaseInterface.ProvisionUnitOfWork();
                var admin = new User(uow)
                {
                    Username = "admin",
                    Password = m_encryptionService.GeneratePasswordHash("password", out var salt),
                    Salt = salt,
                };
                uow.CommitChanges();
            }
        }
    }

}