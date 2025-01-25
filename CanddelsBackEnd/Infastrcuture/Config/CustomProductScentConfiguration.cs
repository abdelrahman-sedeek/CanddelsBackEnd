using CanddelsBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CanddelsBackEnd.Infastrcuture.Config
{
    public class CustomProductScentConfiguration : IEntityTypeConfiguration<CustomProductScent>
    {
        public void Configure(EntityTypeBuilder<CustomProductScent> builder)
        {
            builder.HasKey(builder => new { builder.CustomProductId, builder.ScentId });

            builder
                .HasOne(cps=>cps.CustomProduct)
                .WithMany(cp => cp.CustomProductScents)
                .HasForeignKey(cps=>cps.CustomProductId);

            builder
                .HasOne(cps => cps.Scent)
                .WithMany(s => s.CustomProductScents)
                .HasForeignKey(cps => cps.ScentId);
        }
    }
}
