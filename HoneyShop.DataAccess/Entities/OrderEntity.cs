using System;
using System.Collections.Generic;
using HoneyShop.Core.Models;

namespace HoneyShop.DataAccess.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public bool OrderCompleted { get; set; }
        public bool OrderPaid { get; set; }
    }
}