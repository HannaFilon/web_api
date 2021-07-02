using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Shop.DAL.Core
{
    public class ShopContext: IdentityDbContext
    {     
           
            public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }    
    }   
}
