using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
