﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using HoneyShop.Core.Models;
using HoneyShop.DataAccess.Entities;
using HoneyShop.Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HoneyShop.DataAccess.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly HoneyContext _honeyContext;

        public ProductRepository(HoneyContext honeyContext)
        {
            _honeyContext = honeyContext ?? throw new InvalidDataException("Product Repository must have a DB context in constructor");
        }
        public List<Product> GetAllProducts()
        {
            return _honeyContext.Products.Select(pe => new Product()
            {
                Id = pe.Id,
                Name = pe.Name,
                Description = pe.Description,
                Price = pe.Price
            }).ToList();
        }

        public bool DeleteProduct(int id)
        {
            var productToRemove = _honeyContext.Products.Where(p => p.Id == id);
            if (productToRemove != null)
            {
                _honeyContext.RemoveRange(productToRemove);
                _honeyContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateProduct(Product product)
        {

            if (product != null)
            {
                _honeyContext.Update(product);
                _honeyContext.SaveChanges();
                return true;
            }

            return false;

        }

        public bool CreateProduct(Product product)
        {
            var productEntity = new ProductEntity()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
            _honeyContext.Products.Attach(productEntity).State = EntityState.Added;
            _honeyContext.SaveChanges();
            return true;
        }

        public Product GetProductById(int id)
        {
            var productById = _honeyContext.Products.FirstOrDefault(product => id.Equals(product.Id));
            if (productById != null)
            {
                return new Product()
                {
                    Id = productById.Id,
                    Name = productById.Name,
                    Description = productById.Description,
                    Price = productById.Price
                };
            }

            return null;
        }
    }
}