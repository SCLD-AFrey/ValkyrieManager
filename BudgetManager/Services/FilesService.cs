using System.IO;
using Microsoft.Extensions.Logging;

namespace BudgetTracker.Services;

public class FilesService
{
    private const string c_solutionDataFolderName = ".valkyriemanager";
    private const string c_appDataFolderName = "budget";
    private readonly ILogger<FilesService> m_logger;
    private readonly EncryptionService m_encryptionService;

    public FilesService(ILogger<FilesService> p_logger, EncryptionService p_encryptionService)
    {
        m_logger = p_logger;
        m_encryptionService = p_encryptionService;

        SolutionDataFolderName =
            Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData),
                c_solutionDataFolderName);

        UserSettingsFolderPath =
            Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData),
                c_solutionDataFolderName);

        AppDataPath = Path.Combine(SolutionDataFolderName, c_appDataFolderName);

        LogsFilePath = Path.Combine(AppDataPath, "logs", "events.log");
        ClientSettingsFilePath = Path.Combine(AppDataPath ,"client-settings.enc");
        UserSettingsFilePath = Path.Combine(UserSettingsFolderPath, c_appDataFolderName, "user-settings.enc");
        AppDataFilePath = Path.Combine(AppDataPath, "data", "budget.db");
    }

    public void CreateDirectories()
    {
        
        if(Directory.Exists(SolutionDataFolderName) == false)
        {
            m_logger.LogInformation("Creating SolutionDataFolderName... {SolutionDataFolderName}", SolutionDataFolderName);
            Directory.CreateDirectory(SolutionDataFolderName);
        }
        if(Directory.Exists(AppDataPath) == false)
        {
            m_logger.LogInformation("Creating AppDataPath... {AppDataPath}", AppDataPath);
            Directory.CreateDirectory(AppDataPath);
        }
        
        m_logger.LogInformation("Create Necessary Directories...");
        
        m_logger.LogInformation("                            ... {Filepath}", Path.GetDirectoryName(AppDataPath));
        Directory.CreateDirectory(Path.GetDirectoryName(AppDataPath) ?? string.Empty);
        m_logger.LogInformation("                            ... {Filepath}", Path.GetDirectoryName(LogsFilePath));
        Directory.CreateDirectory(Path.GetDirectoryName(LogsFilePath) ?? string.Empty);
        m_logger.LogInformation("                            ... {Filepath}", Path.GetDirectoryName(ClientSettingsFilePath));
        Directory.CreateDirectory(Path.GetDirectoryName(ClientSettingsFilePath) ?? string.Empty);
        m_logger.LogInformation("                            ... {Filepath}", Path.GetDirectoryName(UserSettingsFilePath));
        Directory.CreateDirectory(Path.GetDirectoryName(UserSettingsFilePath) ?? string.Empty);
        m_logger.LogInformation("                            ... {Filepath}", Path.GetDirectoryName(AppDataFilePath));
        Directory.CreateDirectory(Path.GetDirectoryName(AppDataFilePath) ?? string.Empty);
        
        if(!File.Exists(ClientSettingsFilePath))
        {
            m_logger.LogInformation("Create Necessary Files... {Filepath}", ClientSettingsFilePath);
            string jsonString = "[]";
            string encryptedJson = m_encryptionService.EncryptString(jsonString);
            File.WriteAllText(ClientSettingsFilePath, encryptedJson);
        }
            
        if(!File.Exists(UserSettingsFilePath))
        {
            m_logger.LogInformation("Create Necessary Files... {Filepath}", UserSettingsFilePath);
            string jsonString = "[]";
            string encryptedJson = m_encryptionService.EncryptString(jsonString);
            File.WriteAllText(UserSettingsFilePath, encryptedJson);
        }
        
    }

    private string SolutionDataFolderName { get; set; }
    private string UserSettingsFolderPath { get; set; }
    
    public string AppDataPath { get; set; }
    public string AppDataFilePath { get; set; }
    
    public string LogsFilePath { get; }
    public string ClientSettingsFilePath { get; }
    public string UserSettingsFilePath { get; }
}