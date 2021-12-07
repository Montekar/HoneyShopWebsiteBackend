using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using HoneyShop.Core.Models;
using Xunit;
namespace HoneyShop.Core.Test.Models
{
    public class OrderTest
    {
        private Order _order;
        public OrderTest()
        {
            _order = new Order();
            _order.ProductList = new List<Product>();
        }
        
        
        [Fact]
        public void OrderClass_Exists()
        {
            Assert.NotNull(_order);
        }
        
        [Fact]
        public void OrderClass_HasId_WithTypeInt()
        {
            var expected = 1;
            _order.Id = 1;
            Assert.Equal(expected, _order.Id);
            Assert.True(_order.Id is int);
        }
        
        [Fact]
        public void OrderClass_HasCustomerId_WithTypeInt()
        {
            var expected = 1;
            _order.CustomerId = 1;
            Assert.Equal(expected, _order.CustomerId);
            Assert.True(_order.CustomerId is int);
        }
        
        
        [Fact]
        public void OrderClass_HasList_WithProductObject()
        {    var product1 = new Product() { Id = 1, Name = "Honey", Description = "1kg Dark Honey", Price = 5.00 };
            
            _order.ProductList.Add(product1);

            Assert.True(_order.ProductList.Count > 0);
            Assert.True(_order.ProductList[0].GetType() == typeof(Product));
        }

        [Fact]
        public void OrderClass_HasBool_OrderCompleted()
        {
            _order.OrderCompleted = true;
            
            Assert.True(_order.OrderCompleted);
        }

    }

}



