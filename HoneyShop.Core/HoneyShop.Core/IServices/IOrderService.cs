using System.Collections.Generic;
using HoneyShop.Core.Models;

namespace HoneyShop.Core.IServices
{
    public interface IOrderService
    {
        Order CreateOrder(Order order);
    }
}