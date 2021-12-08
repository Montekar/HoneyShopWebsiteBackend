using System.Collections.Generic;
using HoneyShop.Core.Models;

namespace HoneyShop.Domain.IRepository
{
    public interface IOrderRepository
    {
        Order ReadSingleOrder(int OrderId);
        List<Order> ReadAllOrders();
        bool DeleteOrder(int OrderId);
        bool CreateOrder(Order order);
        bool EditOrder(Order order);
        
    }
}