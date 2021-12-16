using System.Collections.Generic;
using System.IO;
using System.Linq;
using HoneyShop.Core.Models;
using HoneyShop.DataAccess.Entities;
using HoneyShop.Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HoneyShop.DataAccess.Repositories
{
    /*public class InClassName
    {
        public InClassName(int OrderId)
        {
            this.OrderId = OrderId;
        }

        public int OrderId { get; private set; }
    }

    public class Order
    {
        public Order(InClassName inClassName)
        {
            InClassName = inClassName;
        }

        public InClassName InClassName { get; private set; }
    }
    */

    public class OrderRepository : IOrderRepository
    {
        private readonly HoneyDbContext _honeyContext;

        public OrderRepository(HoneyDbContext honeyContext)
        {
            _honeyContext = honeyContext ?? throw new InvalidDataException("Product Repository must have a DB context in constructor");
        }
        
        public Order CreateOrder(Order order)
        {
            var orderEntity = new OrderEntity()
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                OrderPaid = false,
                OrderCompleted = false,

            };
            _honeyContext.Order.Add(orderEntity);
            _honeyContext.SaveChanges();
            return new Order
            {
                Id = orderEntity.Id,
                CustomerId = orderEntity.CustomerId,
                OrderPaid = orderEntity.OrderPaid,
                OrderCompleted = orderEntity.OrderCompleted
            };
        }
    }
}