﻿using CanddelsBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CanddelsBackEnd.Contexts
{
    public class CandelContext : DbContext
    {
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Scent> Scents { get; set; }
        public DbSet<CustomProduct> customProducts { get; set; }
        public DbSet<CustomProductScent> CustomProductScents { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ShippingDetail> ShippingDetails { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Store> Stores { get; set; }
        public CandelContext(DbContextOptions<CandelContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<CustomProduct>()
            .HasKey(cp => cp.Id);
        }
    }
}
