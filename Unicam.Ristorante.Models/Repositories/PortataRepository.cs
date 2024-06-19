using Unicam.Ristorante.Models.Entities;
using Unicam.Ristorante.Models.Context;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;

namespace Unicam.Ristorante.Models.Repositories
{
    public class PortataRepository : GenericRepository<Portata>
    {
        public PortataRepository(MyDbContext ctx) : base(ctx)
        {
        }

        public async Task<Tuple<List<Portata>,int>> OttieniTuttiAsync(int from, int num)
        {
            var query = _ctx.Portate.AsQueryable();

            int risultatiTotali = await query.CountAsync();

            var portate = await _ctx.Portate
                .OrderBy(p => p.Tipo)
                .ThenBy(p => p.Nome)
                .Skip(from)
                .Take(num)
                .ToListAsync();

            return new Tuple<List<Portata>, int>(portate, risultatiTotali);
        }
    }
}
