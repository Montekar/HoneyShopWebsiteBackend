using System.Collections.Generic;
using HoneyShop.Core.Models;

namespace HoneyShop.Core.IServices
{
    public interface IOrderService
    {
        Order ReadSingleOrder(int OrderId);
        List<Order> ReadAllOrders();
        bool DeleteOrder(int OrderId);
        bool CreateOrder(Order order);
        bool EditOrder(Order order);


    }
}