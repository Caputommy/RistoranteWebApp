using System.Security.Cryptography;
using Unicam.Ristorante.Application.Abstractions.Services;
using Unicam.Ristorante.Models.Entities;
using Unicam.Ristorante.Models.Repositories;

namespace Unicam.Ristorante.Application.Services
{
    public class OrdineService : IOrdineService
    {
        private readonly OrdineRepository _ordineRepository;
        private readonly IndirizzoRepository _indirizzoRepository;

        private static readonly decimal CoefficienteSconto = 0.9M;

        public OrdineService(OrdineRepository ordineRepository, IndirizzoRepository indirizzoRepository) 
        { 
            _ordineRepository = ordineRepository;
            _indirizzoRepository = indirizzoRepository;
        }
        public async Task<decimal> CreateOrdineAsync(int idUtente, Ordine ordine)
        {
            await ImpostaIndirizzo(ordine);
            ordine.IdUtente = idUtente;
            await _ordineRepository.AggiungiAsync(ordine);
            await _ordineRepository.SaveAsync();
            return CalcolaPrezzo(ordine);
        }

        //TODO: Testare se così funziona ma non credo

        private async Task ImpostaIndirizzo(Ordine ordine)
        {
            int idIndirizzo = await _indirizzoRepository.OttieniIdAsync(ordine.IndirizzoConsegna);
            if (idIndirizzo != 0)
            {
                ordine.IndirizzoConsegna = null!;
                ordine.IdIndirizzo = idIndirizzo;
            }
        }

        private decimal CalcolaPrezzo(Ordine ordine)
        {
            var listePrezziPerTipo = Enum
                .GetValues(typeof(TipoPortata))
                .Cast<TipoPortata>()
                .Select(t => OttieniListaPrezziOrdinata(ordine, t))
                .ToList();

            var numeroPastiCompleti = listePrezziPerTipo
                .Select(l => l.Count)
                .Min();

            return listePrezziPerTipo
                .SelectMany(l => l.Select((value, index) => index < numeroPastiCompleti ? value * CoefficienteSconto : value))
                .Sum();
        }

        private List<decimal> OttieniListaPrezziOrdinata(Ordine ordine, TipoPortata tipo)
        {
            return ordine.Voci
                .Where(v => v.Portata.Tipo == tipo)
                .OrderByDescending(v => v.Portata.Prezzo)
                .SelectMany(v => Enumerable.Repeat(v.Portata.Prezzo ?? 0, v.Quantita ?? 0))
                .ToList();
        }
    }
}
