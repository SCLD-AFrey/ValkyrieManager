﻿namespace BankManager.Models;

public class LoginObject
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool   RememberMe { get; set; } = false;
}