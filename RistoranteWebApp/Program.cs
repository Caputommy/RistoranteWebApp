using Unicam.Ristorante.Models.Extensions;
using Unicam.Ristorante.Application.Extensions;
using Unicam.Ristorante.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddModelServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddWebServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.AddWebMiddleware();

app.Run();

