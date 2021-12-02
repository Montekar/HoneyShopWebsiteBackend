using System.Linq;
using System.Runtime.InteropServices;
using HoneyShop.Core.Models;
using HoneyShop.Domain;

namespace HoneyShop.Security.Helpers
{
    public class UserAuthenticator:IUserAuthenticator
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationHelper _authenticationHelper;

        public UserAuthenticator(IUserRepository userRepository,IAuthenticationHelper authenticationHelper)
        {
            _userRepository = userRepository;
            _authenticationHelper = authenticationHelper;
        }
        
        public bool Login(string username, string password, out string token)
        {
            User user = _userRepository.GetAllUsers().FirstOrDefault(user => user.Email.Equals(username));

            if (user == null)
            {
                token = null;
                return false;
            }

            if (!_authenticationHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                token = null;
                return false;
            }

            token = _authenticationHelper.GenerateToken(user);
            return true;
        }

        public bool CreateUser(string username, string password)
        {
            User user = _userRepository.GetAllUsers().FirstOrDefault(user => user.Email.Equals(username));
            
            if (user != null)
            {
                return false;
            }
            byte[] salt;
            byte[] passwordHash;
            _authenticationHelper.CreatePasswordHash(password, out passwordHash, out salt);
            
            user = new User()
            {
                Email = username,
                Role = "User",
                PasswordHash = passwordHash,
                PasswordSalt = salt
            };

            _userRepository.CreateUser(user);
            
            return true;
        }
    }
}