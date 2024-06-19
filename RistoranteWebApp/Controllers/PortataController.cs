using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unicam.Ristorante.Application.Abstractions.Services;
using Unicam.Ristorante.Application.Factory;
using Unicam.Ristorante.Application.Models.Dtos;
using Unicam.Ristorante.Application.Models.Requests;
using Unicam.Ristorante.Application.Models.Responses;
using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PortataController : ControllerBase
    {
        private readonly IPortataService _portataService;

        public PortataController(IPortataService portataService)
        {
            _portataService = portataService;
        }

        [HttpPost]
        [Route("new")]
        [Authorize(Roles = "Amministratore")]
        public async Task<IActionResult> CreatePortataAsync(CreatePortataRequest request)
        {
            var portata = request.ToEntity();
            await _portataService.AddPortataAsync(portata);

            var response = new CreatePortataResponse();
            response.Portata = new PortataDto(portata);
            return Ok(ResponseFactory.WithSuccess(response));
        }

        [HttpGet]
        [Route("list")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPortateAsync([FromQuery] PaginazioneRequest paginazione)
        {
            var portate = await _portataService.GetPortateAsync(
                paginazione.DimensionePagina * paginazione.NumeroPagina,
                paginazione.DimensionePagina);
            var pagioneTotali = (int)Math.Ceiling(portate.Item2 / (decimal)paginazione.DimensionePagina);
            var response = new GetPortateResponse(portate.Item1, paginazione.NumeroPagina, pagioneTotali);
            return Ok(ResponseFactory.WithSuccess(response));
        }
    }
}
