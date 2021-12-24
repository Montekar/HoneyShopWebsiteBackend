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

            _ctx.Order.Add(new OrderEntity() { CustomerId = 1, OrderCompleted = false, OrderPaid = false });
            _ctx.Order.Add(new OrderEntity() { CustomerId = 1, OrderCompleted = false, OrderPaid = false });
            _ctx.Order.Add(new OrderEntity() { CustomerId = 1, OrderCompleted = false, OrderPaid = false });
            _ctx.Order.Add(new OrderEntity() { CustomerId = 1, OrderCompleted = false, OrderPaid = false });

            _ctx.Products.Add(new ProductEntity() { Name = "Honey", Description = "Bright fresh", Price = 5});
            _ctx.Products.Add(new ProductEntity() { Name = "Honey", Description = "Bright solid", Price = 5});
            _ctx.Products.Add(new ProductEntity() { Name = "Honey", Description = "Dark fresh", Price = 5});
            _ctx.Products.Add(new ProductEntity() { Name = "Honey", Description = "Dark solid", Price = 5});
            
            _ctx.Products.Add(new ProductEntity() { Name = "Bee Bread", Description = "Bee Bread 100g", Price = 4});
            _ctx.Products.Add(new ProductEntity() { Name = "Bee Bread", Description = "Bee Bread 200g", Price = 8});
            
            _ctx.Products.Add(new ProductEntity() { Name = "Soap 1", Description = "Soap with something 1", Price = 3.5});
            _ctx.Products.Add(new ProductEntity() { Name = "Soap 2", Description = "Soap with something 2", Price = 3.5});
            _ctx.Products.Add(new ProductEntity() { Name = "Soap 3", Description = "Soap with something 3", Price = 3.5});
            _ctx.Products.Add(new ProductEntity() { Name = "Soap 4", Description = "Soap with something 4", Price = 3.5});
            _ctx.Products.Add(new ProductEntity() { Name = "Soap 5", Description = "Soap with something 5", Price = 3.5});
            _ctx.Products.Add(new ProductEntity() { Name = "Soap 6", Description = "Soap with something 6", Price = 3.5});

            _ctx.SaveChanges();
        }

        public void SeedProduction()
        {
            _ctx.Database.EnsureCreated();

            _ctx.Products.Add(new ProductEntity() { Name = "Honey", Description = "Bright fresh", Price = 5});
            _ctx.Products.Add(new ProductEntity() { Name = "Honey", Description = "Bright solid", Price = 5});
            _ctx.Products.Add(new ProductEntity() { Name = "Honey", Description = "Dark fresh", Price = 5});
            _ctx.Products.Add(new ProductEntity() { Name = "Honey", Description = "Dark solid", Price = 5});
            
            _ctx.Products.Add(new ProductEntity() { Name = "Bee Bread", Description = "Bee Bread 100g", Price = 4});
            _ctx.Products.Add(new ProductEntity() { Name = "Bee Bread", Description = "Bee Bread 200g", Price = 8});
            
            _ctx.Products.Add(new ProductEntity() { Name = "Soap 1", Description = "Soap with something 1", Price = 3.5});
            _ctx.Products.Add(new ProductEntity() { Name = "Soap 2", Description = "Soap with something 2", Price = 3.5});
            _ctx.Products.Add(new ProductEntity() { Name = "Soap 3", Description = "Soap with something 3", Price = 3.5});
            _ctx.Products.Add(new ProductEntity() { Name = "Soap 4", Description = "Soap with something 4", Price = 3.5});
            _ctx.Products.Add(new ProductEntity() { Name = "Soap 5", Description = "Soap with something 5", Price = 3.5});
            _ctx.Products.Add(new ProductEntity() { Name = "Soap 6", Description = "Soap with something 6", Price = 3.5});
            
            _ctx.SaveChanges();
        }
    }
}