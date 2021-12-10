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
            
            var salt = "123#!";
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


            _ctx.SaveChanges();
        }

        public void SeedProduction()
        {
            //todo Add seed data to context
        }
    }
}