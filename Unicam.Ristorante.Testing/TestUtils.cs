global using NUnit.Framework;
global using Microsoft.Extensions.Configuration;
global using Unicam.Ristorante.Models.Repositories;
global using Unicam.Ristorante.Models.Entities;
global using Unicam.Ristorante.Models.Configurations;

using Unicam.Ristorante.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Unicam.Ristorante.Testing
{
    internal class TestUtils
    {
        internal static IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json").Build();

        internal static MyDbContext ctx = new MyTestDbContext(config);

        internal static async Task ClearRepositoryAsync(string tableName)
        {
            await ctx.Database.ExecuteSqlRawAsync($"TRUNCATE TABLE [{tableName}]");
        }

        internal static void DetachAllEntities()
        {
            ctx.ChangeTracker.Clear();
        }
    }
}