using System;
using HoneyShop.Core.Models;

namespace HoneyShop.Security.Helpers
{
    public interface IAuthenticationHelper
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        string GenerateToken(User user);
    }
}