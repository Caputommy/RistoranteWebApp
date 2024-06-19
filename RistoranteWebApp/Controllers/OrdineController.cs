using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Unicam.Ristorante.Application.Abstractions.Services;
using Unicam.Ristorante.Application.Factory;
using Unicam.Ristorante.Application.Models.Requests;
using Unicam.Ristorante.Application.Models.Responses;
using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrdineController : ControllerBase
    {
        private readonly IOrdineService _ordineService;

        public OrdineController(IOrdineService ordineService)
        {
            _ordineService = ordineService;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> CreateOrdineAsync(CreateOrdineRequest request)
        {
            ClaimsIdentity identity = this.User.Identity as ClaimsIdentity;
            int id = Int32.Parse(identity.Claims.FirstOrDefault(c => c.Type == identity.NameClaimType).Value);

            Ordine ordine = request.ToEntity();
            decimal totale = await _ordineService.CreateOrdineAsync(id, ordine);
            var response = new CreateOrdineResponse(ordine, totale);

            return Ok(ResponseFactory.WithSuccess(response));
        }

        [HttpGet]
        [Route("list")]
        [Authorize]
        public async Task<IActionResult> GetOrdiniAsync([FromQuery] GetOrdiniRequest request)
        {
            ClaimsIdentity identity = this.User.Identity as ClaimsIdentity;

            if (this.User.IsInRole(Ruolo.Cliente.ToString()))
            {
                request.IdCliente = Int32.Parse(identity.Claims.FirstOrDefault(c => c.Type == identity.NameClaimType).Value);
            }

            var ordini = await _ordineService.GetOrdiniAsync(
                request.Paginazione.DimensionePagina * request.Paginazione.NumeroPagina, 
                request.Paginazione.DimensionePagina,
                request.DataInizio, request.DataFine, request.IdCliente);
            var pagineTotali = (int) Math.Ceiling(ordini.Item2 / (decimal) request.Paginazione.DimensionePagina);
            var response = new GetOrdiniResponse(ordini.Item1, request.Paginazione.NumeroPagina, pagineTotali);

            return Ok(ResponseFactory.WithSuccess(response));
        }

    }
}
