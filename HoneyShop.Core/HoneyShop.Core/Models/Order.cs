using System;
using System.Collections.Generic;

namespace HoneyShop.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public bool OrderCompleted { get; set; }
        public bool OrderPaid { get; set; }
    }
}