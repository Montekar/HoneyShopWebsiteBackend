using System.Collections.Generic;
using HoneyShop.Core.IServices;
using HoneyShop.Core.Models;
using Moq;
using Xunit;

namespace HoneyShop.Core.Test.IServices
{
    public class ICustomerDetailsServiceTest
    {
        private readonly Mock<ICustomerDetailsService> _service;

        public ICustomerDetailsServiceTest()
        {
            _service = new Mock<ICustomerDetailsService>();
        }

        [Fact]
        public void ICostumerDetailsService_Exists()
        {
            Assert.NotNull(_service.Object);
        }
        
        [Fact]
        public void GetAllCustomerDetails()
        {
            var expectedList = new List<CustomerDetails>()
            {
                new CustomerDetails()
                {
                    Id = 1, 
                    FirstName = "Soup",
                    LastName = "sefwef",
                    Email = "safse",
                    PhoneNumber = "213546",
                    
                    AddressCountry = "ewfwef",
                    AddressCity = "ewfwef",
                    AddressPostCode = 123,
                    AddressStreet = "ewfwef",
                    AddressNumber = "ewfwef"
                },
                new CustomerDetails()
                {
                    Id = 2, 
                    FirstName = "Soup",
                    LastName = "sefwef",
                    Email = "safse",
                    PhoneNumber = "213546",
                    
                    AddressCountry = "ewfwef",
                    AddressCity = "ewfwef",
                    AddressPostCode = 123,
                    AddressStreet = "ewfwef",
                    AddressNumber = "ewfwef"
                },
            };
            _service.Setup(ps => ps.GetAllCustomerDetails())
                .Returns(expectedList);
            
            Assert.Equal(expectedList, _service.Object.GetAllCustomerDetails());
        }
        

        [Fact]
        public void CreateCustomerDetails_ReturnsTrue()
        {
            var expected = true;
            var customerDetails = new CustomerDetails()
            {
                Id = 1, 
                FirstName = "Soup",
                LastName = "sefwef",
                Email = "safse",
                PhoneNumber = "213546",
                    
                AddressCountry = "ewfwef",
                AddressCity = "ewfwef",
                AddressPostCode = 123,
                AddressStreet = "ewfwef",
                AddressNumber = "ewfwef"
            };
            _service.Setup(ps => ps.CreateCustomerDetails(customerDetails))
                .Returns(expected);
            
            Assert.Equal(expected, _service.Object.CreateCustomerDetails(customerDetails));
        }

        [Fact]
        public void UpdateCustomerDetails_ReturnsTrue()
        {
            var expected = true;
            var customerDetails = new CustomerDetails()
            {
                Id = 1, 
                FirstName = "Bob",
                LastName = "TheBuilder",
                Email = "email@gmail.com",
                PhoneNumber = "12345678",
                    
                AddressCountry = "Denmark",
                AddressCity = "Esbjerg",
                AddressPostCode = 6500,
                AddressStreet = "Randomgade",
                AddressNumber = "96 ST TV"
            };
            _service.Setup(ps => ps.UpdateCustomerDetails(customerDetails))
                .Returns(expected);
            
            Assert.Equal(expected, _service.Object.UpdateCustomerDetails(customerDetails));
        }
        
        [Fact]
        public void DeleteCustomerDetails_ReturnsTrue()
        {
            var expected = true;
            _service.Setup(ps => ps.DeleteCustomerDetails(1))
                .Returns(expected);
            
            Assert.Equal(expected, _service.Object.DeleteCustomerDetails(1));
        }
        
        [Fact]
        public void GetCustomerDetails_ById()
        {
            var expected = new CustomerDetails();
            
            _service.Setup(ps => ps.GetCustomerDetailsById(1))
                .Returns(expected);
            
            Assert.Equal(expected, _service.Object.GetCustomerDetailsById(1));
        }
    }
}