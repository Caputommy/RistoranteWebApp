using Unicam.Ristorante.Models.Repositories;
using Unicam.Ristorante.Models.Entities;
using Unicam.Ristorante.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Unicam.Ristorante.Models.Repositories
{
    public class UtenteRepository : GenericRepository<Utente>
    {
        public UtenteRepository(MyDbContext ctx) : base(ctx)
        {
        }

        public async Task<Utente> GetUtenteByEmailAsync(string email)
        {
            return await _ctx.Utenti.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

    }
}
