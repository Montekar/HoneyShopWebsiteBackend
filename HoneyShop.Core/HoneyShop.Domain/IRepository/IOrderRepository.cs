using System.Collections.Generic;
using HoneyShop.Core.Models;

namespace HoneyShop.Domain.IRepository
{
    public interface IOrderRepository
    {
        Order CreateOrder(Order order);
        Order ReadSingleOrder(int orderId);
        List<Order> ReadAllOrders();
        bool DeleteOrder(int orderId);
        bool EditOrder(Order order);

    }
}