using HoneyShop.DataAccess.Entities;

namespace HoneyShop.DataAccess
{
    public class HoneyDbSeeder : IHoneyDbSeeder
    {
        private readonly HoneyDbContext _ctx;

        public HoneyDbSeeder(HoneyDbContext ctx)
        {
            _ctx = ctx;
        }

        public void SeedDevelopment()
        {
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
            
            _ctx.CustomerDetails.Add(new CustomerDetailsEntity
            {
                Id = 1,
                UserId = 1,
                AddressCity = "Silale",
                AddressCountry = "Lithuania",
                AddressNumber = "10",
                AddressStreet = "Kestucio",
                FirstName = "Faustas",
                LastName = "Anulis",
                PhoneNumber = "+45something",
                AddressPostCode = "7501"
            });

            _ctx.Products.Add(new ProductEntity() {Id = 1, Name = "Honey", Description = "Bright fresh", Price = 5});
            _ctx.Products.Add(new ProductEntity() {Id = 2, Name = "Honey", Description = "Bright solid", Price = 5});
            _ctx.Products.Add(new ProductEntity() {Id = 3, Name = "Honey", Description = "Dark fresh", Price = 5});
            _ctx.Products.Add(new ProductEntity() {Id = 4, Name = "Honey", Description = "Dark solid", Price = 5});
            
            _ctx.Products.Add(new ProductEntity() {Id = 5, Name = "Bee Bread", Description = "Bee Bread 100g", Price = 4});
            _ctx.Products.Add(new ProductEntity() {Id = 6, Name = "Bee Bread", Description = "Bee Bread 200g", Price = 8});
            
            _ctx.Products.Add(new ProductEntity() {Id = 7, Name = "Soap 1", Description = "Soap with something 1", Price = 3.5});
            _ctx.Products.Add(new ProductEntity() {Id = 8, Name = "Soap 2", Description = "Soap with something 2", Price = 3.5});
            _ctx.Products.Add(new ProductEntity() {Id = 9, Name = "Soap 3", Description = "Soap with something 3", Price = 3.5});
            _ctx.Products.Add(new ProductEntity() {Id = 10, Name = "Soap 4", Description = "Soap with something 4", Price = 3.5});
            _ctx.Products.Add(new ProductEntity() {Id = 11, Name = "Soap 5", Description = "Soap with something 5", Price = 3.5});
            _ctx.Products.Add(new ProductEntity() {Id = 12, Name = "Soap 6", Description = "Soap with something 6", Price = 3.5});

            _ctx.SaveChanges();
        }

        public void SeedProduction()
        {
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();

            _ctx.Products.Add(new ProductEntity() {Id = 1, Name = "Honey", Description = "Bright fresh", Price = 5});
            _ctx.Products.Add(new ProductEntity() {Id = 2, Name = "Honey", Description = "Bright solid", Price = 5});
            _ctx.Products.Add(new ProductEntity() {Id = 3, Name = "Honey", Description = "Dark fresh", Price = 5});
            _ctx.Products.Add(new ProductEntity() {Id = 4, Name = "Honey", Description = "Dark solid", Price = 5});
            
            _ctx.Products.Add(new ProductEntity() {Id = 5, Name = "Bee Bread", Description = "Bee Bread 100g", Price = 4});
            _ctx.Products.Add(new ProductEntity() {Id = 6, Name = "Bee Bread", Description = "Bee Bread 200g", Price = 8});
            
            _ctx.Products.Add(new ProductEntity() {Id = 7, Name = "Soap 1", Description = "Soap with something 1", Price = 3.5});
            _ctx.Products.Add(new ProductEntity() {Id = 8, Name = "Soap 2", Description = "Soap with something 2", Price = 3.5});
            _ctx.Products.Add(new ProductEntity() {Id = 9, Name = "Soap 3", Description = "Soap with something 3", Price = 3.5});
            _ctx.Products.Add(new ProductEntity() {Id = 10, Name = "Soap 4", Description = "Soap with something 4", Price = 3.5});
            _ctx.Products.Add(new ProductEntity() {Id = 11, Name = "Soap 5", Description = "Soap with something 5", Price = 3.5});
            _ctx.Products.Add(new ProductEntity() {Id = 12, Name = "Soap 6", Description = "Soap with something 6", Price = 3.5});
            
            _ctx.SaveChanges();
        }
    }
}