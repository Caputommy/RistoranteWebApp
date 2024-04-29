using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Models.Context
{
    public class MyDbContext : DbContext
    {

        public MyDbContext() : base() 
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> config) : base(config) 
        {
        }

        public DbSet<Utente> Utenti { get; set; }
        public DbSet<Portata> Portate { get; set; }
        public DbSet<Ordine> Ordini { get; set; }
        public DbSet<VoceOrdine> VociOrdine { get; set; }
        public DbSet<Indirizzo> Indirizzi { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
