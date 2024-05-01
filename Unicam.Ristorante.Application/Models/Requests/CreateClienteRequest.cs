using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Application.Models.Requests
{
    public class CreateClienteRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Nome {  get; set; } = string.Empty;
        public string Cognome { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public Utente ToEntity()
        {
            return new Utente
            {
                Email = Email,
                Nome = Nome,
                Cognome = Cognome,
                Password = Password,
                Ruolo = Ruolo.Cliente
            };
        }
    }
}
