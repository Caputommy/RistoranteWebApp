using Unicam.Ristorante.Models.Entities;
using Unicam.Ristorante.Models.Context;

namespace Unicam.Ristorante.Models.Repositories
{
    public class PortataRepository : GenericRepository<Portata>
    {
        public PortataRepository(MyDbContext ctx) : base(ctx)
        {
        }
    }
}
