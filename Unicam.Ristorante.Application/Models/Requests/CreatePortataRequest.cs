using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Application.Models.Requests
{
    public class CreatePortataRequest
    {
        public string   Nome    { get; set; } = null!;
        public decimal? Prezzo  { get; set; } = null!;
        public int?     Tipo    { get; set; } = null!;

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
