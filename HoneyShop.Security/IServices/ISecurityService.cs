using HoneyShop.Security.Models;

namespace HoneyShop.Security.IServices
{
    public interface ISecurityService
    {
        JwtToken GenerateJwtToken(string email, string password);
        string HashedPassword(string plainTextPassword, byte[] userSalt);
        User RegisterUser(string email, string password,bool isAdmin);
    }
}