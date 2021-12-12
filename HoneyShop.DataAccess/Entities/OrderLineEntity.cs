namespace HoneyShop.DataAccess.Entities
{
    public class OrderLineEntity
    {
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }
        public int OrderId { get; set; }
        public OrderEntity Order { get; set; }
        public int Amount { get; set; }
    }
}