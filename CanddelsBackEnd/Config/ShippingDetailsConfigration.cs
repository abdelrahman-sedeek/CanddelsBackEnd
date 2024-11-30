using CanddelsBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CanddelsBackEnd.Config
{
    public class ShippingDetailsConfigration : IEntityTypeConfiguration<ShippingDetail>
    {
        public void Configure(EntityTypeBuilder<ShippingDetail> builder)
        {
            // Order and ShippingDetail (One-to-One)
            builder
                .HasOne(sd => sd.Order)
                .WithOne(o => o.ShippingDetail)
                .HasForeignKey<ShippingDetail>(sd => sd.OrderId);
        }
    }
}
