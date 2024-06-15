using Unicam.Ristorante.Models.Entities;
using Unicam.Ristorante.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Unicam.Ristorante.Models.Repositories
{
    public class OrdineRepository : GenericRepository<Ordine>
    {
        public OrdineRepository(MyDbContext ctx) : base(ctx)
        {
        }

        public async Task<Tuple<List<Ordine>,int>> OttieniTuttiAsync(int from, int num, DateTime dataInizio, DateTime dataFine, int? idCliente)
        {
            var query = _ctx.Ordini
                .Where(o => dataInizio <= o.Data && o.Data <= dataFine);

            if (idCliente != null)
            {
                query = query.Where(o => o.IdUtente == idCliente);
            }

            int risultatiTotali = await query.CountAsync();

            var ordini = await query
                .Include(o => o.IndirizzoConsegna)
                .Include(o => o.Voci)
                .ThenInclude(v => v.Portata)
                .OrderBy(o => o.Data)
                .Skip(from)
                .Take(num)
                .ToListAsync();

            return new Tuple<List<Ordine>, int>(ordini, risultatiTotali);
        }
    }
}
