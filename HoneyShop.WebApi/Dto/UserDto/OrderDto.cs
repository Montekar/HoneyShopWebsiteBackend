namespace HoneyShopWebsiteBackend.Dto.UserDto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public bool OrderCompleted { get; set; }
        public bool OrderPaid { get; set; }
    }
}