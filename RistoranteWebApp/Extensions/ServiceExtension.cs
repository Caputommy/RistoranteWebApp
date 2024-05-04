using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Unicam.Ristorante.Web.Results;

namespace Unicam.Ristorante.Web.Extensions
{
    public static class ServiceExtension
    {
        public static void AddWebServices (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentValidationAutoValidation();
            services.AddControllers()
                .ConfigureApiBehaviorOptions(opt =>
                {
                    opt.InvalidModelStateResponseFactory = context =>
                    {
                        return new BadRequestResultFactory(context);
                    };
                });
        }
    }
}
