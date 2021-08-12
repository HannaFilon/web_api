using System;
using System.Collections.Generic;

namespace Shop.DAL.Core.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public bool Comleted { get; set; }
        public List<Product> Products { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}