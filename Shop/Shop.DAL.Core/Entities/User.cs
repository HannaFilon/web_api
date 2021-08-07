using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Shop.DAL.Core.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string AddressDelivery { get; set; }

        public ICollection<ProductRating> Ratings { get; set; }

    }
}