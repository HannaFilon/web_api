﻿using Microsoft.AspNetCore.Identity;
using System;

namespace Shop.DAL.Core.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string AddressDelivery { get; set; }

    }
}