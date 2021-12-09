using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using HoneyShop.Security.IServices;
using HoneyShop.Security.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HoneyShop.Security.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly AuthDbContext _ctx;
        private readonly IAuthUserService _authUserService;

        public SecurityService(IConfiguration configuration, AuthDbContext ctx,IAuthUserService authUserService)
        {
            _ctx = ctx;
            Configuration = configuration;
            _authUserService = authUserService;
        }

        private IConfiguration Configuration { get; }

        public JwtToken GenerateJwtToken(string email, string password)
        {
            var user = _authUserService.GetUser(email);

            if (!Authenticate(password,user))
            {
                throw new AuthenticationException("Email or Password not correct");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                Configuration["Jwt:Issuer"],
                Configuration["Jwt:Audience"],
                null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            
            return new JwtToken
            {
                Jwt = new JwtSecurityTokenHandler().WriteToken(token),
                Message = "Ok"
            };
        }
        private bool Authenticate(string plainTextPassword, User user)
        {
            if (user == null || user.HashedPassword.Length <= 0 || user.Salt.Length <=0)
            {
                return false;
            }
            

            var hashedPasswordFromPlain = HashedPassword(plainTextPassword,user.Salt);

            return user.HashedPassword.Equals(hashedPasswordFromPlain);
        }

         public string HashedPassword(string plainTextPassword, byte[] userSalt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: plainTextPassword,
                salt: userSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }

         public User RegisterUser(string email, string password)
         {
             var salt = "123#$%^";
             var hashedPasswordFromPlain = HashedPassword(password,Encoding.ASCII.GetBytes(salt));

             return _authUserService.RegisterUser(email,hashedPasswordFromPlain,salt);
         }
    }
}