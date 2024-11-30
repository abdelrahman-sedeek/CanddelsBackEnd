﻿using CanddelsBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CanddelsBackEnd.Config
{
    public class ProductVariantConfigration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder
            .HasIndex(p => p.Barcode)
            .IsUnique();
            builder.Property(pv => pv.Barcode)
                  .HasPrecision(18, 0);

            builder.Property(pv => pv.Price)
                .HasPrecision(18, 2);

            // Product and ProductVariant (One-to-Many)
            builder
                .HasOne(pv => pv.Product)
                .WithMany(p => p.productVariants)
                .HasForeignKey(pv => pv.ProductId);
    
            
        }
    }
}
