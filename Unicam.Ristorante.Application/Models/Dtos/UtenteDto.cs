using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Application.Models.Dtos
{
    public class UtenteDto
    {
        public UtenteDto() { }

        public UtenteDto(Utente utente) 
        { 
            Id = utente.Id;
            Email = utente.Email;
            Nome = utente.Nome;
            Cognome = utente.Cognome;
            Password = utente.Password;
            Ruolo = utente.Ruolo;
        }

        public int Id { get; set; }
        public string? Email { get; set; } = null!;
        public string? Nome { get; set; } = null!;
        public string? Cognome { get; set; } = null!;
        public string? Password { get; set; } = null!;
        public Ruolo? Ruolo { get; set; } = null!;
    }
}
