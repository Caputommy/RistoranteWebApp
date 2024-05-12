using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Unicam.Ristorante.Models.Context;
using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Models.Repositories
{
    public class IndirizzoRepository : GenericRepository<Indirizzo>
    {
        public IndirizzoRepository(MyDbContext ctx) : base(ctx)
        {
        }

        /**
         * <summary>
         * Ottiene l'ID di un indirizzo se esiste già nel database.
         * </summary>
         * <param name="indirizzo">Indirizzo da cercare.</param>
         * <returns>ID dell'indirizzo se esiste, 0 altrimenti.</returns>
         **/
        public async Task<int> OttieniIdAsync(Indirizzo indirizzo)
        {
            var indirizzoDb = await _ctx.Indirizzi
                .Where(i =>
                    i.Via == indirizzo.Via &&
                    i.NumeroCivico == indirizzo.NumeroCivico &&
                    i.Citta == indirizzo.Citta &&
                    i.CAP == indirizzo.CAP)
                .FirstOrDefaultAsync();

            return indirizzoDb == null ? 0 : indirizzoDb.Id;
        }
    }
}
