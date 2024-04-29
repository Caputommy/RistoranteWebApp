using System.ComponentModel.DataAnnotations.Schema;

namespace Unicam.Ristorante.Models.Entities
{
    public class Indirizzo
    {
        public int    Id            { get; set; }
        public string Via           { get; set; } = null!;
        public string NumeroCivico  { get; set; } = null!;
        public string Citta         { get; set; } = null!;
        public string CAP           { get; set; } = null!;
    }
}
