using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Application.Models.Dtos
{
    public class IndirizzoDto
    {
        public string Via { get; set; } = null!;
        public string NumeroCivico { get; set; } = null!;
        public string Citta { get; set; } = null!;
        public string CAP { get; set; } = null!;

        public IndirizzoDto(Indirizzo indirizzo)
        {
            Via = indirizzo.Via;
            NumeroCivico = indirizzo.NumeroCivico;
            Citta = indirizzo.Citta;
            CAP = indirizzo.CAP;
        }

        public Indirizzo ToEntity()
        {
            return new Indirizzo
            {
                Via = Via,
                NumeroCivico = NumeroCivico,
                Citta = Citta,
                CAP = CAP
            };
        }
    }
}
