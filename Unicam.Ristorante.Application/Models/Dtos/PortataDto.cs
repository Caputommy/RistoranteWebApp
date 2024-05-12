using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Application.Models.Dtos
{
    public class PortataDto
    {
        public int?       Id      { get; set; } = null!;
        public string?    Nome    { get; set; } = null!;
        public decimal?   Prezzo  { get; set; } = null!;
        public string?    Tipo    { get; set; } = null!;

        public PortataDto(Portata portata)
        {
            Id = portata.Id;
            Nome = portata.Nome;
            Prezzo = portata.Prezzo;
            Tipo = portata.Tipo.ToString();
        }
    }
}
