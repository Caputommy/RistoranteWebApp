namespace Unicam.Ristorante.Models.Entities
{
    public class Portata
    {
        public int          Id      { get; set; }
        public string       Nome    { get; set; } = string.Empty;
        public decimal      Prezzo  { get; set; } = 0M;
        public TipoPortata  Tipo    { get; set; }
    }
}
