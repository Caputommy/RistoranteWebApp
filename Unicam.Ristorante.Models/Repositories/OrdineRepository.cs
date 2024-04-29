using Unicam.Ristorante.Models.Entities;
using Unicam.Ristorante.Models.Context;

namespace Unicam.Ristorante.Models.Repositories
{
    public class OrdineRepository : GenericRepository<Ordine>
    {
        public OrdineRepository(MyDbContext ctx) : base(ctx)
        {
        }
    }
}
