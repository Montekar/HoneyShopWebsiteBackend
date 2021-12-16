using System.Collections.Generic;
using System.IO;
using HoneyShop.Core.Models;
using HoneyShop.Domain.IRepository;
using HoneyShop.Domain.Service;
using Moq;
using Xunit;

namespace HoneyShop.Core.Test.IServices
{
    public class IOrderServiceTest
    {
        private readonly Mock<IOrderRepository> _mock;
        private readonly OrderService _service;
        private readonly Order _order;
        public IOrderServiceTest()
        {
            _mock = new Mock<IOrderRepository>();
            _service = new OrderService(_mock.Object);
            _order = new Order();
        }
        
        [Fact]
        public void OrderRepository_WithNullOrderRepository_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => new OrderService(null));
            Assert.Equal("Product repository can not be null", exception.Message);
        }

        [Fact]
        public void GetOrders_NoFilter_Returns_ListOfAllOrders()
        {
            var expected = new List<Order>()
            {
                new Order() {Id = 1, OrderCompleted = false, CustomerId = 1, OrderPaid = true},
                new Order() {Id = 2, OrderCompleted = false, CustomerId = 1, OrderPaid = true},
            };

            _mock.Setup(r => r.ReadAllOrders())
                .Returns(expected);
            var actual = _service.ReadAllOrders();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DeleteOrder_Returns_BooleanValue()
        {
            var expected = true;
            _mock.Setup(r => r.DeleteOrder(1))
                .Returns(expected);
            var actual = _service.DeleteOrder(1);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UpdateOrder_Returns_BooleanValue()
        {
            List<Product> list = new List<Product>();

            Order order = new Order() {Id = 1, OrderCompleted = false, CustomerId = 1, OrderPaid = true};
            var expected = true;
            _mock.Setup(r => r.EditOrder(order))
                .Returns(expected); 
            var actual = _service.EditOrder(order);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateOrder_Returns_BooleanValue()
        {
            Order order = new Order() {Id = 1, OrderCompleted = false, CustomerId = 1, OrderPaid = true};
            var expected = new Order() {Id = 1, OrderCompleted = false, CustomerId = 1, OrderPaid = true};
            _mock.Setup(r => r.CreateOrder(order))
                .Returns(expected);
            var actual = _service.CreateOrder(order);
            Assert.Equal(expected, actual);
        }
    }
}