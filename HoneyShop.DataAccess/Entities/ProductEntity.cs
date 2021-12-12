﻿using System.Collections.Generic;

namespace HoneyShop.DataAccess.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<OrderLineEntity> OrderLines { get; set; }
    }
}