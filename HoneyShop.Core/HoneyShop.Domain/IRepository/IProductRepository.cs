using System.Collections.Generic;
using HoneyShop.Core.Models;

namespace HoneyShop.Domain.IRepository
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        bool DeleteProduct(int i);
        bool UpdateProduct(Product product);
        bool CreateProduct(Product product);

    }
}