namespace HoneyShop.Core.Models
{
    public class CustomerDetails
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        
        
        //Address info
        public string AddressCountry { get; set; }
        public string AddressCity { get; set; }
        public int AddressPostCode { get; set; }
        public string AddressStreet { get; set; }
        public string AddressNumber { get; set; }
    }
}