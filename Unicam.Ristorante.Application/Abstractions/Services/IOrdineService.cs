using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Application.Abstractions.Services
{
    public interface IOrdineService
    {
        Task<decimal> CreateOrdineAsync(int idUtente, Ordine oridne);
        Task<Tuple<List<Ordine>,int>> GetOrdiniAsync(int from, int num, 
            DateTime dataInizio, DateTime dataFine, int? idCliente);
    }
}
