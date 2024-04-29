using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Models.Configurations
{
    public class VoceOrdineConfiguration : IEntityTypeConfiguration<VoceOrdine>
    {
        public static string TableName => "VociOrdine";
        public void Configure(EntityTypeBuilder<VoceOrdine> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(v => new {v.NumeroOrdine, v.IdPortata});
            builder.Property(v => v.Quantita).IsRequired();

            builder.HasOne(v => v.Ordine)
                .WithMany(o => o.Voci)
                .HasForeignKey(v => v.NumeroOrdine);

            builder.HasOne(v => v.Portata)
                .WithMany()
                .HasForeignKey(v => v.IdPortata);
        }
    }
}
