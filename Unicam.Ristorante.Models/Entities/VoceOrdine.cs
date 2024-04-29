﻿namespace Unicam.Ristorante.Models.Entities
{
    public class VoceOrdine
    {
        public int      NumeroOrdine  { get; set; }
        public int      IdPortata     { get; set; }
        public int?     Quantita      { get; set; } = null!;

        public Ordine   Ordine        { get; set; } = null!;
        public Portata  Portata       { get; set; } = null!;
    }
}
