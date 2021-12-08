using System.IO;
using System.Linq;
using System.Reflection;
using HoneyShop.Core.IServices;
using HoneyShop.Domain.Service;
using HoneyShopWebsiteBackend.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HoneyShop.Presentation.Test
{
    public class ShoppingCartControllerTest
    {
        private readonly Mock<IShoppingCartService> _service;
        private readonly ShoppingCartController _controller;

        public ShoppingCartControllerTest()
        {
            _service = new Mock<IShoppingCartService>();
            _controller = new ShoppingCartController(_service.Object);
        }

        [Fact]
        public void ShoppingCartController_HasShoppingCartService_IsOfTypeControllerBase()
        {
            Assert.IsAssignableFrom<ControllerBase>(_controller);
        }
        
        [Fact]
        public void ShoppingCartController_WithNullShoppingCartService_ThrowsException()
        {
            Assert.Throws<InvalidDataException>(
                () => new ProductController(null));
        }

        [Fact]
        public void ShoppingCartController_WithNullShoppingCartService_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => new ShoppingCartController(null));
            Assert.Equal("Service can not be null", exception.Message);
        }
        
        [Fact]
        public void ShoppingCartController_UsesApiControllerAttribute()
        {
            var typeInfo = typeof(ShoppingCartController).GetTypeInfo();
            var attr = typeInfo.GetCustomAttributes()
                .FirstOrDefault(a => a.GetType().Name.Equals("ApiControllerAttribute"));
            Assert.NotNull(attr); 
        }
        
        [Fact]
        public void ShoppingCartController_UsesRouteAttribute_WithParamApiControllerNameRoute()
        {
            var typeInfo = typeof(ShoppingCartController).GetTypeInfo();
            var attr = typeInfo.GetCustomAttributes()
                .FirstOrDefault(a => a.GetType().Name.Equals("RouteAttribute"));
            Assert.NotNull(attr);
            var routeAttribute = attr as RouteAttribute;
            Assert.Equal("api/[controller]", routeAttribute?.Template);
        }
    }
}