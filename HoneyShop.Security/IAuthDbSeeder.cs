namespace HoneyShop.Security
{
    public interface IAuthDbSeeder
    {
        void SeedDevelopment();
        void SeedProduction();
    }
}