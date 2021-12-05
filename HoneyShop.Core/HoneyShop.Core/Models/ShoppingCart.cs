namespace HoneyShop.Core.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
    }
}