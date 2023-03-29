using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using BankManager.Models;
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
        LoadClientSettings();
        LoadUserSettings();
    }

    public async void SaveSettings()
    {
        m_logger.LogInformation("Saving settings...");
        SaveClientSettings();
        SaveUserSettings();
    }

    public async void SaveClientSettings()
    {
        m_logger.LogInformation("Save Client Settings... {Filepath}", m_fileService.ClientSettingsFile);
        var jsonString = JsonSerializer.Serialize(ClientSettings);
        File.WriteAllText(m_fileService.ClientSettingsFile, jsonString);
    }
    public async void SaveUserSettings()
    {
        m_logger.LogInformation("Save User Settings... {Filepath}", m_fileService.UserSettingsFile);
        var jsonString = JsonSerializer.Serialize(UserSettings);
        //var encryptedJson = m_encryptionService.EncryptString(jsonString);
        File.WriteAllText(m_fileService.UserSettingsFile, jsonString);
    }

    public async void LoadClientSettings()
    {
        using StreamReader rdr = new(m_fileService.ClientSettingsFile);
        var json = rdr.ReadToEnd();
        ClientSettings = JsonSerializer.Deserialize<ClientSettings>(json)!;
    }

    public async void LoadUserSettings()
    {
        using StreamReader rdr = new(m_fileService.UserSettingsFile);
        var json = rdr.ReadToEnd();
        //var decryptedJson = m_encryptionService.DecryptString(json);
        UserSettings = JsonSerializer.Deserialize<UserSettings>(json)!;
    }
}