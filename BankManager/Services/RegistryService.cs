using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System;
using System.Security.Cryptography;

namespace BankManager.Services;

public class RegistryService
{
    private static ILogger<RegistryService> m_logger;

    public RegistryService(ILogger<RegistryService> p_logger)
    {
        m_logger = p_logger;
        m_logger.LogInformation("RegistryService initialized");
        if (string.IsNullOrEmpty(AesKey))
        {
            LoadAesKey();
        }
    }

    private const string KeyName = @"SOFTWARE\ValkyrieManager";
    public string AesKey { get; set; } = String.Empty;

    public static void InitAesKey()
    {
        m_logger.LogInformation("Initializing AesKey in registry");
#pragma warning disable CA1416
        var key = Registry.CurrentUser.CreateSubKey(KeyName);
        key.SetValue("AesKey", GenerateAesKey());
        key.Close();
#pragma warning restore CA1416
    }
    
    public void LoadAesKey()
    {   
#pragma warning disable CA1416
        var key = Registry.CurrentUser.OpenSubKey(KeyName);
        if (key == null)
        {
            InitAesKey();
            Registry.CurrentUser.OpenSubKey(KeyName);
        }
        m_logger.LogInformation("Loading AesKey from registry");
        AesKey = key.GetValue("AesKey")!.ToString()!;
        key.Close();
#pragma warning restore CA1416 
    }

    private static string GenerateAesKey()
    {
        m_logger.LogInformation("Generating AesKey");
        using var aes = Aes.Create();
        return Convert.ToBase64String(aes.Key);
    }
}