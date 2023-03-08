using BusinessObject;
using eStoreClient2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eStoreClient2.Data
{
    public class ApplicationDbContext : IdentityDbContext<Member>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<OrderDetail>().HasKey(p => new { p.OrderId, p.ProductId });

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = ApplicationRole.Admim, NormalizedName = ApplicationRole.Admim.ToUpper()},
                new IdentityRole { Name = ApplicationRole.Customer, NormalizedName = ApplicationRole.Customer.ToUpper()}
                );
        }
    }
}
