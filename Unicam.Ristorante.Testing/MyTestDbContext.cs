using Microsoft.EntityFrameworkCore;
using Unicam.Ristorante.Models.Context;

namespace Unicam.Ristorante.Testing
{
    internal class MyTestDbContext : MyDbContext
    {
        private IConfiguration configuration;

        public MyTestDbContext(IConfiguration configuration) : base()
        {
            this.configuration = configuration;
        }

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
