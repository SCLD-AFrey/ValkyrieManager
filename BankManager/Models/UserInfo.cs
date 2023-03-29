namespace BankManager.Models;

public class UserInfo
{
    public int Oid { get; set; } = 0;
    public string UserName { get; set; }
    
    public UserInfo(int p_oid, string p_userName)
    {
        Oid = p_oid;
        UserName = p_userName;
    }
    
}