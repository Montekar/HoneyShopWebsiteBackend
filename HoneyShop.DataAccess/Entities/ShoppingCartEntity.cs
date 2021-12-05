namespace HoneyShop.DataAccess.Entities
{
    public class ShoppingCartEntity
    {
        public int Id { get; set; }
        public ProductEntity Product { get; set; }
        public int Amount { get; set; }
    }
}