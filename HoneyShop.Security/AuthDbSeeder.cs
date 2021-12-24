using System.Text;
using HoneyShop.Security.Entities;
using HoneyShop.Security.IServices;
using HoneyShop.Security.Models;

namespace HoneyShop.Security
{
    public class AuthDbSeeder :IAuthDbSeeder
    {
        private readonly AuthDbContext _ctx;
        private readonly ISecurityService _securityService;

        public AuthDbSeeder(AuthDbContext ctx,ISecurityService securityService)
        {
            _ctx = ctx;
            _securityService = securityService;
        }

        public void SeedDevelopment()
        {
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();

            var salt = "123#!";
            
            _ctx.AuthUsers.Add(new AuthUserEntity
            {
                Salt = salt,
                HashedPassword = _securityService.HashedPassword("user123",Encoding.ASCII.GetBytes(salt)),
                Email = "ouruser@gmail.com",
                Role="User"
            });
            _ctx.AuthUsers.Add(new AuthUserEntity
            {
                Salt = salt,
                HashedPassword = _securityService.HashedPassword("admin123",Encoding.ASCII.GetBytes(salt)),
                Email = "ouradmin@gmail.com",
                Role = "Admin"
            });
            
            
            _ctx.SaveChanges();
        }

        public void SeedProduction()
        {
            _ctx.Database.EnsureCreated();

            var salt = "123#!";

            _ctx.AuthUsers.Add(new AuthUserEntity
            {
                Salt = salt,
                HashedPassword = _securityService.HashedPassword("user123", Encoding.ASCII.GetBytes(salt)),
                Email = "ouruser@gmail.com",
                Role = "User"
            });
            _ctx.AuthUsers.Add(new AuthUserEntity
            {
                Salt = salt,
                HashedPassword = _securityService.HashedPassword("admin123", Encoding.ASCII.GetBytes(salt)),
                Email = "ouradmin@gmail.com",
                Role = "Admin"
            });


            _ctx.SaveChanges();
        }
    }
}