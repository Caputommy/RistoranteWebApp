using Unicam.Ristorante.Application.Models.Dtos;
using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Application.Models.Responses
{
    public class CreateOrdineResponse
    {
        public int? Numero { get; set; } = null!;
        public List<CreateVoceOrdineResponse> VociOrdine { get; set; } = null!;
        public decimal? Totale { get; set; } = null;

        public CreateOrdineResponse(Ordine ordine, decimal totale)
        {
            this.Numero = ordine.Numero;
            this.VociOrdine = ordine.Voci.Select(v => new CreateVoceOrdineResponse(v)).ToList();
            this.Totale = totale;
        }

        public class CreateVoceOrdineResponse
        {
            public PortataDto Portata { get; set; } = null!;
            public int? Quantita { get; set; } = null!;

            public CreateVoceOrdineResponse(VoceOrdine voceOrdine)
            {
                this.Portata = new PortataDto(voceOrdine.Portata);
                this.Quantita = voceOrdine.Quantita;
            }
        }
    }
}
