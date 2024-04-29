using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Models.Configurations
{
    public class OrdineConfiguration : IEntityTypeConfiguration<Ordine>
    {
        public static string TableName => "Ordini";
        public void Configure(EntityTypeBuilder<Ordine> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(o => o.Numero);
            builder.Property(o => o.Data).HasColumnName("Data").IsRequired();

            builder.HasOne(o => o.Utente)
                .WithMany()
                .HasForeignKey(o => o.IdUtente);

            builder.HasOne(o => o.IndirizzoConsegna)
                .WithMany()
                .HasForeignKey(o => o.IdIndirizzo);
        }
    }
}
