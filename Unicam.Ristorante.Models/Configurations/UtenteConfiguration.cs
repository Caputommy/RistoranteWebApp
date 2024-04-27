using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Models.Configurations
{
    public class UtenteConfiguration : IEntityTypeConfiguration<Utente>
    {
        public static string TableName => "Utenti";
        public void Configure(EntityTypeBuilder<Utente> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Email).HasMaxLength(100).IsRequired();
            builder.Property(u => u.Nome).HasMaxLength(100);
            builder.Property(u => u.Cognome).HasMaxLength(100);
            builder.Property(u => u.Password).HasMaxLength(100).IsRequired();
            builder.Property(u => u.Ruolo).HasConversion<int>().IsRequired();
        }

    }
}
