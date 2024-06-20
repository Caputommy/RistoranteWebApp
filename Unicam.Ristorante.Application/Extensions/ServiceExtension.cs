using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Unicam.Ristorante.Application.Abstractions.Services;
using Unicam.Ristorante.Application.Services;
using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Application.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssembly(
                AppDomain.CurrentDomain.GetAssemblies().
                SingleOrDefault(assembly => assembly.GetName().Name == "Unicam.Ristorante.Application")
            );

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUtenteService, UtenteService>();
            services.AddScoped<IPortataService, PortataService>();
            services.AddScoped<IOrdineService, OrdineService>();
            services.AddScoped<IPasswordHasher<Utente>, PasswordHasher<Utente>>();

            return services;
        }
    }
}
