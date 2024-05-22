using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unicam.Ristorante.Application.Abstractions.Services;
using Unicam.Ristorante.Application.Services;

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
        [Authorize(Roles = "Amministratore")]
        public async Task<IActionResult> CreateOrdineAsync()
        {
            //TODO: Fare CreateOrdine validator,
            //prendere id dell'utente,
            //richiamare il servizio,
            //poi rispondere con CreateOrdineResponse calcolando il prezzo nel service
        }

    }
}
