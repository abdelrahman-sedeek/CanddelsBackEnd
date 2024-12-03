using CanddelsBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CanddelsBackEnd.Config
{
    public class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder
                .Property(o => o.SubTotal)
                .HasPrecision(18, 2);


            builder.HasOne(o => o.ShippingDetail)
                .WithOne(o => o.Order)
                .HasForeignKey<ShippingDetail>(o => o.OrderId);

            builder.HasOne(o => o.Payment)
                .WithOne(o => o.Order)
                .HasForeignKey<Payment>(o => o.OrderId);
           
        }
    }
}
