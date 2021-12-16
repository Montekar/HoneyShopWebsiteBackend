using System.Collections.Generic;
using System.IO;
using System.Linq;
using HoneyShop.Core.Models;
using HoneyShop.DataAccess.Entities;
using HoneyShop.Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HoneyShop.DataAccess.Repositories
{
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
        
        public Order ReadSingleOrder(int orderId)
        {

            return _honeyContext.Order
                .Select(o => new Order()
                {
                    Id = o.Id,
                    CustomerId = o.CustomerId,
                    OrderCompleted = o.OrderCompleted,
                    OrderPaid = o.OrderPaid
                }).FirstOrDefault(i => i.Id == orderId);
        } 

        public List<Order> ReadAllOrders()
        {
            return _honeyContext.Order.Select(oe => new Order()
            {
                Id = oe.Id,
                CustomerId = oe.CustomerId,
                OrderCompleted = oe.OrderCompleted,
                OrderPaid = oe.OrderPaid
            }).ToList();

        }

        public bool DeleteOrder(int OrderId)
        {
            var productToRemove = _honeyContext.Order.Where(o => o.Id == OrderId);
            if (productToRemove != null)
            {
                _honeyContext.RemoveRange(productToRemove);
                _honeyContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool EditOrder(Order order)
        {
            var entity = new OrderEntity()
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                OrderCompleted = order.OrderCompleted,
                OrderPaid = order.OrderPaid
            };

            if (entity == null) return false;
            _honeyContext.Update(entity);
            _honeyContext.SaveChanges();
            return true;

        }
    }
}