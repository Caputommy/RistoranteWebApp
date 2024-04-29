namespace Unicam.Ristorante.Models.Entities
{
    public class Ordine
    {
        public int               Numero              { get; set; }
        public DateTime          Data                { get; set; } = DateTime.Now;
        public int               IdIndirizzo         { get; set; }
        public int               IdUtente            { get; set; }


        public Indirizzo         IndirizzoConsegna   { get; set; } = null!;
        public Utente            Utente              { get; set; } = null!;
        public List<VoceOrdine>  Voci                { get; set; } = null!;
    }
}
