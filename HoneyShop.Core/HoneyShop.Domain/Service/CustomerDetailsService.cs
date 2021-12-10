using System.Collections.Generic;
using System.IO;
using HoneyShop.Core.IServices;
using HoneyShop.Core.Models;
using HoneyShop.Domain.IRepository;

namespace HoneyShop.Domain.Service
{
    public class CustomerDetailsService: ICustomerDetailsService
    {
        private readonly ICustomerDetailsRepository _customerDetailsRepository;

        public CustomerDetailsService(ICustomerDetailsRepository customerDetailsRepository)
        {
            _customerDetailsRepository = customerDetailsRepository ?? throw new InvalidDataException("CustomerDetails repository can not be null");
        }

        public List<CustomerDetails> GetAllCustomerDetails()
        {
            return _customerDetailsRepository.GetAllCustomerDetails();
        }

        public CustomerDetails CreateCustomerDetails(CustomerDetails customerDetails)
        {
            return _customerDetailsRepository.CreateCustomerDetails(customerDetails);
        }
        
        public CustomerDetails UpdateCustomerDetails(CustomerDetails customerDetails)
        {
            return _customerDetailsRepository.UpdateCustomerDetails(customerDetails);
        }
        
        public CustomerDetails DeleteCustomerDetails(int id)
        {
            return _customerDetailsRepository.DeleteCustomerDetails(id);
        }

        public CustomerDetails GetCustomerDetailsById(int id)
        {
            return _customerDetailsRepository.GetCustomerDetailsById(id);
        }
        public List<CustomerDetails> GetCustomerDetailsByUserId(int userId)
        {
            return _customerDetailsRepository.GetCustomerDetailsByUserId(userId);
        }
    }
}