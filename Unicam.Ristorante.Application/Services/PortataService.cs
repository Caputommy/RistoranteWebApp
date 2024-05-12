using Unicam.Ristorante.Application.Abstractions.Services;
using Unicam.Ristorante.Models.Entities;
using Unicam.Ristorante.Models.Repositories;

namespace Unicam.Ristorante.Application.Services
{
    public class PortataService : IPortataService
    {
        private readonly PortataRepository _portataRepository;

        public PortataService(PortataRepository portataRepository)
        {
            _portataRepository = portataRepository;
        }

        public async Task AddPortataAsync(Portata portata)
        {
            await _portataRepository.AggiungiAsync(portata);
            await _portataRepository.SaveAsync();
        }

        public async Task<IEnumerable<Portata>> GetPortateAsync()
        {
            return await _portataRepository.OttieniTuttiAsync();
        }
    }
}
