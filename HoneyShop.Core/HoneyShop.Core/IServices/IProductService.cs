using System.Collections.Generic;
using HoneyShop.Core.Models;

namespace HoneyShop.Core.IServices
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        bool DeleteProduct(int id);
        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        Product GetProductById(int id);
    }
}