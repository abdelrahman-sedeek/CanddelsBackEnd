﻿using CanddelsBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CanddelsBackEnd.Config
{
    public class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Features)
                .HasColumnType("nvarchar(max)"); 
            
            builder.Property(p => p.Benefits)
                .HasColumnType("nvarchar(max)");


            builder.HasOne(p => p.Category)
                .WithMany(p => p.products)
                .HasForeignKey(p => p.CategoryId);



        }
    }
}
