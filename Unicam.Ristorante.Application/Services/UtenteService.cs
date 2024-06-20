using Microsoft.AspNetCore.Identity;
using Unicam.Ristorante.Application.Abstractions.Services;
using Unicam.Ristorante.Models.Entities;
using Unicam.Ristorante.Models.Repositories;

namespace Unicam.Ristorante.Application.Services
{
    public class UtenteService : IUtenteService
    {
        private readonly UtenteRepository _utenteRepository;
        private readonly IPasswordHasher<Utente> _passwordHasher;

        public UtenteService(UtenteRepository utenteRepository, IPasswordHasher<Utente> passwordHasher) 
        {
            this._utenteRepository = utenteRepository;
            this._passwordHasher = passwordHasher;
        }

        public async Task AddUtenteAsync(Utente utente)
        {
            utente.Password = _passwordHasher.HashPassword(utente, utente.Password);
            await _utenteRepository.AggiungiAsync(utente);
            await _utenteRepository.SaveAsync();
        }
    }
}
