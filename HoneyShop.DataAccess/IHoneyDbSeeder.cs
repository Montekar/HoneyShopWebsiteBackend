namespace HoneyShop.DataAccess
{
    public interface IHoneyDbSeeder
    {
        void SeedDevelopment();
        void SeedProduction();
    }
}