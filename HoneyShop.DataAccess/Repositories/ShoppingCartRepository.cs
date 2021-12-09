using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HoneyShop.Core.Models;
using HoneyShop.DataAccess.Entities;
using HoneyShop.Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HoneyShop.DataAccess.Repositories
{
    public class ShoppingCartRepository: IShoppingCartRepository
    {
        private readonly HoneyDbContext _dbContext;

        public ShoppingCartRepository(HoneyDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new InvalidDataException("Shopping Cart Repository must have a DB context in constructor");
        }

        public List<ShoppingCart> GetAllItems()
        {
            return _dbContext.ShoppingCartItems.Select(sc => new ShoppingCart()
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
        }

        public ShoppingCart DeleteItem(int id)
        {
            var itemRemoved = _dbContext.ShoppingCartItems.FirstOrDefault(sc => sc.Id == id);
            if (itemRemoved != null)
            {
                _dbContext.RemoveRange(itemRemoved);
                _dbContext.SaveChanges();
                return new ShoppingCart()
                {
                    Id = itemRemoved.Id,
                    Product = new Product()
                    {
                        Id = itemRemoved.Product.Id,
                        Name = itemRemoved.Product.Name,
                        Description = itemRemoved.Product.Description,
                        Price = itemRemoved.Product.Price
                    },
                    Amount = itemRemoved.Amount
                };
            }
            return null;
        }

        public ShoppingCart AddItem(ShoppingCart shoppingCartItem)
        {
            var shoppingCartEntity = new ShoppingCartEntity()
            {
                Id = shoppingCartItem.Id,
                Product = new ProductEntity()
                {
                    Id = shoppingCartItem.Product.Id,
                    Name = shoppingCartItem.Product.Name,
                    Description = shoppingCartItem.Product.Description,
                    Price = shoppingCartItem.Product.Price
                },
                Amount = shoppingCartItem.Amount
            };

            _dbContext.ShoppingCartItems.Attach(shoppingCartEntity).State = EntityState.Added;
            _dbContext.SaveChanges();
            return shoppingCartItem;
        }

        public ShoppingCart UpdateItem(ShoppingCart shoppingCartItem)
        {
            if (shoppingCartItem != null)
            {
                _dbContext.Update(shoppingCartItem);
                _dbContext.SaveChanges();
                return shoppingCartItem;
            }

            return null;
        }

        public ShoppingCart GetItemById(int id)
        {
            var itemRemoved = _dbContext.ShoppingCartItems.FirstOrDefault(sc => sc.Id == id);
            if (itemRemoved != null)
            {
                return new ShoppingCart()
                {
                    Id = itemRemoved.Id,
                    Product = new Product()
                    {
                        Id = itemRemoved.Product.Id,
                        Name = itemRemoved.Product.Name,
                        Description = itemRemoved.Product.Description,
                        Price = itemRemoved.Product.Price
                    },
                    Amount = itemRemoved.Amount
                };
            }
            return null;
        }

        public bool EmptyCart()
        {
            //To be implemented
            return true;
        }
    }
}