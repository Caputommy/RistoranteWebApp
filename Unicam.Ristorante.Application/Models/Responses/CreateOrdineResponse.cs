using Unicam.Ristorante.Application.Models.Dtos;
using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Application.Models.Responses
{
    public class CreateOrdineResponse
    {
        public OrdineDto? Ordine { get; set; } = null!;
        public decimal? Totale { get; set; } = null;

        public CreateOrdineResponse(Ordine ordine, decimal totale)
        {
            this.Ordine = new OrdineDto(ordine);
            this.Totale = totale;
        }
    }
}
