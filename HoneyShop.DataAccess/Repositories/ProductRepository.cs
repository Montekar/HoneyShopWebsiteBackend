using System.Collections.Generic;
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
        private readonly HoneyDbContext _honeyDbContext;

        public ProductRepository(HoneyDbContext honeyDbContext)
        {
            _honeyDbContext = honeyDbContext ?? throw new InvalidDataException("Product Repository must have a DB context in constructor");
        }
        public List<Product> GetAllProducts()
        {
            return _honeyDbContext.Products.Select(pe => new Product()
            {
                Id = pe.Id,
                Name = pe.Name,
                Description = pe.Description,
                Price = pe.Price
            }).ToList();
        }

        public bool DeleteProduct(int id)
        {
            var productToRemove = _honeyDbContext.Products.Where(p => p.Id == id);
            if (productToRemove != null)
            {
                _honeyDbContext.RemoveRange(productToRemove);
                _honeyDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateProduct(Product product)
        {
            var entity = new ProductEntity()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };

            if (product != null)
            {
                _honeyDbContext.Update(entity);
                _honeyDbContext.SaveChanges();
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
            _honeyDbContext.Products.Attach(productEntity).State = EntityState.Added;
            _honeyDbContext.SaveChanges();
            return true;
        }

        public Product GetProductById(int id)
        {
            var productById = _honeyDbContext.Products.FirstOrDefault(product => id.Equals(product.Id));
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