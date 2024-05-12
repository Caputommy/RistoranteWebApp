using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Authentication;
using Unicam.Ristorante.Application.Models.Requests;
using Unicam.Ristorante.Application.Options;
using Unicam.Ristorante.Application.Services;
using Unicam.Ristorante.Web.Controllers;

namespace Unicam.Ristorante.Testing.Controllers
{
    [TestFixture]
    internal class TokenControllerTest
    {
        private JwtAuthenticationOption jwtAuthenticationOption;
        private TokenController _tokenController;
        private UtenteController _utenteController;


        public TokenControllerTest() 
        {
            jwtAuthenticationOption = new JwtAuthenticationOption();
            TestUtils.config.GetSection("JwtAuthentication").Bind(jwtAuthenticationOption);

            _tokenController = new TokenController(
                new TokenService(new UtenteRepository(TestUtils.ctx), Options.Create(jwtAuthenticationOption)));

            _utenteController = new UtenteController(new UtenteService(new UtenteRepository(TestUtils.ctx)));
        }

        private static CreateTokenRequest[] requests =
        {
            new CreateTokenRequest()
            {
                Email = "marco.caputo@popmail.com",
                Password = "Password123"
            },
            new CreateTokenRequest()
            {
                Email = "luigi.bianchi@hotmail.it",
                Password = "Password123"
            },
            new CreateTokenRequest()
            {
                Email = "marco.caputo@popmail.com",
                Password = "WrongPassword123"
            }
        };

        private static CreateClienteRequest[] clienteRequests =
        {
            new CreateClienteRequest()
            {
                Email = "marco.caputo@popmail.com",
                Nome = "Marco",
                Cognome = "Caputo",
                Password = "Password123"
            }
        };

        [SetUp]
        public async Task SetUpAsync()
        {
            await _utenteController.CreateCliente(clienteRequests[0]);
        }

        [TearDown]
        public async Task TearDownAsync()
        {
            TestUtils.DetachAllEntities();
            await TestUtils.ClearRepositoryAsync(UtenteConfiguration.TableName);
        }

        [Test]
        public async Task ShouldCreateToken()
        {
            var result = await _tokenController.CreateToken(requests[0]);
            
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.Pass();
        }

        [Test]
        public void ShouldNotCreateTokenWithNoUser()
        {
            Assert.ThrowsAsync<InvalidCredentialException>(
                async () => await _tokenController.CreateToken(requests[1])
            );
        }

        [Test]
        public void ShouldNotCreateTokenWithWrongPassword()
        {
            Assert.ThrowsAsync<InvalidCredentialException>(
                async () => await _tokenController.CreateToken(requests[2])
            );
        }

    }
}
