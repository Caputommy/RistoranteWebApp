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

        public async Task<List<Ordine>> OttieniTuttiAsync(DateTime dataInizio, DateTime dataFine, int? idCliente)
        {
            var query = _ctx.Ordini
                .Where(o => dataInizio <= o.Data && o.Data <= dataFine);

            if (idCliente != null)
            {
                query = query.Where(o => o.IdUtente == idCliente);
            }

            return await query
                .Include(o => o.IndirizzoConsegna)
                .Include(o => o.Voci)
                .ThenInclude(v => v.Portata)
                .ToListAsync();
        }
    }
}
