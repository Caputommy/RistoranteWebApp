using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Application.Models.Dtos
{
    public class PortataDto
    {
        public int        Id      { get; set; }
        public string     Nome    { get; set; } = string.Empty;
        public decimal    Prezzo  { get; set; }
        public string     Tipo    { get; set; } = string.Empty;

        public PortataDto(Portata portata)
        {
            Id = portata.Id;
            Nome = portata.Nome;
            Prezzo = portata.Prezzo;
            Tipo = portata.Tipo.ToString();
        }
    }
}
