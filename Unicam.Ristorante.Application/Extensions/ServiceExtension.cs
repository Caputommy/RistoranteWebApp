using Unicam.Ristorante.Application.Abstractions.Services;
using Unicam.Ristorante.Application.Services;

namespace Unicam.Ristorante.Application.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            /*
            services.AddValidatorsFromAssembly(
                AppDomain.CurrentDomain.GetAssemblies().
                SingleOrDefault(assembly => assembly.GetName().Name == "Unicam.Ristorante.Application")
            );
            */

            services.AddScoped<IUtenteService, UtenteService>();
            return services;
        }
    }
}
