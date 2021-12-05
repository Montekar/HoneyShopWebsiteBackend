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
    public class ShoppingCartServiceTest
    {
        private readonly Mock<IShoppingCartRepository> _mock;
        private readonly ShoppingCartService _service;

        public ShoppingCartServiceTest()
        {
            _mock = new Mock<IShoppingCartRepository>();
            _service = new ShoppingCartService(_mock.Object);
        }

        [Fact]
        public void ShoppingCartService_IsIShoppingCartService()
        {
            Assert.True(_service is IShoppingCartService);
        }

        [Fact]
        public void ShoppingCartService_WithNullShoppingCartRepository_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => new ShoppingCartService(null));
            Assert.Equal("Shopping cart repository can not be null",exception.Message);
        }
        
        [Fact]
        public void GetAllItems_WithNoParams_ReturnsListOfShoppingCart()
        {
            var expectedList = new List<ShoppingCart>()
            {
                new ShoppingCart()
                {
                    Id = 1,
                    Product = new Product() { Id = 1, Name = "Soup", Description = "Soup with honey", Price = 3.50 },
                    Amount = 3,
                },
                new ShoppingCart()
                {
                    Id = 2,
                    Product = new Product() { Id = 2, Name = "Soup", Description = "Soup with honey and goat milk", Price = 3.50 },
                    Amount = 4,
                }
            };

            _mock.Setup(shoppingCartRepository => shoppingCartRepository.GetAllItems())
                .Returns(expectedList);
            
            Assert.Equal(expectedList, _mock.Object.GetAllItems());
        }

        [Fact]
        public void DeleteItem_ReturnsDeletedItem()
        {
            var expected = new ShoppingCart()
            {
                Id = 1,
                Product = new Product() { Id = 1, Name = "Soup", Description = "Soup with honey", Price = 3.50 },
                Amount = 3,
            };

            var shoppingCartItem = new ShoppingCart()
            {
                Id = 1,
                Product = new Product() { Id = 1, Name = "Soup", Description = "Soup with honey", Price = 3.50 },
                Amount = 3,
            };

            _mock.Setup(shoppingCartRepository => shoppingCartRepository.DeleteItem(1))
                .Returns(expected);
            
            Assert.Equal(expected, _mock.Object.DeleteItem(1));
        }
        
        [Fact]
        public void AddItem_ReturnsAddedItem()
        {
            var expected = new ShoppingCart()
            {
                Id = 1,
                Product = new Product() { Id = 1, Name = "Soup", Description = "Soup with honey", Price = 3.50 },
                Amount = 3,
            };

            var shoppingCartItem = new ShoppingCart()
            {
                Id = 1,
                Product = new Product() { Id = 1, Name = "Soup", Description = "Soup with honey", Price = 3.50 },
                Amount = 3,
            };

            _mock.Setup(shoppingCartRepository => shoppingCartRepository.AddItem(shoppingCartItem))
                .Returns(expected);
            
            Assert.Equal(expected, _mock.Object.AddItem(shoppingCartItem));
        }
        
        [Fact]
        public void UpdateItem_ReturnsUpdatedItem()
        {
            var expected = new ShoppingCart()
            {
                Id = 1,
                Product = new Product() { Id = 1, Name = "Soup", Description = "Soup with honey", Price = 3.50 },
                Amount = 3,
            };

            var shoppingCartItem = new ShoppingCart()
            {
                Id = 1,
                Product = new Product() { Id = 1, Name = "Soup", Description = "Soup with honey", Price = 3.50 },
                Amount = 3,
            };

            _mock.Setup(shoppingCartRepository => shoppingCartRepository.UpdateItem(shoppingCartItem))
                .Returns(expected);
            
            Assert.Equal(expected, _mock.Object.UpdateItem(shoppingCartItem));
        }
        
        [Fact]
        public void GetItemById_ReturnsItem()
        {
            var expected = new ShoppingCart()
            {
                Id = 1,
                Product = new Product() { Id = 1, Name = "Soup", Description = "Soup with honey", Price = 3.50 },
                Amount = 3,
            };

            _mock.Setup(shoppingCartRepository => shoppingCartRepository.GetItemById(1))
                .Returns(expected);
            
            Assert.Equal(expected, _mock.Object.GetItemById(1));
        }
        
        [Fact]
        public void EmptyShoppingCart_ReturnsBool()
        {
            var expected = true;

            _mock.Setup(shoppingCartRepository => shoppingCartRepository.EmptyCart())
                .Returns(expected);
            
            Assert.Equal(expected, _mock.Object.EmptyCart());
        }
    }
}