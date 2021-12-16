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
                    PhoneNumber = "12345678",
                    
                    AddressCountry = "Denmark",
                    AddressCity = "Esbjerg",
                    AddressPostCode = "6500",
                    AddressStreet = "Randomgade",
                    AddressNumber = "96 ST TV"
                },
                new CustomerDetails()
                {
                    Id = 2, 
                    FirstName = "Bob2",
                    LastName = "TheBuilder2",
                    PhoneNumber = "87654321",
                    
                    AddressCountry = "Denmark",
                    AddressCity = "Esbjerg",
                    AddressPostCode = "6500",
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
                PhoneNumber = "12345678",
                    
                AddressCountry = "Denmark",
                AddressCity = "Esbjerg",
                AddressPostCode = "6500",
                AddressStreet = "Randomgade",
                AddressNumber = "96 ST TV"
            };
            var expected = true;
            _mock.Setup(r => r.CreateCustomerDetails(customerDetails))
                .Returns(customerDetails);
            var actual = _service.CreateCustomerDetails(customerDetails);
            Assert.Equal(customerDetails, actual);
        }
        
        [Fact]
        public void UpdateCustomerDetails_Returns_CustomerDetails()
        {
            CustomerDetails customerDetails = new CustomerDetails()
            {
                Id = 1, 
                FirstName = "Bob",
                LastName = "TheBuilder",
                PhoneNumber = "12345678",
                    
                AddressCountry = "Denmark",
                AddressCity = "Esbjerg",
                AddressPostCode = "6500",
                AddressStreet = "Randomgade",
                AddressNumber = "96 ST TV"
            };
            _mock.Setup(r => r.UpdateCustomerDetails(customerDetails))
                .Returns(customerDetails);
            var actual = _service.UpdateCustomerDetails(customerDetails);
            Assert.Equal(customerDetails, actual);
        }
        [Fact]
        public void DeleteCustomerDetails_Returns_CustomerDetails()
        {
            CustomerDetails expected = new CustomerDetails()
            {
                Id = 1, 
                FirstName = "Bob",
                LastName = "TheBuilder",
                PhoneNumber = "12345678",
                    
                AddressCountry = "Denmark",
                AddressCity = "Esbjerg",
                AddressPostCode = "6500",
                AddressStreet = "Randomgade",
                AddressNumber = "96 ST TV"
            };
            _mock.Setup(r => r.DeleteCustomerDetails(1))
                .Returns(expected);
            var actual = _service.DeleteCustomerDetails(1);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetCustomerDetailsById_Returns_CustomerDetails()
        {
            int id = 1;
            CustomerDetails expected = new CustomerDetails()
            {
                Id = id, 
                FirstName = "Bob",
                LastName = "TheBuilder",
                PhoneNumber = "12345678",
                    
                AddressCountry = "Denmark",
                AddressCity = "Esbjerg",
                AddressPostCode = "6500",
                AddressStreet = "Randomgade",
                AddressNumber = "96 ST TV"
            };
            _mock.Setup(r => r.GetCustomerDetailsById(id))
                .Returns(expected);
            var actual = _service.GetCustomerDetailsById(id);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetCustomerDetailsByUserId_Returns_CustomerDetailsList()
        {
            int firstId = 1;
            int secondId = 2;
            int userId = 1;
            List<CustomerDetails> expected = new List<CustomerDetails>
            {
                new CustomerDetails()
                {
                    Id = firstId,
                    UserId = userId,
                    FirstName = "Bob",
                    LastName = "TheBuilder",
                    PhoneNumber = "12345678",

                    AddressCountry = "Denmark",
                    AddressCity = "Esbjerg",
                    AddressPostCode = "6500",
                    AddressStreet = "Randomgade",
                    AddressNumber = "96 ST TV"
                },
                new CustomerDetails()
                {
                    Id = secondId,
                    UserId = userId,
                    FirstName = "kek",
                    LastName = "TheBuilder",
                    PhoneNumber = "12345678",

                    AddressCountry = "Denmark",
                    AddressCity = "Esbjerg",
                    AddressPostCode = "6500",
                    AddressStreet = "Randomgade",
                    AddressNumber = "96 ST TV"
                },
                
            };
            _mock.Setup(r => r.GetCustomerDetailsByUserId(userId))
                .Returns(expected);
            var actual = _service.GetCustomerDetailsByUserId(userId);
            Assert.Equal(expected, actual);
        }
    }
}