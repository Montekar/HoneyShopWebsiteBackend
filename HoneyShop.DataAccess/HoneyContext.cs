using HoneyShop.Core.Models;
using HoneyShop.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace HoneyShop.DataAccess
{
    public class HoneyContext: DbContext
    {
        public HoneyContext(DbContextOptions<HoneyContext> options): base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //modelBuilder.Entity<ProductEntity>().HasOne(o => o.Type).WithMany(p => p.Products);
            
            modelBuilder.Entity<ProductEntity>()
                .HasData(new ProductEntity() {Id = 1, Name = "Honey", Description = "Bright fresh", Price = 5});
            modelBuilder.Entity<ProductEntity>()
                .HasData(new ProductEntity() {Id = 2, Name = "Honey", Description = "Bright solid", Price = 5});
            modelBuilder.Entity<ProductEntity>()
                .HasData(new ProductEntity() {Id = 3, Name = "Honey", Description = "Dark fresh", Price = 5});
            modelBuilder.Entity<ProductEntity>()
                .HasData(new ProductEntity() {Id = 4, Name = "Honey", Description = "Dark solid", Price = 5});

            modelBuilder.Entity<UserEntity>()
                .HasData(new UserEntity() {Id = 1, Username = "Username"});
            /*
            modelBuilder.Entity<UserEntity>()
                .HasData(new UserEntity() {Id = 1, FirstName = "Chad", LastName = "Giga", Email = "bruh@gmail.com"});
            modelBuilder.Entity<UserEntity>()
                .HasData(new UserEntity() {Id = 2, FirstName = "Sonic", LastName = "Speed", Email = "light@gmail.com"});
                
            modelBuilder.Entity<AdminEntity>()
                .HasData(new AdminEntity() {Id = 1, FirstName = "Chad", LastName = "Giga", Email = "bruh@gmail.com"});
            modelBuilder.Entity<AdminEntity>()
                .HasData(new AdminEntity() {Id = 2, FirstName = "Sonic", LastName = "Speed", Email = "light@gmail.com"});
            
            modelBuilder.Entity<ProductTypeEntity>().HasData(new ProductTypeEntity() {Id = 1, Type = "Honey"});
            modelBuilder.Entity<ProductTypeEntity>().HasData(new ProductTypeEntity() {Id = 2, Type = "Soup"});
            modelBuilder.Entity<ProductTypeEntity>().HasData(new ProductTypeEntity() {Id = 3, Type = "BeeBread"});
            */    
        }
        public virtual DbSet<ProductEntity> Products { get; set; }
        public virtual DbSet<CustomerDetailsEntity> CustomerDetails { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }
    }
}