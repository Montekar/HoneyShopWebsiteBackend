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
        }

        public void SeedProduction()
        {
            //todo Add seed data to context
        }
    }
}