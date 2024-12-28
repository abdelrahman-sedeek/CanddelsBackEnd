﻿using CanddelsBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CanddelsBackEnd.Infastrcuture.Config
{
    public class paymentConfigration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(p => p.TotalPrice)
           .HasPrecision(18, 2);
            // Order and Payment (One-to-One)
          

        }
    }
}
