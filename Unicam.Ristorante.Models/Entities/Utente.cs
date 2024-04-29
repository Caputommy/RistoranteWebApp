using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicam.Ristorante.Models.Entities
{
    public class Utente
    {
        public int      Id          { get; set; }
        public string?  Email       { get; set; } = null!;
        public string?  Nome        { get; set; } = null!;
        public string?  Cognome     { get; set; } = null!;
        public string?  Password    { get; set; } = null!;
        public Ruolo?   Ruolo       { get; set; } = null!;

    }
}
