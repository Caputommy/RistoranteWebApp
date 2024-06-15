using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Application.Models.Dtos
{
    public class OrdineDto
    {
        public int?                Numero              { get; set; } = null!;
        public DateTime?           Data                { get; set; } = null!;
        public IndirizzoDto        IndirizzoConsegna   { get; set; } = null!;
        public List<VoceOrdineDto> Voci                { get; set; } = Enumerable.Empty<VoceOrdineDto>().ToList();

        
        public class VoceOrdineDto
        {
            public PortataDto Portata { get; set; } = null!;
            public int? Quantita { get; set; } = null!;

            public VoceOrdineDto(VoceOrdine voceOrdine)
            {
                this.Portata = new PortataDto(voceOrdine.Portata);
                this.Quantita = voceOrdine.Quantita;
            }
        }

        public OrdineDto(Ordine ordine)
        {
            Numero = ordine.Numero;
            Data = ordine.Data;
            IndirizzoConsegna = new IndirizzoDto(ordine.IndirizzoConsegna);
            Voci = ordine.Voci.Select(v => new VoceOrdineDto(v)).ToList();
        }
    }
}
