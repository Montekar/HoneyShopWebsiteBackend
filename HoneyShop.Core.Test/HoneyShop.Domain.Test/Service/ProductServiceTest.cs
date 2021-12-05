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
    public class ProductServiceTest
    {
        private readonly Mock<IProductRepository> _mock;
        private readonly ProductService _service;

        public ProductServiceTest()
        {
            _mock = new Mock<IProductRepository>();
            _service = new ProductService(_mock.Object);
        }

        [Fact]
        public void ProductService_IsIProductService()
        {
            Assert.True(_service is IProductService);
        }

        [Fact]
        public void ProductService_WithNullProductRepository_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => new ProductService(null));
            Assert.Equal("Product repository can not be null", exception.Message);
        }

        [Fact]
        public void GetProducts_NoFilter_Returns_ListOfAllProducts()
        {
            var expected = new List<Product>()
            {
                new Product(){Id = 1, Name = "Bee bread small", Description = "Bee Bread 100g", Price = 4.00},
                new Product(){Id = 1, Name = "Bee bread large", Description = "Bee Bread 200g", Price = 8.00},
            };

            _mock.Setup(r => r.GetAllProducts())
                .Returns(expected);
            var actual = _service.GetAllProducts();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DeleteProduct_Returns_BooleanValue()
        {
            var expected = true;
            _mock.Setup(r => r.DeleteProduct(1))
                .Returns(expected);
            var actual = _service.DeleteProduct(1);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UpdateProduct_Returns_BooleanValue()
        {
            Product product = new Product() { Id = 1, Name = "Soup", Description = "Soup with honey", Price = 3.50 };
            var expected = true;
            _mock.Setup(r => r.UpdateProduct(product))
                .Returns(expected);
            var actual = _service.UpdateProduct(product);
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void CreateProduct_Returns_BooleanValue()
        {
            Product product = new Product() { Id = 1, Name = "Soup", Description = "Soup with honey", Price = 3.50 };
            var expected = true;
            _mock.Setup(r => r.CreateProduct(product))
                .Returns(expected);
            var actual = _service.CreateProduct(product);
            Assert.Equal(expected, actual);
        }
        
    }
}