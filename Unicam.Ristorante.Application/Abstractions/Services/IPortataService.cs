using Microsoft.AspNetCore.Mvc;
using Unicam.Ristorante.Application.Models.Requests;
using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Application.Abstractions.Services
{
    public interface IPortataService
    {
        Task<IEnumerable<Portata>> GetPortateAsync();

        Task AddPortataAsync(Portata portata);
    }
}
