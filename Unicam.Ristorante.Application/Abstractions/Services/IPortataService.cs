using Microsoft.AspNetCore.Mvc;
using Unicam.Ristorante.Application.Models.Requests;
using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Application.Abstractions.Services
{
    public interface IPortataService
    {
        Task<Tuple<List<Portata>,int>> GetPortateAsync(int from, int num);

        Task AddPortataAsync(Portata portata);
    }
}
