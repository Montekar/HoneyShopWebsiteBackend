using HoneyShop.Security.IRepositories;
using HoneyShop.Security.IServices;
using HoneyShop.Security.Models;

namespace HoneyShop.Security.Services
{
    public class AuthUserService :IAuthUserService
    {
        private readonly IAuthUserRepository _authUserRepository;

        public AuthUserService(IAuthUserRepository authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }
        public User GetUser(string email)
        {
            return _authUserRepository.FindUser(email);
        }

        public User RegisterUser(string email, string hashedPassword, string salt, bool isAdmin)
        {
            return _authUserRepository.RegisterUser(email, hashedPassword, salt,isAdmin);
        }
    }
}