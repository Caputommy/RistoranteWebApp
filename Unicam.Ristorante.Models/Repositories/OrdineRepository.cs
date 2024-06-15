using Unicam.Ristorante.Models.Entities;
using Unicam.Ristorante.Models.Context;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;

namespace Unicam.Ristorante.Models.Repositories
{
    public class OrdineRepository : GenericRepository<Ordine>
    {
        public OrdineRepository(MyDbContext ctx) : base(ctx)
        {
        }

        public async Task<Ordine> OttieniConEagerLoadingAsync(int numero)
        {
            return await _ctx.Ordini.Where(o => o.Numero == numero)
                .Include(o => o.IndirizzoConsegna)
                .Include(o => o.Voci)
                .ThenInclude(v => v.Portata)
                .FirstAsync();
        }
    }
}
