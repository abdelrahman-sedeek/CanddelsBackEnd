using CanddelsBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CanddelsBackEnd.Infastrcuture.Config
{
    public class OrderItemsConfigration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(oi => oi.Total)
            .HasPrecision(18, 2);

            // Order and OrderItems (One-to-Many)
            builder.HasOne(oi => oi.Order)
                   .WithMany(o => o.OrderItems)
                   .HasForeignKey(oi => oi.OrderId);

            // One-to-Many relationship with ProductVariant
            builder.HasOne(oi => oi.productVariant)
                   .WithMany(pv => pv.OrderItems)
                   .HasForeignKey(oi => oi.productVariantId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);

            // One-to-Many relationship with CustomProduct
            builder.HasOne(oi => oi.customProduct)
                   .WithMany(cp => cp.OrderItems)
                   .HasForeignKey(oi => oi.customProductId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
