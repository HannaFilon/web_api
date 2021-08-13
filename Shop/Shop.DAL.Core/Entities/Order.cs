using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Shop.DAL.Core.Entities
{
    [Index( nameof(UserId),IsUnique = false)]
    public class Order
    {
        public Guid OrderId { get; set; }
        public bool Comleted { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        
        public List<OrderProduct> OrderProducts { get; set; }
    }
}