using HoneyShop.Core.Models;
using Xunit;

namespace HoneyShop.Core.Test.Models
{
    public class ShoppingCartTest
    {
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartTest()
        {
            _shoppingCart = new ShoppingCart();
        }

        [Fact]
        public void ShoppingCart_Exists()
        {
            Assert.NotNull(_shoppingCart);
        }

        [Fact]
        public void ShoppingCart_HasId_WithTypeInt()
        {
            var expected = 1;
            _shoppingCart.Id = 1;
            Assert.Equal(expected, _shoppingCart.Id);
            Assert.True(_shoppingCart.Id is int);
        }
        
        [Fact]
        public void ShoppingCart_HasProduct_WithTypeProduct()
        {
            var expected = new Product()
                { Id = 1, Name = "Bee bread small", Description = "Bee Bread 100g", Price = 4.00 };
            _shoppingCart.Product = new Product()
                { Id = 1, Name = "Bee bread small", Description = "Bee Bread 100g", Price = 4.00 };
            Assert.Equal(expected, _shoppingCart.Product);
            Assert.True(_shoppingCart.Product is Product);
        }
        
        [Fact]
        public void ShoppingCart_HasAmount_WithTypeInt()
        {
            var expected = 1;
            _shoppingCart.Amount = 1;
            Assert.Equal(expected, _shoppingCart.Amount);
            Assert.True(_shoppingCart.Amount is int);
        }

    }
}