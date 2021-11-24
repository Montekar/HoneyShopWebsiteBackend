using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HoneyShop.Core.IServices;
using HoneyShop.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HoneyShopWebsiteBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service ?? throw new InvalidDataException("Product service can not be null");
        }

        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] Product product)
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                return BadRequest("Name is required");
            }
            var createProduct = _service.CreateProduct(product);
            if (createProduct)
            {
                return Ok("Product was created");
            }

            return BadRequest("Product was not created");
        }

        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            return _service.GetAllProducts();
        }

        [HttpPut("{id}")]
        public ActionResult<Product> UpdateProduct(int id,[FromBody] Product product)
        {
            if (id < 1 || id != product.Id)
            {
                return BadRequest("Parameter Id and product Id must be the same");
            }
            
            var updateProduct = _service.UpdateProduct(product);
            if (updateProduct)
            {
                return Ok("Product was updated");
            }

            return BadRequest("Product couldn't be updated");
        }

        [HttpDelete("{id}")]
        public ActionResult<Product> DeleteProduct(int id)
        {
            var deletedProduct = _service.DeleteProduct(id);
            if (deletedProduct)
            {
                return Ok("Product was deleted");
            }
            return BadRequest("Product couldn't be deleted");
        }
        
        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var productById = _service.GetProductById(id);
            if (productById != null)
            {
                return productById;
            }

            return BadRequest("Product was not found");
        }
    }
}