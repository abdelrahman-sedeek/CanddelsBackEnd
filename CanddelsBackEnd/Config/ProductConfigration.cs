using CanddelsBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CanddelsBackEnd.Config
{
    public class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.BasePrice)
                   .HasPrecision(18, 2);

            builder.Property(p => p.DefaultWeight)
                   .HasPrecision(18, 2);
        }
    }
}
