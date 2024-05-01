using Microsoft.AspNetCore.Mvc;
using Unicam.Ristorante.Application.Abstractions.Services;
using Unicam.Ristorante.Application.Factory;
using Unicam.Ristorante.Application.Models.Dtos;
using Unicam.Ristorante.Application.Models.Requests;
using Unicam.Ristorante.Application.Models.Responses;

namespace Unicam.Ristorante.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UtenteController : ControllerBase
    {
        private readonly IUtenteService _utenteService;
        public UtenteController(IUtenteService utenteService)
        {
            _utenteService = utenteService;
        }

        [HttpPost]
        [Route("new")]
        public async Task<IActionResult> CreateCliente(CreateClienteRequest request)
        {
            /*var validator = new CreateUtenteRequestValidator();
            validator.Validate(request);*/
            var utente = request.ToEntity();
            await _utenteService.AddUtenteAsync(utente);

            var response = new CreateClienteResponse();
            response.Utente = new UtenteDto(utente);
            return Ok(ResponseFactory.WithSuccess(response));
        }
    }
}
