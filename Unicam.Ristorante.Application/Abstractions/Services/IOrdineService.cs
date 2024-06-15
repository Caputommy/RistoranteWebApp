using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Application.Abstractions.Services
{
    public interface IOrdineService
    {
        Task<decimal> CreateOrdineAsync(int idUtente, Ordine oridne);
        Task<List<Ordine>> GetOrdiniAsync(DateTime dataInizio, DateTime dataFine, int? idCliente);
    }
}
