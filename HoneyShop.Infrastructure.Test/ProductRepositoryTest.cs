using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EntityFrameworkCore.Testing.Moq;
using HoneyShop.Core.Models;
using HoneyShop.DataAccess;
using HoneyShop.DataAccess.Entities;
using HoneyShop.DataAccess.Repositories;
using HoneyShop.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace HoneyShop.Infrastructure.Test
{
    public class ProductRepositoryTest
    {
        private readonly HoneyDbContext _fakeDbContext;
        private readonly ProductRepository _productRepository;
        private readonly List<ProductEntity> _list;
        public ProductRepositoryTest()
        {
            _fakeDbContext = Create.MockedDbContextFor<HoneyDbContext>();
            _productRepository = new ProductRepository(_fakeDbContext);
            _list = new List<ProductEntity>
            {
                new ProductEntity()
                {
                    Id = 1,
                    Name = "Honey",
                    Description = "1kg of fresh honey",
                    Price = 5.00
                },
                new ProductEntity()
                {
                    Id = 2,
                    Name = "Bee bread",
                    Description = "100g of bee bread",
                    Price = 4.00
                },
                new ProductEntity()
                {
                    Id = 3,
                    Name = "Soup",
                    Description = "Natural soup with honey",
                    Price = 3.5
                }
            };

        }

        [Fact]
        public void ProductRepository_IsIProductRepository()
        {
            Assert.IsAssignableFrom<IProductRepository>(_productRepository);
        }
        
        [Fact]
        public void ProductRepository_WithNullDBContext_ThrowsInvalidDataException()
        {
            var actual = Assert.Throws<InvalidDataException>(() => new ProductRepository(null));
            Assert.Equal("Product Repository must have a DB context in constructor", actual.Message);
        }

        [Fact]
        public void FindAll_GetAllProductsEntitiesInDBContext_AsAListOfProduct()
        {
            _fakeDbContext.Set<ProductEntity>().AddRange(_list);
            _fakeDbContext.SaveChanges();

            var repositoryList = _list.Select(pe => new Product()
            {
                Id = pe.Id,
                Name = pe.Name,
                Description = pe.Description,
                Price = pe.Price
            }).ToList();

            var actualResult = _productRepository.GetAllProducts();
            Assert.Equal(repositoryList,actualResult, new Comparer());
        }

        [Fact]
        public void DeleteProduct_DeleteProductInDBContext_ReturnBoolean()
        {
            _fakeDbContext.Set<ProductEntity>().AddRange(_list);
            _fakeDbContext.SaveChanges();

            var productToRemove = _fakeDbContext.Products.Where(p => p.Id == 1);
            if (productToRemove != null)
            {
                _fakeDbContext.RemoveRange(productToRemove);
                _fakeDbContext.SaveChanges();
            }

            var actual = _productRepository.DeleteProduct(1);
            Assert.Equal(3, _list.Count);
            Assert.True(actual);
        }
        
        /*[Fact]
        public void UpdateProduct_UpdateProductInDBContext_ReturnProduct()
        {
            _fakeDbContext.Set<ProductEntity>().AddRange(_list);
            _fakeDbContext.SaveChanges();
            
            var productToUpdate = _fakeDbContext.Products.FirstOrDefault(p => p.Id == 1);
            
            if (productToUpdate != null)
            {
                _fakeDbContext.Update(productToUpdate);
                _fakeDbContext.SaveChanges();
            }

            var product = new Product()
            {
                Id = productToUpdate.Id,
                Name = productToUpdate.Name,
                Description = "1kg of honey",
                Price = 5.00
            };
            
            var actual = _productRepository.UpdateProduct(product);
            Assert.True(actual);
        }*/
        
        public class Comparer: IEqualityComparer<Product>
        {
            public bool Equals(Product x, Product y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id && x.Name == y.Name && x.Description == y.Description && x.Price.Equals(y.Price);
            }

            public int GetHashCode(Product obj)
            {
                return HashCode.Combine(obj.Id, obj.Name, obj.Description, obj.Price);
            }
        }
    }
}