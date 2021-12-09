using HoneyShop.Security.Entities;
using HoneyShop.Security.Models;
using Microsoft.EntityFrameworkCore;

namespace HoneyShop.Security
{
    public class AuthDbContext:DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext>options):base(options)
        {
            
        }
        public DbSet<AuthUserEntity> AuthUsers { get; set; }
    }
}