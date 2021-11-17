namespace HoneyShop.DataAccess
{
    public class DbInitialize
    {
        public static void InitData(HoneyContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}