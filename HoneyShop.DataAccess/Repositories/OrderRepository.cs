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
        private readonly HoneyContext _honeyContext;

        public OrderRepository(HoneyContext honeyContext)
        {
            _honeyContext = honeyContext ?? throw new InvalidDataException("Product Repository must have a DB context in constructor");
        }
        
        public Order ReadSingleOrder(int orderId)
        {

            return _honeyContext.Order
                .Select(o => new Order()
                {
                    Id = o.Id,
                    CustomerId = o.CustomerId,
                    ProductList = o.ProductList,
                    OrderCompleted = o.OrderCompleted
                }).FirstOrDefault(i => i.Id == orderId);
        } 

        public List<Order> ReadAllOrders()
        {
            return _honeyContext.Order.Select(oe => new Order()
            {
                Id = oe.Id,
                OrderCompleted = oe.OrderCompleted,
                CustomerId = oe.CustomerId,
                ProductList = oe.ProductList
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
        

        public bool CreateOrder(Order order)
        {
            var orderEntity = new OrderEntity()
            {
                Id = order.Id,
                OrderCompleted = order.OrderCompleted,
                ProductList = order.ProductList
          
            };
            _honeyContext.Order.Attach(orderEntity).State = EntityState.Added;
            _honeyContext.SaveChanges();
            return true;        }

        public bool EditOrder(Order order)
        {
            if (order != null)
            {
                var orderEntity = new OrderEntity()
                {
                    Id = order.Id,
                    CustomerId = order.CustomerId,
                    ProductList = order.ProductList,
                    OrderCompleted = order.OrderCompleted
                };
                var savedEntity = _honeyContext.Order.Update(orderEntity).Entity;
                _honeyContext.SaveChanges();
                return true;
            }
            return false;
        }


    }
}