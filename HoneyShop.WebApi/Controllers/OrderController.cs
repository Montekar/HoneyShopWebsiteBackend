using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HoneyShop.Core.IServices;
using HoneyShop.Core.Models;
using HoneyShopWebsiteBackend.Dto.UserDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HoneyShopWebsiteBackend.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service ?? throw new InvalidDataException("Email service can not be null");
        }
        
        [HttpPost]
        public ActionResult<Order> CreateOrder(OrderDto orderDto)
        {
            var order = new Order
            {
                CustomerId = orderDto.CustomerId,
                OrderCompleted = orderDto.OrderCompleted,
                OrderPaid = orderDto.OrderPaid
            };
            return _service.CreateOrder(order);
        }
        
        [HttpPut]
        public ActionResult<Order> UpdateOrder([FromBody] OrderDto orderDto)
        {
            var order = new Order
            {
                CustomerId = orderDto.CustomerId,
                OrderCompleted = orderDto.OrderCompleted,
                OrderPaid = orderDto.OrderPaid
            };
            var updateItem = _service.EditOrder(order);
            if (updateItem)
            {
                return Ok("Order was updated");
            }

            return BadRequest("Order couldn't be updated");
        }

        [HttpDelete("{id}")]
        public ActionResult<Order> DeleteItem(int id)
        {
            var deletedItem = _service.DeleteOrder(id);
            if (deletedItem)
            {
                return Ok("Order was deleted");
            }
            return BadRequest("Order couldn't be deleted");
        }
        
        [HttpGet("{id}")]
        public ActionResult<Order> GetItemById(int id)
        {
            var order = _service.ReadSingleOrder(id);
            if (order!= null)
            {
                return order;
            }

            return BadRequest("Item was not found");
        }

    }
}