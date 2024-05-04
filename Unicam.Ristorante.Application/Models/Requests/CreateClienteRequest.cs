using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Application.Models.Requests
{
    public class CreateClienteRequest
    {
        public string Email { get; set; } = null!;
        public string Nome {  get; set; } = null!;
        public string Cognome { get; set; } = null!;
        public string Password { get; set; } = null!;

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
