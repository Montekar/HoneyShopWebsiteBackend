namespace HoneyShop.Security.Helpers
{
    public interface IUserAuthenticator
    {
        bool Login(string username, string password, out string token);

        bool CreateUser(string username, string password);
    }
}