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
            
            modelBuilder.Entity<ProductEntity>()
                .HasData(new ProductEntity() {Id = 5, Name = "Bee Bread", Description = "Bee Bread 100g", Price = 4});
            modelBuilder.Entity<ProductEntity>()
                .HasData(new ProductEntity() {Id = 6, Name = "Bee Bread", Description = "Bee Bread 200g", Price = 8});
            
            modelBuilder.Entity<ProductEntity>()
                .HasData(new ProductEntity() {Id = 7, Name = "Soap 1", Description = "Soap with something 1", Price = 3.5});
            modelBuilder.Entity<ProductEntity>()
                .HasData(new ProductEntity() {Id = 8, Name = "Soap 2", Description = "Soap with something 2", Price = 3.5});
            modelBuilder.Entity<ProductEntity>()
                .HasData(new ProductEntity() {Id = 9, Name = "Soap 3", Description = "Soap with something 3", Price = 3.5});
            modelBuilder.Entity<ProductEntity>()
                .HasData(new ProductEntity() {Id = 10, Name = "Soap 4", Description = "Soap with something 4", Price = 3.5});
            modelBuilder.Entity<ProductEntity>()
                .HasData(new ProductEntity() {Id = 11, Name = "Soap 5", Description = "Soap with something 5", Price = 3.5});
            modelBuilder.Entity<ProductEntity>()
                .HasData(new ProductEntity() {Id = 12, Name = "Soap 6", Description = "Soap with something 6", Price = 3.5});
            
            modelBuilder.Entity<UserEntity>()
                .HasData(new UserEntity() {Id = 1, Username = "test@gmail.com"});

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