using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Models.Configurations
{
    public class PortataConfiguration : IEntityTypeConfiguration<Portata>
    {
        public static string TableName => "Portate";
        public void Configure(EntityTypeBuilder<Portata> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Prezzo).HasPrecision(8,2).IsRequired();
            builder.Property(p => p.Tipo).HasConversion<int>().IsRequired();
        }
    }
}
