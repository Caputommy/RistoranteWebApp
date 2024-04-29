using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Models.Configurations
{
    public class IndirizzoConfiguration
    {
        public static string TableName => "Indirizzi";
        public void Configure(EntityTypeBuilder<Indirizzo> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Via).HasColumnName("Via").HasMaxLength(100).IsRequired();
            builder.Property(i => i.NumeroCivico).HasColumnName("NumeroCivico").HasMaxLength(10).IsRequired();
            builder.Property(i => i.Citta).HasColumnName("Citta").HasMaxLength(50).IsRequired();
            builder.Property(i => i.CAP).HasColumnName("CAP").HasMaxLength(5).IsRequired();
        }
    }
}
