using System.IO;
using System.Linq;
using System.Reflection;
using HoneyShop.Core.IServices;
using HoneyShopWebsiteBackend.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HoneyShop.Presentation.Test
{
    public class ProductControllerTest
    {
        private readonly Mock<IProductService> _service;
        private readonly ProductController _controller;

        public ProductControllerTest()
        {
            _service = new Mock<IProductService>();
            _controller = new ProductController(_service.Object);
        }
        
        [Fact]
        public void ProductController_HasProductService_IsOfTypeControllerBase()
        {
            Assert.IsAssignableFrom<ControllerBase>(_controller);
        }

        [Fact]
        public void ProductController_WithNullProductService_ThrowsException()
        {
            Assert.Throws<InvalidDataException>(
                () => new ProductController(null));
        }

        [Fact]
        public void ProductController_WithNullProductService_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => new ProductController(null));
            Assert.Equal("Product service can not be null", exception.Message);
        }

        [Fact]
        public void ProductController_UsesApiControllerAttribute()
        {
            var typeInfo = typeof(ProductController).GetTypeInfo();
            var attr = typeInfo.GetCustomAttributes()
                .FirstOrDefault(a => a.GetType().Name.Equals("ApiControllerAttribute"));
            Assert.NotNull(attr); 
        }

        [Fact]
        public void ProductController_UsesRouteAttribute_WithParamApiControllerNameRoute()
        {
            var typeInfo = typeof(ProductController).GetTypeInfo();
            var attr = typeInfo.GetCustomAttributes()
                .FirstOrDefault(a => a.GetType().Name.Equals("RouteAttribute"));
            Assert.NotNull(attr);
            var routeAttribute = attr as RouteAttribute;
            Assert.Equal("api/[controller]", routeAttribute?.Template);
        }

        [Fact]
        public void GetAll_WithNoParams_HasGetHttpAttribute()
        {
            var methodInfo = typeof(ProductController)
                .GetMethods()
                .FirstOrDefault(m => m.Name == "GetAll");
            var attr = methodInfo?.CustomAttributes
                .FirstOrDefault(ca => ca.AttributeType.Name == "HttpGetAttribute");
            Assert.NotNull(attr);
        }
    }
}