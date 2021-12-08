using System;
using System.Collections.Generic;

namespace HoneyShop.Core.Models
{
    public class Order
    {
        public bool OrderCompleted { get; set; }
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public List<Product> ProductList { get; set; }

    }
}