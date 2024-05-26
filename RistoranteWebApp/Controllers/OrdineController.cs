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
            int id = Int32.Parse(identity.Claims.Where(c => c.Type == "Id").FirstOrDefault().Value);

            Ordine ordine = request.ToEntity();
            decimal totale = await _ordineService.CreateOrdineAsync(id, ordine);
            var response = new CreateOrdineResponse(ordine, totale);

            return Ok(ResponseFactory.WithSuccess(response));
        }

    }
}
