using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        //Per scopi di test
        private IConfiguration configuration;

        //Per scopi di test
        public MyDbContext(IConfiguration configuration) : base()
        {
            this.configuration = configuration;
        }

        //Per scopi di test
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured && this.configuration != null)
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyDbContext"), 
                    options => options.EnableRetryOnFailure());
            }
        }
    }
}
