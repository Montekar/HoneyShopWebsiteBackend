using System.Collections.Generic;
using HoneyShop.Core.Models;

namespace HoneyShop.Core.IServices
{
    public interface IOrderService
    {
        Order ReadSingleOrder(int orderId);
        List<Order> ReadAllOrders();
        bool DeleteOrder(int orderId);
        Order CreateOrder(Order order);
        bool EditOrder(Order order);
    }
}