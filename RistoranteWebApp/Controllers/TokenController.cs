using Microsoft.AspNetCore.Mvc;
using Unicam.Ristorante.Application.Abstractions.Services;
using Unicam.Ristorante.Application.Factory;
using Unicam.Ristorante.Application.Models.Requests;
using Unicam.Ristorante.Application.Models.Responses;

namespace Unicam.Ristorante.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateToken(CreateTokenRequest request)
        {
            var token = await _tokenService.CreateTokenAsync(request);
            var response = new CreateTokenResponse(token);
            return Ok(ResponseFactory.WithSuccess(response));
        }
    }
}
