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
        public void GetOrders_NoFilter_Returns_ListOfAllOrders()
        { Product _product = new Product();
            List<Product> list = new List<Product>();

            var expected = new List<Order>()
            {
               
                new Order(){Id = 1, OrderCompleted = false, CustomerId = 1, ProductList = (list)},
                new Order(){Id = 1,OrderCompleted = false, CustomerId = 1, ProductList = (list)},
                
  

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

            Order order = new Order() {Id = 1, OrderCompleted = false, CustomerId = 1, ProductList = (list)};
            var orderi = new Order();
            var expected = true;
            _mock.Setup(r => r.EditOrder(order))
                .Returns(expected); 
            var actual = _service.EditOrder(order);
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void CreateOrder_Returns_BooleanValue()
        {            
            List<Product> list = new List<Product>();

            Order order = new Order() {Id = 1, OrderCompleted = false, CustomerId = 1, ProductList = (list)};
            var expected = true;
            _mock.Setup(r => r.CreateOrder(order))
                .Returns(expected);
            var actual = _service.CreateOrder(order);
            Assert.Equal(expected, actual);
        }
    }


}