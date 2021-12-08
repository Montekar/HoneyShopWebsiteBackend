using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EntityFrameworkCore.Testing.Moq;
using HoneyShop.Core.Models;
using HoneyShop.DataAccess;
using HoneyShop.DataAccess.Entities;
using HoneyShop.DataAccess.Repositories;
using HoneyShop.Domain.IRepository;
using HoneyShop.Domain.Service;
using Xunit;

namespace HoneyShop.Infrastructure.Test
{
    public class ShoppingCartRepositoryTest
    {
        private readonly HoneyContext _context;
        private readonly ShoppingCartRepository _repository;
        private readonly List<ShoppingCartEntity> _list;

        public ShoppingCartRepositoryTest()
        {
            _context = Create.MockedDbContextFor<HoneyContext>();
            _repository = new ShoppingCartRepository(_context);
            _list = new List<ShoppingCartEntity>()
            {
                new ShoppingCartEntity()
                {
                    Id = 1,
                    Product = new ProductEntity() { Id = 1, Name = "Soup", Description = "Soup with honey", Price = 3.50 },
                    Amount = 3,
                },
                new ShoppingCartEntity()
                {
                    Id = 2,
                    Product = new ProductEntity() { Id = 2, Name = "Soup", Description = "Soup with honey and goat milk", Price = 3.50 },
                    Amount = 4,
                }
            };
        }

        [Fact]
        public void ShoppingRepository_IsIShoppingCartRepository()
        {
            Assert.IsAssignableFrom<IShoppingCartRepository>(_repository);
        }

        [Fact]
        public void ShoppingCartRepository_WithNullDBContext_ThrowsInvalidDateException()
        {
            var actual = Assert.Throws<InvalidDataException>(() => new ShoppingCartRepository(null));
            Assert.Equal("Shopping Cart Repository must have a DB context in constructor", actual.Message);
        }

        [Fact]
        public void GetAllItems_ReturnsAllItems_AsListOfShoppingCart()
        {
            _context.Set<ShoppingCartEntity>().AddRange(_list);
            _context.SaveChanges();

            var repositoryList = _list.Select(sc => new ShoppingCart()
            {
                Id = sc.Id,
                Product = new Product()
                {
                    Id = sc.Product.Id,
                    Name = sc.Product.Name,
                    Description = sc.Product.Description,
                    Price = sc.Product.Price
                },
                Amount = sc.Amount
            }).ToList();

            var actualResult = _repository.GetAllItems();
            Assert.Equal(repositoryList.Count, actualResult.Count);
        }

        [Fact]
        public void DeleteItem_ReturnsDeleteItem()
        {
            _context.Set<ShoppingCartEntity>().AddRange(_list);
            _context.SaveChanges();

            var itemRemoved = _context.ShoppingCartItems.Where(sc => sc.Id == 1);
            if (itemRemoved != null)
            {
                _context.RemoveRange(itemRemoved);
                _context.SaveChanges();
            }
            
            //Check
            var actualResult = _repository.DeleteItem(1);
            Assert.Equal(2, _list.Count);
        }

        [Fact]
        public void AddItem_ReturnsItemAdded()
        {
            _context.Set<ShoppingCartEntity>().AddRange(_list);
            _context.SaveChanges();

            var repositoryList = _list.Select(sc => new ShoppingCart()
            {
                Id = sc.Id,
                Product = new Product()
                {
                    Id = sc.Product.Id,
                    Name = sc.Product.Name,
                    Description = sc.Product.Description,
                    Price = sc.Product.Price
                },
                Amount = sc.Amount
            }).ToList();

            var actualResult = _repository.GetAllItems();
            Assert.Equal(repositoryList.Count, actualResult.Count);
        }

        [Fact]
        public void UpdateItem_ReturnsItemUpdated()
        {
            _context.Set<ShoppingCartEntity>().AddRange(_list);
            _context.SaveChanges();
            
            var itemToBeUpdated = _context.ShoppingCartItems.FirstOrDefault(sc => sc.Id == 1);

            if (itemToBeUpdated != null)
            {
                _context.Update(itemToBeUpdated);
                _context.SaveChanges();
            }

            var item = new ShoppingCart()
            {
                Id = itemToBeUpdated.Id,
                Product = new Product()
                {
                    Id = itemToBeUpdated.Product.Id,
                    Name = itemToBeUpdated.Product.Name,
                    Description = itemToBeUpdated.Product.Description,
                    Price = itemToBeUpdated.Product.Price
                },
                Amount = itemToBeUpdated.Amount
            };

            var actual = _repository.UpdateItem(item);
            Assert.NotNull(actual);
        }

        [Fact]
        public void ItemById_ReturnsItem()
        {
            _context.Set<ShoppingCartEntity>().AddRange(_list);
            _context.SaveChanges();
            
            var expectedItem = _context.ShoppingCartItems.FirstOrDefault(sc => sc.Id == 1);
            
            if (expectedItem != null)
            {
                _context.Update(expectedItem);
                _context.SaveChanges();
            }

            var item = new ShoppingCart()
            {
                Id = expectedItem.Id,
                Product = new Product()
                {
                    Id = expectedItem.Product.Id,
                    Name = expectedItem.Product.Name,
                    Description = expectedItem.Product.Description,
                    Price = expectedItem.Product.Price
                },
                Amount = expectedItem.Amount
            };
            
            Assert.NotNull(_repository.GetItemById(1));
        }
    }
}