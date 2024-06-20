using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Application.Models.Dtos
{
    public class UtenteDto
    {
        public int      Id          { get; set; }
        public string   Email       { get; set; } = string.Empty;
        public string?  Nome        { get; set; } = string.Empty;
        public string?  Cognome     { get; set; } = string.Empty;
        public string   Password    { get; set; } = string.Empty;
        public Ruolo    Ruolo       { get; set; }

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
    }
}
