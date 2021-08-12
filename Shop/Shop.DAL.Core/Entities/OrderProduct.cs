using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.DAL.Core.Entities
{
    public class OrderProduct
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        [Required] 
        public int Amount { get; set; }
    }
}