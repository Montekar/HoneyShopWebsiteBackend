using System.Collections.Generic;
using HoneyShop.Core.IServices;
using HoneyShop.Core.Models;
using Moq;
using Xunit;

namespace HoneyShop.Core.Test.IServices
{
    public class IProductServiceTest
    {
        private readonly Mock<IProductService> _service;

        public IProductServiceTest()
        {
            _service = new Mock<IProductService>();
        }

        [Fact]
        public void IProductsService_Exists()
        {
            Assert.NotNull(_service.Object);
        }

        [Fact]
        public void GetAll_WithNoParams_ReturnsList()
        {
            var expectedList = new List<Product>()
            {
                new Product() { Id = 1, Name = "Soup", Description = "Soup with honey", Price = 3.50 },
                new Product() { Id = 2, Name = "Soup", Description = "Soup with honey and goat milk", Price = 3.50 },
            };
            _service.Setup(ps => ps.GetAllProducts())
                .Returns(expectedList);
            
            Assert.Equal(expectedList, _service.Object.GetAllProducts());
        }

        [Fact]
        public void DeleteProduct_ReturnsTrue()
        {
            var expected = true;
            _service.Setup(ps => ps.DeleteProduct(1))
                .Returns(expected);
            
            Assert.Equal(expected, _service.Object.DeleteProduct(1));
        }

        [Fact]
        public void CreateProduct_ReturnsTrue()
        {
            var expected = true;
            var product = new Product(){ Id = 1, Name = "Soup", Description = "Soup with honey", Price = 3.50 };
            _service.Setup(ps => ps.CreateProduct(product))
                .Returns(expected);
            
            Assert.Equal(expected, _service.Object.CreateProduct(product));
        }

        [Fact]
        public void UpdateProduct_ReturnsTrue()
        {
            var expected = true;
            var product = new Product(){ Id = 1, Name = "Soup", Description = "Soup with honey", Price = 3.50 };
            _service.Setup(ps => ps.UpdateProduct(product))
                .Returns(expected);
            
            Assert.Equal(expected, _service.Object.UpdateProduct(product));
        }
    }
}