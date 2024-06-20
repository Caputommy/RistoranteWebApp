using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unicam.Ristorante.Application.Models.Requests;
using Unicam.Ristorante.Application.Models.Responses;
using Unicam.Ristorante.Application.Services;
using Unicam.Ristorante.Web.Controllers;

namespace Unicam.Ristorante.Testing.Controllers
{
    [TestFixture]
    public class UtenteControllerTest
    {
        private UtenteController _controller = 
            new UtenteController(new UtenteService(new UtenteRepository(TestUtils.ctx), new PasswordHasher<Utente>()));

        private static CreateClienteRequest[] requests =
        {
            new CreateClienteRequest()
            {
                Email = "marco.caputo@popmail.com",
                Nome = "Marco",
                Cognome = "Caputo",
                Password = "Password123"
            },
            new CreateClienteRequest()
            {
                Email = "mario.rossi@popmail.com",
                Nome = "Mario",
                Cognome = "Rossi",
                Password = null!
            },
            new CreateClienteRequest()
            {
                Email = "luigi.bianchi@hotmail.it",
                Password = "Password123"
            }
        };

        [TearDown]
        public async Task TearDownAsync()
        {
            TestUtils.DetachAllEntities();
            await TestUtils.ClearRepositoryAsync(UtenteConfiguration.TableName);
        }

        [Test]
        public async Task ShouldAddUser()
        {
            var result = await _controller.CreateClienteAsync(requests[0]);

            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.TypeOf<BaseResponse<CreateClienteResponse>>());

            var baseResponseValue = (BaseResponse<CreateClienteResponse>)okResult.Value;
            Assert.True(baseResponseValue.Success);
            Assert.That(baseResponseValue.Result.Utente.Email, Is.EqualTo(requests[0].Email));
        }

        [Test]
        public void ShouldNotAddUserWithMissingPassword()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _controller.CreateClienteAsync(requests[1]));
        }

        [Test]
        public async Task ShouldAddUserWithoutNameAndSurname()
        {
            var result = await _controller.CreateClienteAsync(requests[2]);

            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.TypeOf<BaseResponse<CreateClienteResponse>>());

            var baseResponseValue = (BaseResponse<CreateClienteResponse>)okResult.Value;
            Assert.True(baseResponseValue.Success);
            Assert.That(baseResponseValue.Result.Utente.Email, Is.EqualTo(requests[2].Email));
            Assert.That(baseResponseValue.Result.Utente.Nome, Is.Empty);
        }
    }
}
