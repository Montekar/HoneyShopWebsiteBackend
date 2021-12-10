using System.Collections.Generic;
using HoneyShop.Core.Models;

namespace HoneyShop.Domain.IRepository
{
    public interface ICustomerDetailsRepository
    {
        List<CustomerDetails> GetAllCustomerDetails();
        CustomerDetails CreateCustomerDetails(CustomerDetails customerDetails);
        CustomerDetails UpdateCustomerDetails(CustomerDetails customerDetails);
        CustomerDetails DeleteCustomerDetails(int id);
        CustomerDetails GetCustomerDetailsById(int id);
        List<CustomerDetails> GetCustomerDetailsByUserId(int userId);
    }
}