using ValkyrieData.Banking;

namespace BankManager.Models.Settings;

public class UserSettings
{
    public string AesKey { get; set; } = string.Empty;
    public UserInfo? LastLogin { get; set; }
}