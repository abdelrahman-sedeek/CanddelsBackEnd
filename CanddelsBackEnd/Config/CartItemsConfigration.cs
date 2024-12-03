﻿using CanddelsBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CanddelsBackEnd.Config
{
    public class CartItemsConfigration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            // Cart and CartItems (One-to-Many)
            builder.HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId);

            builder.HasOne(ci => ci.ProductVariant)
                .WithOne(ci => ci.CartItem)
                .HasForeignKey<CartItem>(ci => ci.ProductVariantId);
            
        }
    }
}
