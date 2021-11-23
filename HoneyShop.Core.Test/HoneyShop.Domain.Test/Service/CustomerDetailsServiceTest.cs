using System.Collections.Generic;
using System.IO;
using HoneyShop.Core.IServices;
using HoneyShop.Core.Models;
using HoneyShop.Domain.IRepository;
using HoneyShop.Domain.Service;
using Moq;
using Xunit;

namespace HoneyShop.Domain.Test.Service
{
    public class CustomerDetailsServiceTest
    {
        private readonly Mock<ICustomerDetailsRepository> _mock;
        private readonly CustomerDetailsService _service;

        public CustomerDetailsServiceTest()
        {
            _mock = new Mock<ICustomerDetailsRepository>();
            _service = new CustomerDetailsService(_mock.Object);
        }

        [Fact]
        public void CustomerDetailsRepository_IsICustomerDetailsRepository()
        {
            Assert.True(_service is ICustomerDetailsService);
        }

        [Fact]
        public void CustomerDetailsRepository_WithNullCustomerDetailsRepository_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => new CustomerDetailsService(null));
            Assert.Equal("CustomerDetails repository can not be null", exception.Message);
        }

        [Fact]
        public void GetCustomerDetails_NoFilter_Returns_ListOfAllCustomerDetails()
        {
            var expected = new List<CustomerDetails>()
            {
                new CustomerDetails()
                {
                    Id = 1, 
                    FirstName = "Bob",
                    LastName = "TheBuilder",
                    Email = "email@gmail.com",
                    PhoneNumber = 12345678,
                    
                    AddressCountry = "Denmark",
                    AddressCity = "Esbjerg",
                    AddressPostCode = 6500,
                    AddressStreet = "Randomgade",
                    AddressNumber = "96 ST TV"
                },
                new CustomerDetails()
                {
                    Id = 2, 
                    FirstName = "Bob2",
                    LastName = "TheBuilder2",
                    Email = "email2@gmail.com",
                    PhoneNumber = 87654321,
                    
                    AddressCountry = "Denmark",
                    AddressCity = "Esbjerg",
                    AddressPostCode = 6500,
                    AddressStreet = "Randomgade",
                    AddressNumber = "88 ST TV"
                },
            };

            _mock.Setup(r => r.GetAllCustomerDetails())
                .Returns(expected);
            var actual = _service.GetAllCustomerDetails();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateCustomerDetails_Returns_BooleanValue()
        {
            CustomerDetails customerDetails = new CustomerDetails()
            {
                Id = 1, 
                FirstName = "Bob",
                LastName = "TheBuilder",
                Email = "email@gmail.com",
                PhoneNumber = 12345678,
                    
                AddressCountry = "Denmark",
                AddressCity = "Esbjerg",
                AddressPostCode = 6500,
                AddressStreet = "Randomgade",
                AddressNumber = "96 ST TV"
            };
            var expected = true;
            _mock.Setup(r => r.CreateCustomerDetails(customerDetails))
                .Returns(expected);
            var actual = _service.CreateCustomerDetails(customerDetails);
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void UpdateCustomerDetails_Returns_BooleanValue()
        {
            CustomerDetails customerDetails = new CustomerDetails()
            {
                Id = 1, 
                FirstName = "Bob",
                LastName = "TheBuilder",
                Email = "email@gmail.com",
                PhoneNumber = 12345678,
                    
                AddressCountry = "Denmark",
                AddressCity = "Esbjerg",
                AddressPostCode = 6500,
                AddressStreet = "Randomgade",
                AddressNumber = "96 ST TV"
            };
            var expected = true;
            _mock.Setup(r => r.UpdateCustomerDetails(customerDetails))
                .Returns(expected);
            var actual = _service.UpdateCustomerDetails(customerDetails);
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void DeleteCustomerDetails_Returns_BooleanValue()
        {
            var expected = true;
            _mock.Setup(r => r.DeleteCustomerDetails(1))
                .Returns(expected);
            var actual = _service.DeleteCustomerDetails(1);
            Assert.Equal(expected, actual);
        }
        
    }
}