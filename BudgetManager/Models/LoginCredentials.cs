namespace BudgetManager.Models;

public class LoginCredentials
{
    public LoginCredentials(string p_userName, string p_password)
    {
        Username      = p_userName;
        Password      = p_password;
    }
    public string Username { get; set; }
    public string Password { get; set; }
}