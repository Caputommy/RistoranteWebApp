using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Application.Models.Requests
{
    public class CreatePortataRequest
    {
        public string   Nome    { get; set; } = string.Empty;
        public decimal  Prezzo  { get; set; }
        public int      Tipo    { get; set; }

        public Portata ToEntity()
        {
            return new Portata
            {
                Nome = Nome,
                Prezzo = Prezzo,
                Tipo = (TipoPortata)Tipo
            };
        }
    }
}
