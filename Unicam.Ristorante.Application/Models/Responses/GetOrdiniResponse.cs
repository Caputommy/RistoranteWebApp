using Unicam.Ristorante.Application.Models.Dtos;
using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Application.Models.Responses
{
    public class GetOrdiniResponse
    {
        public List<OrdineDto> Ordini { get; set; }
        public PaginazioneResponse paginazione { get; set; } = null!;

        public GetOrdiniResponse(List<Ordine> ordini, int numeroPagina, int pagineTotali)
        {
            this.Ordini = ordini.Select(o => new OrdineDto(o)).ToList();
            this.paginazione = new PaginazioneResponse() { NumeroPagina = numeroPagina, PagineTotali = pagineTotali };
        }
    }
}
