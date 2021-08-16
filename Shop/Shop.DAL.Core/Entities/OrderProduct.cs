using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Shop.DAL.Core.Entities
{
    [Index(nameof(ProductId), IsUnique = false)]
    public class OrderProduct
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        private int _amount;

        [Required]
        public int Amount
        {
            get => _amount;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Amount can't be negative.");
                }

                _amount = value;
            }
        }
    }
}