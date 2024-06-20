using Unicam.Ristorante.Application.Models.Dtos;
using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Application.Models.Responses
{
    public class GetPortateResponse
    {
        public List<PortataDto>     Portate     { get; set; } = new List<PortataDto>();
        public PaginazioneResponse  Paginazione { get; set; } = null!;
        
        public GetPortateResponse(List<Portata> portate, int numeroPagina, int pagineTotali)
        {
            this.Portate = portate.Select(p => new PortataDto(p)).ToList();
            this.Paginazione = new PaginazioneResponse() { NumeroPagina = numeroPagina, PagineTotali = pagineTotali };
        }
    }
}
