using Unicam.Ristorante.Application.Abstractions.Services;
using Unicam.Ristorante.Models.Entities;
using Unicam.Ristorante.Models.Repositories;

namespace Unicam.Ristorante.Application.Services
{
    public class UtenteService : IUtenteService
    {
        private readonly UtenteRepository _utenteRepository;

        public UtenteService(UtenteRepository utenteRepository) 
        {
            this._utenteRepository = utenteRepository;
        }

        public async Task AddUtenteAsync(Utente utente)
        {
            await _utenteRepository.AggiungiAsync(utente);
            await _utenteRepository.SaveAsync();
        }
    }
}
