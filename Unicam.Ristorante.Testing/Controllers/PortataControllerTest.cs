using Microsoft.AspNetCore.Mvc;
using Unicam.Ristorante.Application.Models.Requests;
using Unicam.Ristorante.Application.Models.Responses;
using Unicam.Ristorante.Application.Services;
using Unicam.Ristorante.Web.Controllers;

namespace Unicam.Ristorante.Testing.Controllers
{
    [TestFixture]
    internal class PortataControllerTest
    {
        private PortataController _controller =
            new PortataController(new PortataService(new PortataRepository(TestUtils.ctx)));

        private static CreatePortataRequest[] requests =
        {
            new CreatePortataRequest()
            {
                Nome = "Lasagne",
                Prezzo = 10.50m,
                Tipo = 1
            },
            new CreatePortataRequest()
            {
                Nome = "Bistecca",
                Prezzo = 10m,
                Tipo = 2
            },
            new CreatePortataRequest()
            {
                Nome = "Tiramisù",
                Prezzo = 4.50m,
                Tipo = 4
            }
        };

        [TearDown]
        public async Task TearDownAsync()
        {
            TestUtils.DetachAllEntities();
            await TestUtils.ClearRepositoryAsync(PortataConfiguration.TableName);
        }

        [Test]
        public async Task ShouldCreateCourse()
        {
            for (int i=0; i<requests.Length; i++)
            {
                var result = await _controller.CreatePortataAsync(requests[0]);
                Assert.That(result, Is.TypeOf<OkObjectResult>());

                var okResult = (OkObjectResult)result;
                Assert.That(okResult.Value, Is.TypeOf<BaseResponse<CreatePortataResponse>>());

                var baseResponseValue = (BaseResponse<CreatePortataResponse>)okResult.Value;
                Assert.True(baseResponseValue.Success);
                Assert.That(baseResponseValue.Result.Portata.Nome, Is.EqualTo(requests[0].Nome));
            }
        }

        [Test]
        public async Task ShouldGetCourses()
        {
            for (int i = 0; i < requests.Length; i++)
            {
                await _controller.CreatePortataAsync(requests[0]);
            }
            var result = await _controller.GetPortateAsync(new PaginazioneRequest(){DimensionePagina = 10, NumeroPagina = 0});
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.TypeOf<BaseResponse<GetPortateResponse>>());

            var baseResponseValue = (BaseResponse<GetPortateResponse>)okResult.Value;
            Assert.True(baseResponseValue.Success);
            Assert.That(baseResponseValue.Result.Portate.Count, Is.EqualTo(3));

            for (int i = 0; i < requests.Length; i++)
            {
                Assert.True(requests.Select(r => r.Nome).Contains(baseResponseValue.Result.Portate[i].Nome));
                Assert.True(requests.Select(r => r.Prezzo).Contains(baseResponseValue.Result.Portate[i].Prezzo));
                Assert.That(requests.Select(r => r.Tipo).Contains((int) Enum.Parse(typeof(TipoPortata), baseResponseValue.Result.Portate[i].Tipo)));
            }
        }

    }
}
