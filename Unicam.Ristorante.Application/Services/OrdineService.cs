using Unicam.Ristorante.Application.Abstractions.Services;
using Unicam.Ristorante.Models.Entities;
using Unicam.Ristorante.Models.Repositories;

namespace Unicam.Ristorante.Application.Services
{
    public class OrdineService : IOrdineService
    {
        private readonly OrdineRepository _ordineRepository;
        private readonly IndirizzoRepository _indirizzoRepository;

        public OrdineService(OrdineRepository ordineRepository, IndirizzoRepository indirizzoRepository) 
        { 
            _ordineRepository = ordineRepository;
            _indirizzoRepository = indirizzoRepository;
        }
        public async Task CreateOrdineAsync(int idUtente, Ordine ordine)
        {
            await ImpostaIndirizzo(ordine);
            ordine.IdUtente = idUtente;
            await _ordineRepository.AggiungiAsync(ordine);
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
    }
}
