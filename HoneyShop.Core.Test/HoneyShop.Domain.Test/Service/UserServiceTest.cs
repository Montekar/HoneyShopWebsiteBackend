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
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _mock;
        private readonly UserService _service;
        private readonly List<User> _expected;

        public UserServiceTest()
        {
            _mock = new Mock<IUserRepository>();
            _service = new UserService(_mock.Object);
            _expected = new List<User>
            {
                new User{Id = 1,Email = "ExpectedUsername1"},
                new User{Id = 2,Email = "ExpectedUsername2"}
            };
        }
        
        [Fact]
        public void ProductService_IsIUserService()
        {
            Assert.True(_service is IUserService);
        }

        [Fact]
        public void UserService_WithNullProductRepository_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(
                () => new UserService(null)
                );
        }
        
        [Fact]
        public void UserService_WithNullUserRepository_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => new UserService(null)
            );
            
            Assert.Equal("UserRepository Cannot Be Null", exception.Message);
        }
        [Fact]
        public void GetAllUsers_CallsUsersRepositoriesGetAllUsers_ExactlyOnce()
        {
            _service.GetAllUsers();
            _mock.Verify(r => r.GetAllUsers(),Times.Once);
        }

        [Fact]
        public void GetAllUsers_NoFilter_ReturnsListOfAllUsers()
        {
            _mock.Setup(r => r.GetAllUsers())
                .Returns(_expected);
            var actual = _service.GetAllUsers();
            Assert.Equal(_expected,actual);
        }
        
        [Fact]
        public void GetUser_CallsUsersRepositoriesGetUserById_ExactlyOnce()
        {
            int fakeId = 1;
            _service.GetUser(fakeId);
            _mock.Verify(r => r.GetUser(fakeId),Times.Once);
        }

        [Fact]
        public void GetUser_ReturnsUserById()
        {
            var fakeUser = _expected[0];
            _mock.Setup(r => r.GetUser(fakeUser.Id))
                .Returns(fakeUser);
            var actual = _service.GetUser(fakeUser.Id);
            Assert.Equal(fakeUser,actual);
        }
        
        [Fact]
        public void CreateUser_CallsUsersRepositoriesCreateUser_ExactlyOnce()
        {
            var fakeUser = _expected[0];
            _service.CreateUser(fakeUser);
            _mock.Verify(r => r.CreateUser(fakeUser),Times.Once);
        }

        [Fact]
        public void CreateUser_ReturnsUserAfterCreation()
        {
            var fakeUser = _expected[0];
            _mock.Setup(r => r.CreateUser(fakeUser))
                .Returns(fakeUser);
            var actual = _service.CreateUser(fakeUser);
            Assert.Equal(fakeUser,actual);
        }
        
        [Fact]
        public void EditUser_CallsUsersRepositoriesEditUser_ExactlyOnce()
        {
            var fakeUser = _expected[0];
            _service.EditUser(fakeUser);
            _mock.Verify(r => r.EditUser(fakeUser),Times.Once);
        }
        
        [Fact]
        public void EditUser_ReturnsEditedUser()
        {
            var fakeUser = _expected[0];
            
            _mock.Setup(r => r.EditUser(fakeUser))
                .Returns(fakeUser);
            var actual = _service.EditUser(fakeUser);
            Assert.Equal(fakeUser,actual);
        }
        
        [Fact]
        public void DeleteUser_CallsUsersRepositoriesDeleteUser_ExactlyOnce()
        {
            var fakeUser = _expected[0];
            _service.DeleteUser(fakeUser.Id);
            _mock.Verify(r => r.DeleteUser(fakeUser.Id),Times.Once);
        }
        
        [Fact]
        public void DeleteUser_ReturnsDeletedUser()
        {
            var fakeUser = _expected[0];
            
            _mock.Setup(r => r.DeleteUser(fakeUser.Id))
                .Returns(fakeUser);
            var actual = _service.DeleteUser(fakeUser.Id);
            Assert.Equal(fakeUser,actual);
        }
    }
}