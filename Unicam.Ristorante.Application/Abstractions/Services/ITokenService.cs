using Unicam.Ristorante.Application.Models.Requests;

namespace Unicam.Ristorante.Application.Abstractions.Services
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(CreateTokenRequest createTokenRequest);
    }
}
