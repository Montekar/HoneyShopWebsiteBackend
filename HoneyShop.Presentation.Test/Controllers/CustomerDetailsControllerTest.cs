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
    public class CustomerDetailsControllerTest
    {
        private readonly Mock<ICustomerDetailsService> _service;
        private readonly CustomerDetailsController _controller;

        public CustomerDetailsControllerTest()
        {
            _service = new Mock<ICustomerDetailsService>();
            _controller = new CustomerDetailsController(_service.Object);
        }
        
        [Fact]
        public void CustomerDetailsController_HasCustomerDetailsService_IsOfTypeControllerBase()
        {
            Assert.IsAssignableFrom<ControllerBase>(_controller);
        }

        [Fact]
        public void CustomerDetailsController_WithNullCustomerDetailsService_ThrowsException()
        {
            Assert.Throws<InvalidDataException>(
                () => new CustomerDetailsController(null));
        }

        [Fact]
        public void CustomerDetailsController_WithNullCustomerDetailsService_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => new CustomerDetailsController(null));
            Assert.Equal("CustomerDetails service can not be null", exception.Message);
        }

        [Fact]
        public void CustomerDetailsController_UsesApiControllerAttribute()
        {
            var typeInfo = typeof(CustomerDetailsController).GetTypeInfo();
            var attr = typeInfo.GetCustomAttributes()
                .FirstOrDefault(a => a.GetType().Name.Equals("ApiControllerAttribute"));
            Assert.NotNull(attr); 
        }

        [Fact]
        public void CustomerDetailsController_UsesRouteAttribute_WithParamApiControllerNameRoute()
        {
            var typeInfo = typeof(CustomerDetailsController).GetTypeInfo();
            var attr = typeInfo.GetCustomAttributes()
                .FirstOrDefault(a => a.GetType().Name.Equals("RouteAttribute"));
            Assert.NotNull(attr);
            var routeAttribute = attr as RouteAttribute;
            Assert.Equal("api/[controller]", routeAttribute?.Template);
        }

        [Fact]
        public void GetAll_WithNoParams_HasGetHttpAttribute()
        {
            var methodInfo = typeof(CustomerDetailsController)
                .GetMethods()
                .FirstOrDefault(m => m.Name == "GetAll");
            var attr = methodInfo?.CustomAttributes
                .FirstOrDefault(ca => ca.AttributeType.Name == "HttpGetAttribute");
            Assert.NotNull(attr);
        }
    }
}