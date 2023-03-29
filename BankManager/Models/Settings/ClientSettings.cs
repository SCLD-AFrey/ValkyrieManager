using System;

namespace BankManager.Models.Settings;

public class ClientSettings
{
    public string AppTitle { get; set; } = String.Empty;
    public bool AutoLogin { get; set; } = false;
}