using System;

namespace Shop.DAL.Core.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool Comleted { get; set; }

    }
}