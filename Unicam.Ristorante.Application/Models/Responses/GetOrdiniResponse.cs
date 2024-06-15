using Unicam.Ristorante.Application.Models.Dtos;
using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Application.Models.Responses
{
    public class GetOrdiniResponse
    {
        public List<OrdineDto> ordini { get; set; }

        public GetOrdiniResponse(List<Ordine> ordini)
        {
            this.ordini = ordini.Select(o => new OrdineDto(o)).ToList();
        }
    }
}
