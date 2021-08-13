using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Shop.DAL.Core.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string AddressDelivery { get; set; }

        public List<ProductRating> Ratings { get; set; } = new List<ProductRating>();
        public List<Order> Orders { get; set; } = new List<Order>();

    }
}