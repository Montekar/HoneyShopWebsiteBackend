using System.Collections.Generic;
using HoneyShop.Core.Models;

namespace HoneyShop.Domain.IRepository
{
    public interface IOrderRepository
    {
        Order CreateOrder(Order order);

    }
}