using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicam.Ristorante.Models.Entities
{
    public class Portata
    {
        public int          Id      { get; set; }
        public string?      Nome    { get; set; } = null!;
        public decimal?     Prezzo  { get; set; } = null!;
        public TipoPortata? Tipo    { get; set; } = null!;
    }
}
