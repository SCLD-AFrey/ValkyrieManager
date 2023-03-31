using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using BankManager.Models.Settings;
using Microsoft.Extensions.Logging;

namespace BankManager.Services;

public class SettingsService
{
    private readonly ILogger<SettingsService> m_logger;
    private readonly FileService m_fileService;
    private readonly EncryptionService m_encryptionService;

    public UserSettings UserSettings { get; set; } = new UserSettings();
    public ClientSettings ClientSettings { get; set; } = new ClientSettings();

    public SettingsService(ILogger<SettingsService> p_logger, FileService p_fileService, EncryptionService p_encryptionService)
    {
        m_logger = p_logger;
        m_fileService = p_fileService;
        m_encryptionService = p_encryptionService;
        m_logger.LogInformation("SettingsService initialized");
    }
    
    public async void LoadSettings()
    {
        m_logger.LogInformation("Loading settings...");
        await LoadClientSettings();
        await LoadUserSettings();
    }

    public async Task SaveSettings()
    {
        m_logger.LogInformation("Saving settings...");
        await SaveClientSettings();
        await SaveUserSettings();
    }

    private async Task SaveClientSettings()
    {
        m_logger.LogInformation("Save Client Settings... {Filepath}", m_fileService.ClientSettingsFile);
        var jsonString = JsonSerializer.Serialize(ClientSettings);
        await File.WriteAllTextAsync(m_fileService.ClientSettingsFile, jsonString);
    }
    private async Task SaveUserSettings()
    {
        m_logger.LogInformation("Save User Settings... {Filepath}", m_fileService.UserSettingsFile);
        var jsonString = JsonSerializer.Serialize(UserSettings);
        //jsonString = m_encryptionService.EncryptString(jsonString);
        await File.WriteAllTextAsync(m_fileService.UserSettingsFile, jsonString);
    }

    private async Task LoadClientSettings()
    {
        using StreamReader rdr = new(m_fileService.ClientSettingsFile);
        var json = await rdr.ReadToEndAsync();
        ClientSettings = JsonSerializer.Deserialize<ClientSettings>(json)!;
    }

    private async Task LoadUserSettings()
    {
        using StreamReader rdr = new(m_fileService.UserSettingsFile);
        var json = await rdr.ReadToEndAsync();
        //json = m_encryptionService.DecryptString(json);
        UserSettings = JsonSerializer.Deserialize<UserSettings>(json)!;
    }
}