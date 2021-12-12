﻿using System.Collections.Generic;
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
            _honeyContext = honeyContext ??
                            throw new InvalidDataException("Product Repository must have a DB context in constructor");
        }

        public Order ReadSingleOrder(int orderId)
        {
            return _honeyContext.Order
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Product)
                .Select(o => new Order
                {
                    Id = o.Id,
                    CustomerId = o.CustomerId,
                    ProductList = o.OrderLines != null
                        ? o.OrderLines.Select(ol => new Product
                        {
                            Id = ol.ProductId,
                            Description = ol.Product.Description,
                            Name = ol.Product.Name,
                            Price = ol.Product.Price
                        }).ToList()
                        : null,
                    OrderCompleted = o.OrderCompleted
                }).FirstOrDefault(i => i.Id == orderId);
        }

        public List<Order> ReadAllOrders()
        {
            return _honeyContext.Order
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Product)
                .Select(o => new Order
                {
                    Id = o.Id,
                    OrderCompleted = o.OrderCompleted,
                    CustomerId = o.CustomerId,
                    ProductList = o.OrderLines != null
                        ? o.OrderLines.Select(ol => new Product
                        {
                            Id = ol.ProductId,
                            Description = ol.Product.Description,
                            Name = ol.Product.Name,
                            Price = ol.Product.Price
                        }).ToList()
                        : null
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
            var orderEntity = new OrderEntity
            {
                Id = order.Id,
                OrderCompleted = order.OrderCompleted,
                OrderLines = order.ProductList != null
                    ? order.ProductList.Select(p => new OrderLineEntity()
                    {
                        ProductId = p.Id,
                        Product = new ProductEntity()
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            Price = p.Price
                        },
                        OrderId = order.Id,
                        Order = new OrderEntity()
                        {
                            OrderCompleted = order.OrderCompleted,
                            Id = order.Id,
                            CustomerId = order.CustomerId,
                        }
                    }).ToList()
                    : null,
            };
            _honeyContext.Order.Attach(orderEntity).State = EntityState.Added;
            _honeyContext.SaveChanges();
            return true;
        }

        public bool EditOrder(Order order)
        {
            if (order != null)
            {
                var orderEntity = new OrderEntity
                {
                    Id = order.Id,
                    CustomerId = order.CustomerId,
                    OrderLines = order.ProductList != null
                        ? order.ProductList.Select(p => new OrderLineEntity()
                        {
                            ProductId = p.Id,
                            Product = new ProductEntity()
                            {
                                Id = p.Id,
                                Name = p.Name,
                                Description = p.Description,
                                Price = p.Price
                            },
                            OrderId = order.Id,
                            Order = new OrderEntity()
                            {
                                OrderCompleted = order.OrderCompleted,
                                Id = order.Id,
                                CustomerId = order.CustomerId,
                            }
                        }).ToList()
                        : null,
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