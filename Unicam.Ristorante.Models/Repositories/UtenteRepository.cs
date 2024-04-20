using Unicam.Ristorante.Models.Repositories;
using Unicam.Ristorante.Models.Entities;
using Unicam.Ristorante.Models.Context;

namespace Unicam.Ristorante.Models.Repositories
{
    internal class UtenteRepository : GenericRepository<Utente>
    {
        public UtenteRepository(MyDbContext ctx) : base(ctx)
        {
        }

    }
}
