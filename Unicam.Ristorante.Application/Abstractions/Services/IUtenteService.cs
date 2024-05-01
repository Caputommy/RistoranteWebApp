using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Application.Abstractions.Services
{
    public interface IUtenteService
    {
        Task AddUtenteAsync(Utente utente);
    }
}
