using Unicam.Ristorante.Application.Models.Dtos;

namespace Unicam.Ristorante.Application.Models.Responses
{
    public class GetPortateResponse
    {
        public List<PortataDto> portate { get; set; } = new List<PortataDto>();
        
        public GetPortateResponse(List<PortataDto> portate)
        {
            this.portate = portate;
        }
    }
}
