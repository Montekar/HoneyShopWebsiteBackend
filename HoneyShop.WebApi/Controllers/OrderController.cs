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
        public ActionResult<Order> CreateCustomerDetails(OrderDto orderDto)
        {
            var order = new Order
            {
                CustomerId = orderDto.CustomerId,
                OrderCompleted = orderDto.OrderCompleted,
                OrderPaid = orderDto.OrderPaid
            };
            return _service.CreateOrder(order);
        }

    }
}