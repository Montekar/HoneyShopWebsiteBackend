using System.Collections.Generic;
using HoneyShop.Core.IServices;
using HoneyShop.Core.Models;
using Moq;
using Xunit;

namespace HoneyShop.Core.Test.IServices
{
    public class InterfaceShoppingCartService
    {
        private readonly Mock<IShoppingCartService> _service;

        public InterfaceShoppingCartService()
        {
            _service = new Mock<IShoppingCartService>();
        }

        [Fact]
        public void IShoppingCartService_Exists()
        {
            Assert.NotNull(_service.Object);
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

            _service.Setup(shoppingCartService => shoppingCartService.GetAllItems())
                .Returns(expectedList);
            
            Assert.Equal(expectedList, _service.Object.GetAllItems());
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

            _service.Setup(shoppingCartService => shoppingCartService.DeleteItem(1))
                .Returns(expected);
            
            Assert.Equal(expected, _service.Object.DeleteItem(1));
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

            _service.Setup(shoppingCartService => shoppingCartService.AddItem(shoppingCartItem))
                .Returns(expected);
            
            Assert.Equal(expected, _service.Object.AddItem(shoppingCartItem));
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

            _service.Setup(shoppingCartService => shoppingCartService.UpdateItem(shoppingCartItem))
                .Returns(expected);
            
            Assert.Equal(expected, _service.Object.UpdateItem(shoppingCartItem));
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

            _service.Setup(shoppingCartService => shoppingCartService.GetItemById(1))
                .Returns(expected);
            
            Assert.Equal(expected, _service.Object.GetItemById(1));
        }

        [Fact]
        public void EmptyShoppingCart_ReturnsBool()
        {
            var expected = true;

            _service.Setup(shoppingCartService => shoppingCartService.EmptyCart())
                .Returns(expected);
            
            Assert.Equal(expected, _service.Object.EmptyCart());
        }
    }
}