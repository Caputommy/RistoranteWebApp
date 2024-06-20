namespace Unicam.Ristorante.Models.Entities
{
    public class Utente
    {
        public int      Id          { get; set; }
        public string   Email       { get; set; } = null!;
        public string   Nome        { get; set; } = string.Empty;
        public string   Cognome     { get; set; } = string.Empty;
        public string   Password    { get; set; } = null!;
        public Ruolo    Ruolo       { get; set; }

    }
}
