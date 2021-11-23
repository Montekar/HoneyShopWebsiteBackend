using System.Collections.Generic;
using HoneyShop.Core.Models;

namespace HoneyShop.Core.IServices
{
    public interface ICustomerDetailsService
    {
        List<CustomerDetails> GetAllCustomerDetails();
        bool CreateCustomerDetails(CustomerDetails customerDetails);
        bool UpdateCustomerDetails(CustomerDetails customerDetails);
        bool DeleteCustomerDetails(int id);
        CustomerDetails GetCustomerDetailsById(int id);
    }

}