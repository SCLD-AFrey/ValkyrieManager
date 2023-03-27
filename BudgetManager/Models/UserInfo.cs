namespace BudgetManager.Models;

public class UserInfo
{
    public int Oid { get; set; } = 0;
    public string Username { get; set; } = string.Empty;
    public bool IsAdmin { get; set; } = false;
}