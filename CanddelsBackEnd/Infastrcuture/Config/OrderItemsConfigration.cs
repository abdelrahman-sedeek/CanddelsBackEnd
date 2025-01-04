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


            builder.HasOne(oi => oi.productVariant)
             .WithOne(pv => pv.OrderItem)
             .HasForeignKey<OrderItem>(oi => oi.productVariantId);



        }
    }
}
