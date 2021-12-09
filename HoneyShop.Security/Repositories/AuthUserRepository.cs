using System.Linq;
using System.Text;
using HoneyShop.Security.Entities;
using HoneyShop.Security.IRepositories;
using HoneyShop.Security.IServices;
using HoneyShop.Security.Models;

namespace HoneyShop.Security.Repositories
{
    public class AuthUserRepository:IAuthUserRepository
    {
        private readonly AuthDbContext _ctx;

        public AuthUserRepository(AuthDbContext ctx)
        {
            _ctx = ctx;
        }

        public User FindUser(string email)
        {
            var entity = _ctx.AuthUsers
                .FirstOrDefault(user =>
                    email.Equals(user.Email));
            if (entity == null)
            {
                return null;

            }

            return new User
            {
                Id = entity.Id,
                Email = entity.Email,
                HashedPassword = entity.HashedPassword,
                Salt = Encoding.ASCII.GetBytes(entity.Salt)
            };
        }

        public User RegisterUser(string email, string hashedPassword, string salt)
        {
            if (FindUser(email) != null)
            {
                return null;
            }
            
            var entity = _ctx.AuthUsers.Add(new AuthUserEntity
            {
                Email = email,
                Salt = salt,
                HashedPassword = hashedPassword
            }).Entity;
            _ctx.SaveChanges();
            return new User
            {
                Id = entity.Id,
                Email = entity.Email,
                HashedPassword = entity.HashedPassword,
                Salt = Encoding.ASCII.GetBytes(entity.Salt)
            };
        }
    }
}