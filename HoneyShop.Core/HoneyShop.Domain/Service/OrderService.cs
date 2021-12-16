using System.Collections.Generic;
using System.IO;
using HoneyShop.Core.IServices;
using HoneyShop.Core.Models;
using HoneyShop.Domain.IRepository;

namespace HoneyShop.Domain.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new InvalidDataException("Product repository can not be null");
        }

        public Order CreateOrder(Order order)
        {
            return _orderRepository.CreateOrder(order);
        }
        
        public Order ReadSingleOrder(int OrderId)
        {
            return _orderRepository.ReadSingleOrder(OrderId);
        }

        public List<Order> ReadAllOrders()
        {
            return _orderRepository.ReadAllOrders();
        }

        public bool DeleteOrder(int OrderId)
        {
            return _orderRepository.DeleteOrder(OrderId);
        }

        public bool EditOrder(Order order)
        {
            return _orderRepository.EditOrder(order);
        }

    }
}