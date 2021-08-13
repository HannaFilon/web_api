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

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .HasQueryFilter(p => !p.IsDeleted)
                .Property(p => p.TotalRating)
                .IsRequired(false);
            modelBuilder.Entity<ProductRating>().HasKey(sc => new { sc.ProductId, sc.UserId });
            modelBuilder.Entity<OrderProduct>().HasKey(sc => new { sc.OrderId, sc.ProductId });
        }
    }
}