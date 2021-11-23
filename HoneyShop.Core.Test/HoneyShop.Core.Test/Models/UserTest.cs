using System;
using System.Linq;
using System.Net.Http;
using HoneyShop.Core.Models;
using Xunit;

namespace HoneyShop.Core.Test.Models
{
    public class UserTest
    {
        private readonly User _user;

        public UserTest()
        {
            _user = new User();
        }

        [Fact]
        public void User_CanBeInitialized()
        {
            Assert.NotNull(_user);
        }
        
        [Fact]
        public void User_Id_MustBeInt()
        {
            Assert.True(_user.Id is int);
        }

        [Fact]
        public void User_SetId_StoresId_StoresNewId()
        {
            _user.Id = 1;
            _user.Id = 2;
            Assert.Equal(2,_user.Id);
        }

        [Fact]
        public void User_SetUsername_StoreUsernameAsString()
        {
            _user.Username = "ExpectedUsername";
            Assert.Equal("ExpectedUsername",_user.Username);
        }
        
        [Fact]
        public void User_SetFirstName_StoreFirstNameAsString()
        {
            _user.FirstName = "ExpectedFirstName";
            Assert.Equal("ExpectedFirstName",_user.FirstName);
        }
        
        [Fact]
        public void User_SetLastName_StoreLastNameAsString()
        {
            _user.LastName = "ExpectedLastName";
            Assert.Equal("ExpectedLastName",_user.LastName);
        }
        
        [Fact]
        public void User_SetAddress_StoreAddressAsString()
        {
            _user.Address = "ExpectedAddress";
            Assert.Equal("ExpectedAddress",_user.Address);
        }
        [Fact]
        public void User_SetEmail_StoreEmailAsString()
        {
            _user.Email = "ExpectedEmail";
            Assert.Equal("ExpectedEmail",_user.Email);
        }
        
        [Fact]
        public void User_SetPhoneNumber_StorePhoneNumberAsString()
        {
            _user.PhoneNumber = "ExpectedPhoneNumber";
            Assert.Equal("ExpectedPhoneNumber",_user.PhoneNumber);
        }

        [Fact]
        public void User_SetPasswordHash_StorePasswordHashAsByteArray()
        {
            _user.PasswordHash = new byte[] {0, 255};
            Assert.Equal(new byte[] {0, 255},_user.PasswordHash);
        }
        
        [Fact]
        public void User_SetPasswordSalt_StorePasswordSaltAsByteArray()
        {
            _user.PasswordSalt = new byte[] {0, 255};
            Assert.Equal(new byte[] {0, 255},_user.PasswordSalt);
        }
    }
}