using HoneyShop.Core.Models;
using Xunit;

namespace HoneyShop.Core.Test.Models
{
    public class CustomerDetailsTest
    {
        private readonly CustomerDetails _customerDetails;

        public CustomerDetailsTest()
        {
            _customerDetails = new CustomerDetails();
        }

        [Fact]
        public void CustomerDetailClass_Exists()
        {
            Assert.NotNull(_customerDetails);
        }

        [Fact]
        public void CustomerDetailClass_HasId_WithTypeInt()
        {
            var expected = 1;
            _customerDetails.Id = 1;
            Assert.Equal(expected, _customerDetails.Id);
            Assert.True(_customerDetails.Id is int);
        }

        [Fact]
        public void CustomerDetailClass_HasFirstAndLastName_WithTypeString()
        {
            var expectedFirstName = "Bob";
            var expectedLastName = "TheBuilder";
            _customerDetails.FirstName = "Bob";
            _customerDetails.LastName = "TheBuilder";

            Assert.Equal(expectedFirstName, _customerDetails.FirstName);
            Assert.True(_customerDetails.FirstName is string);

            Assert.Equal(expectedLastName, _customerDetails.LastName);
            Assert.True(_customerDetails.LastName is string);
        }

        [Fact]
        public void CustomerDetailClass_HasPhoneNumber_WithTypeString()
        {
            var expected = "12345678";
            _customerDetails.PhoneNumber = "12345678";

            Assert.Equal(expected, _customerDetails.PhoneNumber);
            Assert.True(_customerDetails.PhoneNumber is string);
        }



        //Address info
        [Fact]
        public void CustomerDetailClass_HasAddress_Country_WithTypeString()
        {
            var expected = "Denmark";
            _customerDetails.AddressCountry = "Denmark";

            Assert.Equal(expected, _customerDetails.AddressCountry);
            Assert.True(_customerDetails.AddressCountry is string);
        }

        [Fact]
        public void CustomerDetailClass_HasAddress_City_WithTypeString()
        {
            var expected = "Esbjerg";
            _customerDetails.AddressCity = "Esbjerg";

            Assert.Equal(expected, _customerDetails.AddressCity);
            Assert.True(_customerDetails.AddressCity is string);
        }

        [Fact]
        public void CustomerDetailClass_HasAddress_PostCode_WithTypeInt()
        {
            var expected = 6500;
            _customerDetails.AddressPostCode = 6500;

            Assert.Equal(expected, _customerDetails.AddressPostCode);
            Assert.True(_customerDetails.AddressPostCode is int);
        }

        [Fact]
        public void CustomerDetailClass_HasAddress_Street_WithTypeString()
        {
            var expected = "Randomgade";
            _customerDetails.AddressStreet = "Randomgade";

            Assert.Equal(expected, _customerDetails.AddressStreet);
            Assert.True(_customerDetails.AddressStreet is string);
        }

        [Fact]
        public void CustomerDetailClass_HasAddress_Number_WithTypeString()
        {
            var expected = "96 ST TV";
            _customerDetails.AddressNumber = "96 ST TV";

            Assert.Equal(expected, _customerDetails.AddressNumber);
            Assert.True(_customerDetails.AddressNumber is string);
        }

        [Fact]
        public void CustomerDetailClass_HasUserId_WithTypeInt()
        {
            var expected = 1;

            _customerDetails.UserId = 1;
            Assert.Equal(expected, _customerDetails.UserId);
            Assert.True(_customerDetails.UserId is int);

        }
    }
}