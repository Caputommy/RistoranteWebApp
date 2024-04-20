using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Unicam.Ristorante.Models.Context;
using Unicam.Ristorante.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Unicam.Ristorante.Models.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddModelServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyDbContext>(conf =>
            {
                conf.UseSqlServer(configuration.GetConnectionString("MyDbContext"));
            });

            services.AddScoped<UtenteRepository>();

            return services;
        }
    }
}
