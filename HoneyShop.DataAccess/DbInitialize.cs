namespace HoneyShop.DataAccess
{
    public class DbInitialize
    {
        public static void InitData(HoneyDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }
    }
}