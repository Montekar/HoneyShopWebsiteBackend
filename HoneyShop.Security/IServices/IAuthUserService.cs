using HoneyShop.Security.Models;

namespace HoneyShop.Security.IServices
{
    public interface IAuthUserService
    {
        User GetUser(string email);
        User RegisterUser(string email, string hashedPasswordFromPlain, string salt);
    }
}