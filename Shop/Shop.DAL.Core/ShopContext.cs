using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.DAL.Core.Entities;

namespace Shop.DAL.Core
{
    public class ShopContext: IdentityDbContext
    {     
            public DbSet<Product> Products { get; set; }
            public DbSet<Category> Categories { get; set; }

            public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }    
    }   
}
