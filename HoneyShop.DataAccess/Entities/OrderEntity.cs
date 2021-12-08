using System;
using System.Collections.Generic;
using HoneyShop.Core.Models;

namespace HoneyShop.DataAccess.Entities
{
    public class OrderEntity
    {
        public bool OrderCompleted { get; set; }
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public List<Product> ProductList { get; set; }
    }
}