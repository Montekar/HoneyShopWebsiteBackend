using System.Collections.Generic;
using HoneyShop.Core.IServices;
using HoneyShop.Core.Models;
using Moq;
using Xunit;

namespace HoneyShop.Core.Test.IServices
{
    public class IUserServiceTest
    {
        [Fact]
        public void IUserService_IsAvailable()
        {
            IUserService service = new Mock<IUserService>().Object;
            Assert.NotNull(service);
        }

        [Fact]
        public void GetAll_WithNoParam_ReturnsListOfAllUsers()
        {
            var mock = new Mock<IUserService>();
            var fakeList = new List<User>();
            mock.Setup(s=>s.GetAllUsers())
                .Returns(fakeList);
            var service = mock.Object;
            Assert.Equal(fakeList,service.GetAllUsers());
        }
        
        [Fact]
        public void GetUser_WithIdParam_ReturnsUser()
        {
            var mock = new Mock<IUserService>();
            int fakeId = 1;
            var fakeUser = new User
            {
                Id = fakeId,
                Username = "ExpectedUsername",
                PasswordHash = new byte[]{0,255},
                PasswordSalt = new byte[]{0,255},
                Role = "ExpectedRole"
            };
            mock.Setup(s => s.GetUser(fakeId))
                .Returns(fakeUser);
            var service = mock.Object;
            Assert.Equal(fakeUser,service.GetUser(fakeId));
        }
        
        [Fact]
        public void CreateUser_WithUserParam_ReturnsUser()
        {
            var mock = new Mock<IUserService>();
            var fakeUser = new User
            {
                Id = 1,
                Username = "ExpectedUsername",
                PasswordHash = new byte[]{0,255},
                PasswordSalt = new byte[]{0,255},
                Role = "ExpectedRole"
            };
            mock.Setup(s => s.CreateUser(fakeUser))
                .Returns(fakeUser);
            var service = mock.Object;
            Assert.Equal(fakeUser,service.CreateUser(fakeUser));
        }
        
        [Fact]
        public void UpdateUser_WithUserParam_ReturnsUser()
        {
            var mock = new Mock<IUserService>();
            var fakeUser = new User
            {
                Id = 1,
                Username = "ExpectedUsername",
                PasswordHash = new byte[]{0,255},
                PasswordSalt = new byte[]{0,255},
                Role = "ExpectedRole"
            };
            mock.Setup(s => s.EditUser(fakeUser))
                .Returns(fakeUser);
            var service = mock.Object;
            Assert.Equal(fakeUser,service.EditUser(fakeUser));
        }
        
        [Fact]
        public void DeleteUser_WithUserParam_ReturnsUser()
        {
            var mock = new Mock<IUserService>();
            var fakeUser = new User
            {
                Id = 1,
                Username = "ExpectedUsername",
                PasswordHash = new byte[]{0,255},
                PasswordSalt = new byte[]{0,255},
                Role = "ExpectedRole"
            };
            mock.Setup(s => s.DeleteUser(fakeUser.Id))
                .Returns(fakeUser);
            var service = mock.Object;
            Assert.Equal(fakeUser,service.DeleteUser(fakeUser.Id));
        }
    }
}