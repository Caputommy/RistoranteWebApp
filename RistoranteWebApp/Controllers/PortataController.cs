using Microsoft.AspNetCore.Authorization;
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
    public class PortataController : ControllerBase
    {
        private readonly IPortataService _portataService;

        public PortataController(IPortataService portataService)
        {
            _portataService = portataService;
        }

        [HttpPost]
        [Route("new")]
        [Authorize] //TODO: Add authorization for admin
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
        public async Task<IActionResult> GetPortateAsync()
        {
            var portate = await _portataService.GetPortateAsync();
            var response = new GetPortateResponse(portate.Select(p => new PortataDto(p)).ToList());
            return Ok(ResponseFactory.WithSuccess(response));
        }
    }
}
