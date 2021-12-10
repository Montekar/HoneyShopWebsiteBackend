namespace HoneyShop.DataAccess.Entities
{
    public class CustomerDetailsEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressCountry { get; set; }
        public string AddressCity { get; set; }
        public string AddressPostCode { get; set; }
        public string AddressStreet { get; set; }
        public string AddressNumber { get; set; }
    }
}