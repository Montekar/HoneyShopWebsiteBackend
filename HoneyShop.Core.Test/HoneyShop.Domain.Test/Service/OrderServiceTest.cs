using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HoneyShop.Core.IServices;
using HoneyShop.Core.Models;
using HoneyShop.Domain.IRepository;
using HoneyShop.Domain.Service;
using Moq;
using Xunit;

namespace HoneyShop.Domain.Test.Service
{
    public class OrderServiceTest

    {
        private readonly Mock<IOrderRepository> _mock;
        private readonly OrderService _service;
        private readonly Order _order;

        public OrderServiceTest()
        {
            _mock = new Mock<IOrderRepository>();
            _service = new OrderService(_mock.Object);
            _order = new Order();
        }

        [Fact]
        public void OrderRepository_IsIOrderRepository()
        {
            Assert.True(_service is IOrderService);
        }

        [Fact]
        public void OrderRepository_WithNullOrderRepository_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => new OrderService(null));
            Assert.Equal("Product repository can not be null", exception.Message);
        }
        
        [Fact]
        public void CreateOrder_WithTypeOrder_ReturnsOrder()
        {
            Order expectedOrder = new Order() { Id = 1,CustomerId = 1,OrderCompleted = false,OrderPaid = false};
            _mock.Setup(r => r.CreateOrder(expectedOrder))
                .Returns(expectedOrder);
            var actual = _service.CreateOrder(expectedOrder);
            Assert.Equal(expectedOrder, actual);
        }





    }
}