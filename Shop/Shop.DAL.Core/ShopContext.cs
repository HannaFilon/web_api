using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.DAL.Core.Entities;
using System;

namespace Shop.DAL.Core
{
    public class ShopContext : IdentityDbContext<User, Role, Guid>
    {
        public ShopContext(DbContextOptions<ShopContext> options) 
            : base(options) { }

        public DbSet<Product> Products{ get; set; }
    }
}
