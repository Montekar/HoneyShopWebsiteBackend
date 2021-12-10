using HoneyShop.Core.Models;
using HoneyShop.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace HoneyShop.DataAccess
{
    public class HoneyDbContext: DbContext
    {
        public HoneyDbContext(DbContextOptions<HoneyDbContext> options): base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
        public virtual DbSet<ProductEntity> Products { get; set; }
        public virtual DbSet<CustomerDetailsEntity> CustomerDetails { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<OrderEntity> Order { get; set; }
        public virtual DbSet<ShoppingCartEntity> ShoppingCartItems { get; set; }
    }
}