using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using HoneyShop.Core.IServices;
using HoneyShop.Core.Models;
using HoneyShopWebsiteBackend.Controllers;
using HoneyShopWebsiteBackend.Dto.UserDto;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HoneyShop.Presentation.Test
{
    public class UserControllerTest
    {
        [Fact]
        public void UserController_WithNullUserService_ThrowsInvalidException()
        {
            Assert.Throws<InvalidDataException>(() => new UserController(null));
        }

        #region Controller Initialization
        [Fact]
        public void UserController_WithNullUserService_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(() => new UserController(null));
            Assert.Equal("UserService Cannot Be Null",exception.Message);
        }
        
        [Fact]
        public void UserController_UsesApiControllerAttribute()
        {
            var tyoeInfo = typeof(UserController).GetTypeInfo();
            var attribute = tyoeInfo.GetCustomAttributes().FirstOrDefault(a => a.GetType().Name.Equals("ApiControllerAttribute"));
            Assert.NotNull(attribute);
        }
        
        [Fact]
        public void UserController_UsesRouteAttribute_WithParamApiControllerNameRoute()
        {
            var tyoeInfo = typeof(UserController).GetTypeInfo();
            var attribute = tyoeInfo.GetCustomAttributes().FirstOrDefault(a => a.GetType().Name.Equals("RouteAttribute"));
            Assert.NotNull(attribute);
            var routeAttribute = attribute as RouteAttribute;
            Assert.Equal("api/[Controller]",routeAttribute.Template);
        }
        #endregion

        #region GetAllUsersTest
        [Fact]
        public void UserController_HasGetAllUsersMethod_IsPublic()
        {
            var method = typeof(UserController).GetMethods().FirstOrDefault(m => "GetAllUsers".Equals(m.Name));
            Assert.True(method.IsPublic);
        }
        
        [Fact]
        public void UserController_GetAllUsersMethod_ReturnListOfUsersInActionResult()
        {
            var method = typeof(UserController).GetMethods().FirstOrDefault(m => "GetAllUsers".Equals(m.Name));
            Assert.Equal(typeof(ActionResult<AllUsersDto>).FullName,method.ReturnType.FullName);
        }

        [Fact]
        public void GetAllUsers_WithNoParams_HasGetHttpAttribute()
        {
            var methodInfo = typeof(UserController).GetMethods().FirstOrDefault(m => m.Name=="GetAllUsers");
            var attribute = methodInfo.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "HttpGetAttribute");
            Assert.NotNull(attribute);
        }

        [Fact]
        public void GetAllUsers_CallsUserServiceGetAllUsers_Once()
        {
            var mockService = new Mock<IUserService>();
            var controller = new UserController(mockService.Object);

            controller.GetAllUsers();
            
            mockService.Verify(s => s.GetAllUsers(),Times.Once);
        }
        #endregion
        
        #region GetUserTest
        [Fact]
        public void UserController_HasGetUserMethod_IsPublic()
        {
            var method = typeof(UserController).GetMethods().FirstOrDefault(m => "GetUser".Equals(m.Name));
            Assert.True(method.IsPublic);
        }
        
        [Fact]
        public void UserController_GetUserMethod_ReturnUserInActionResult()
        {
            var method = typeof(UserController).GetMethods().FirstOrDefault(m => "GetUser".Equals(m.Name));
            Assert.Equal(typeof(ActionResult<UserDto>).FullName,method.ReturnType.FullName);
        }

        [Fact]
        public void GetUser_WithNoParams_HasGetHttpAttribute()
        {
            var methodInfo = typeof(UserController).GetMethods().FirstOrDefault(m => m.Name=="GetUser");
            var attribute = methodInfo.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "HttpGetAttribute");
            Assert.NotNull(attribute);
        }

        [Fact]
        public void GetUser_CallsUserServiceGetUser_Once()
        {
            var mockService = new Mock<IUserService>();
            var controller = new UserController(mockService.Object);

            var fakeId = 1;

            controller.GetUser(fakeId);
            
            mockService.Verify(s => s.GetUser(fakeId),Times.Once);
        }
        #endregion
        
        #region CreateUserTest
        [Fact]
        public void UserController_HasCreateUserMethod_IsPublic()
        {
            var method = typeof(UserController).GetMethods().FirstOrDefault(m => "CreateUser".Equals(m.Name));
            Assert.True(method.IsPublic);
        }
        
        [Fact]
        public void UserController_CreateUserMethod_ReturnUserInActionResult()
        {
            var method = typeof(UserController).GetMethods().FirstOrDefault(m => "CreateUser".Equals(m.Name));
            Assert.Equal(typeof(ActionResult<UserDto>).FullName,method.ReturnType.FullName);
        }

        [Fact]
        public void CreateUser_WithNoParams_HasPostHttpAttribute()
        {
            var methodInfo = typeof(UserController).GetMethods().FirstOrDefault(m => m.Name=="CreateUser");
            var attribute = methodInfo.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "HttpPostAttribute");
            Assert.NotNull(attribute);
        }

        [Fact]
        public void CreateUser_CallsUserServiceCreateUser_Once()
        {
            var mockService = new Mock<IUserService>();
            var controller = new UserController(mockService.Object);

            var user = new User
            {
                Id = 1,
                Username = "ExpectedUsername"
            };

            controller.CreateUser(new UserDto{Id = user.Id,Username = user.Username});
            
            mockService.Verify(s => s.CreateUser(user),Times.Once);
        }
        #endregion
        
        #region EditUserTest
        [Fact]
        public void UserController_HasEditUserMethod_IsPublic()
        {
            var method = typeof(UserController).GetMethods().FirstOrDefault(m => "EditUser".Equals(m.Name));
            Assert.True(method.IsPublic);
        }
        
        [Fact]
        public void UserController_EditUserMethod_ReturnUserInActionResult()
        {
            var method = typeof(UserController).GetMethods().FirstOrDefault(m => "EditUser".Equals(m.Name));
            Assert.Equal(typeof(ActionResult<UserDto>).FullName,method.ReturnType.FullName);
        }

        [Fact]
        public void EditUser_WithNoParams_HasPutHttpAttribute()
        {
            var methodInfo = typeof(UserController).GetMethods().FirstOrDefault(m => m.Name=="EditUser");
            var attribute = methodInfo.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "HttpPutAttribute");
            Assert.NotNull(attribute);
        }

        [Fact]
        public void EditUser_CallsUserServiceEditUser_Once()
        {
            var mockService = new Mock<IUserService>();
            var controller = new UserController(mockService.Object);

            int fakeId = 1;
            
            var user = new User
            {
                Id = fakeId,
                Username = "ExpectedUsername"
            };

            controller.EditUser(fakeId,new UserDto
            {
                Id = user.Id,
                Username = user.Username
            });
            
            mockService.Verify(s => s.EditUser(user),Times.Once);
        }
        #endregion
        
        #region DeleteUserTest
        [Fact]
        public void UserController_HasDeleteUserMethod_IsPublic()
        {
            var method = typeof(UserController).GetMethods().FirstOrDefault(m => "DeleteUser".Equals(m.Name));
            Assert.True(method.IsPublic);
        }
        
        [Fact]
        public void UserController_DeleteUserMethod_ReturnUserInActionResult()
        {
            var method = typeof(UserController).GetMethods().FirstOrDefault(m => "DeleteUser".Equals(m.Name));
            Assert.Equal(typeof(ActionResult<UserDto>).FullName,method.ReturnType.FullName);
        }

        [Fact]
        public void DeleteUser_WithNoParams_HasDeleteHttpAttribute()
        {
            var methodInfo = typeof(UserController).GetMethods().FirstOrDefault(m => m.Name=="DeleteUser");
            var attribute = methodInfo.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "HttpDeleteAttribute");
            Assert.NotNull(attribute);
        }

        [Fact]
        public void DeleteUser_CallsUserServiceDeleteUser_Once()
        {
            var mockService = new Mock<IUserService>();
            var controller = new UserController(mockService.Object);
            int fakeId = 1;

            controller.DeleteUser(fakeId);
            
            mockService.Verify(s => s.DeleteUser(fakeId),Times.Once);
        }
        #endregion
    }
}