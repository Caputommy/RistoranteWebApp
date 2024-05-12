using Unicam.Ristorante.Application.Models.Dtos;
using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Application.Models.Requests
{
    public class CreateOrdineRequest
    {
        public DateTime                         Data        { get; set; } = DateTime.Now;
        public IndirizzoDto                     Indirizzo   { get; set; } = null!;
        public List<CreateVoceOrdineRequest>    Voci        { get; set; } = new List<CreateVoceOrdineRequest>();

        public class CreateVoceOrdineRequest
        {
            public int IdPortata { get; set; }
            public int Quantita  { get; set; }
        }

        public Ordine ToEntity()
        {
            return new Ordine
            {
                Data = Data,
                IndirizzoConsegna = Indirizzo.ToEntity(),
                Voci = Voci.Select(v => new VoceOrdine
                {
                    IdPortata = v.IdPortata,
                    Quantita = v.Quantita
                }).ToList()
            };
        }
    }
}
