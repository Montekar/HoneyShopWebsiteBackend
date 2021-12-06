using System.Collections.Generic;
using System.IO;
using HoneyShop.Core.IServices;
using HoneyShop.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace HoneyShopWebsiteBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController: ControllerBase
    {
        private readonly IShoppingCartService _service;
        public ShoppingCartController(IShoppingCartService service)
        {
            _service = service ?? throw new InvalidDataException("Service can not be null");
        }
        
        [HttpPost]
        public ActionResult<ShoppingCart> AddItem([FromBody] ShoppingCart item)
        {
            var addItem = _service.AddItem(item);
            if (addItem != null)
            {
                return Ok("Item was created");
            }

            return BadRequest("Item was not created");
        }
        
        [HttpGet]
        public ActionResult<List<ShoppingCart>> GetAll()
        {
            return _service.GetAllItems();
        }
        
        [HttpPut]
        public ActionResult<ShoppingCart> UpdateItem([FromBody] ShoppingCart item)
        {
            
            var updateItem = _service.UpdateItem(item);
            if (updateItem != null)
            {
                return Ok("Item was updated");
            }

            return BadRequest("Item couldn't be updated");
        }

        [HttpDelete("{id}")]
        public ActionResult<ShoppingCart> DeleteItem(int id)
        {
            var deletedItem = _service.DeleteItem(id);
            if (deletedItem != null)
            {
                return Ok("Item was deleted");
            }
            return BadRequest("Item couldn't be deleted");
        }
        
        [HttpGet("{id}")]
        public ActionResult<ShoppingCart> GetItemById(int id)
        {
            var itemById = _service.GetItemById(id);
            if (itemById != null)
            {
                return itemById;
            }

            return BadRequest("Item was not found");
        }
    }
}