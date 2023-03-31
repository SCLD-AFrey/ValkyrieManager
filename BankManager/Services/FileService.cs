using System.IO;
using System.Net;
using System.Text.Json;
using BankManager.Models.Settings;
using Microsoft.Extensions.Logging;

namespace BankManager.Services;

public class FileService
{
    private const string CSolutionDataFolderName = ".ValkyrieManager";
    private const string CAppDataFolderName = "BankManager";
    private readonly ILogger<FileService> m_logger;
    private readonly EncryptionService m_encryptionService;
    
    public string SolutionRoot { get; } 
    public string ApplicationRoot { get; } 
    public string ClientSettingsFile { get; }
    public string UserSettingsFile { get; }
    public string LogsFile { get; }
    public string DatabaseFile { get; }
    public string AesKeyFile { get; }
    
    

    public FileService(ILogger<FileService> p_logger, EncryptionService p_encryptionService)
    {
        m_logger = p_logger;
        m_encryptionService = p_encryptionService;
        
        SolutionRoot = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData),
            CSolutionDataFolderName);
        
        ApplicationRoot = Path.Combine(SolutionRoot, CAppDataFolderName);
        
        ClientSettingsFile = Path.Combine(ApplicationRoot, "settings.ini");
        UserSettingsFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), CAppDataFolderName, "user-settings.enc");
        AesKeyFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), CAppDataFolderName, "aes");
        LogsFile = Path.Combine(ApplicationRoot, "logs", "events.log");
        DatabaseFile = Path.Combine(ApplicationRoot, "data", "bankmanager.db");
        
        
        
    }

    public void CreateDirectories()
    {
        m_logger.LogInformation("Creating SolutionRoot... {SolutionRoot}", SolutionRoot);
        Directory.CreateDirectory(SolutionRoot);
        m_logger.LogInformation("Creating ApplicationRoot... {ApplicationRoot}", ApplicationRoot);
        Directory.CreateDirectory(ApplicationRoot);

        m_logger.LogInformation("Creating ClientSettingsFile... {ClientSettingsFile}", ClientSettingsFile);
        Directory.CreateDirectory(Path.GetDirectoryName(ClientSettingsFile)!);
        m_logger.LogInformation("Creating UserSettingsFile... {UserSettingsFile}", UserSettingsFile);
        Directory.CreateDirectory(Path.GetDirectoryName(UserSettingsFile)!);
        m_logger.LogInformation("Creating AesKeyFile... {AesKeyFile}", AesKeyFile);
        Directory.CreateDirectory(Path.GetDirectoryName(AesKeyFile)!);
        m_logger.LogInformation("Creating LogsFile... {LogsFile}", LogsFile);
        Directory.CreateDirectory(Path.GetDirectoryName(LogsFile)!);
        m_logger.LogInformation("Creating DatabaseFile... {DatabaseFile}", DatabaseFile);
        Directory.CreateDirectory(Path.GetDirectoryName(DatabaseFile)!);
    }

    public void InitSettingsFiles()
    {
        if(!File.Exists(ClientSettingsFile))
        {
            m_logger.LogInformation("Create Necessary Files... {Filepath}", ClientSettingsFile);
            var jsonString = JsonSerializer.Serialize(new ClientSettings()
            {
                AppTitle = "Valkyrie Banking Manager",
                AutoLogin = false
            });;
            File.WriteAllText(ClientSettingsFile, jsonString);
        }
        if(!File.Exists(UserSettingsFile))
        {
            var settings = new UserSettings();
            m_logger.LogInformation("Create Necessary Files... {Filepath}", UserSettingsFile);
            string jsonString = JsonSerializer.Serialize(settings);;
            //jsonString = m_encryptionService.EncryptString(jsonString);
            File.WriteAllText(UserSettingsFile, jsonString);
        }
    }
}