using System.Collections.Generic;
using System.IO;
using System.Linq;
using HoneyShop.Core.Models;
using HoneyShop.Domain;

namespace HoneyShop.DataAccess.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly HoneyContext _ctx;

        public UserRepository(HoneyContext ctx)
        {
            _ctx = ctx ?? throw new InvalidDataException("User Repository Must Have a HoneyContext");
        }

        public List<User> GetAllUsers()
        {
            return _ctx.Users.Select(ue => new User
            {
                Id = ue.Id,
                Role = ue.Role,
                Username = ue.Username,
                PasswordHash = ue.PasswordHash,
                PasswordSalt = ue.PasswordSalt
            }).ToList();
        }

        public User GetUser(int id)
        {
            return _ctx.Users.Select(ue => new User
            {
                Id = ue.Id,
                Role = ue.Role,
                Username = ue.Username,
                PasswordHash = ue.PasswordHash,
                PasswordSalt = ue.PasswordSalt
            }).FirstOrDefault(user => user.Id == id);
        }

        public User CreateUser(User user)
        {
            var entity = new UserEntity
            {
                Id = user.Id,
                Role = user.Role,
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt
            };
            var savedEntity = _ctx.Users.Add(entity).Entity;
            _ctx.SaveChanges();
            return new User
            {
                Id = savedEntity.Id,
                Role = savedEntity.Role,
                Username = savedEntity.Username,
                PasswordHash = savedEntity.PasswordHash,
                PasswordSalt = savedEntity.PasswordSalt
            };
        }

        public User EditUser(User user)
        {
            var entity = new UserEntity
            {
                Id = user.Id,
                Role = user.Role,
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt
            };
            var savedEntity = _ctx.Users.Update(entity).Entity;
            _ctx.SaveChanges();
            
            return new User
            {
                Id = savedEntity.Id,
                Role = savedEntity.Role,
                Username = savedEntity.Username,
                PasswordHash = savedEntity.PasswordHash,
                PasswordSalt = savedEntity.PasswordSalt
            };
        }

        public User DeleteUser(int id)
        {
            var entity = _ctx.Users.FirstOrDefault(ue => ue.Id == id);
            var deletedEntity = _ctx.Remove(entity).Entity;
            _ctx.SaveChanges();
            return new User
            {
                Id = deletedEntity.Id,
                Role = deletedEntity.Role,
                Username = deletedEntity.Username,
                PasswordHash = deletedEntity.PasswordHash,
                PasswordSalt = deletedEntity.PasswordSalt
            };
        }
    }
}