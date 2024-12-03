﻿using CanddelsBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CanddelsBackEnd.Config
{
    public class DiscountConfigration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.Property(d => d.DiscountPercentage)
            .HasColumnType("decimal(5, 2)"); 
            
            builder.Property(d => d.PriceAfterDiscount)
            .HasColumnType("decimal(5, 2)");

        }
    }
}
