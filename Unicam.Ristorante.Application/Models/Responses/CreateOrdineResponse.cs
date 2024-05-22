using Unicam.Ristorante.Application.Models.Dtos;

namespace Unicam.Ristorante.Application.Models.Responses
{
    public class CreateOrdineResponse
    {
        public string Id { get; set; } = string.Empty;
        public List<CreateVoceOrdineResponse> VociOrdine { get; set; } = null!;
        public decimal? Totale { get; set; } = null;

        public class CreateVoceOrdineResponse
        {
            public PortataDto Portata { get; set; } = null!;
            public int? Quantita { get; set; } = null!;
        }
    }
}
