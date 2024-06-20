using Unicam.Ristorante.Application.Models.Requests;
using Unicam.Ristorante.Application.Models.Responses;
using Unicam.Ristorante.Application.Services;
using Unicam.Ristorante.Web.Controllers;
using Unicam.Ristorante.Application.Models.Dtos;
using static Unicam.Ristorante.Application.Models.Requests.CreateOrdineRequest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

namespace Unicam.Ristorante.Testing.Controllers
{
    [TestFixture]
    internal class OrdineControllerTest {

        private static IndirizzoRepository _indirizzoRepository = new IndirizzoRepository(TestUtils.ctx);
        private static PortataRepository _portataRepository = new PortataRepository(TestUtils.ctx);

        private UtenteController utenteController = 
            new UtenteController(new UtenteService(new UtenteRepository(TestUtils.ctx), new PasswordHasher<Utente>()));

        private OrdineController _controller =
            new OrdineController(new OrdineService(new OrdineRepository(TestUtils.ctx), _indirizzoRepository, _portataRepository));

        private PortataController _portataController =
            new PortataController(new PortataService(new PortataRepository(TestUtils.ctx)));

        private static Indirizzo[] indirizzi = [
            new Indirizzo() {
                Via = "Via Roma",
                NumeroCivico = "1",
                Citta = "Ancona",
                CAP = "60125"
            },

            new Indirizzo() {
                Via = "Via Milano",
                NumeroCivico = "2",
                Citta = "Ancona",
                CAP = "60125"
            }
        ];

        private static Portata[] portate = {
            new Portata()
            {
                Nome = "Lasagne",
                Prezzo = 8.5M,
                Tipo = TipoPortata.Primo
            },
            new Portata()
            {
                Nome = "Tagliatelle al Ragù",
                Prezzo = 7.5M,
                Tipo = TipoPortata.Primo
            },
            new Portata()
            {
                Nome = "Arista",
                Prezzo = 10M,
                Tipo = TipoPortata.Secondo
            },
            new Portata()
            {
                Nome = "Salmone",
                Prezzo = 12M,
                Tipo = TipoPortata.Secondo
            },
            new Portata() {
                Nome = "Insalata",
                Prezzo = 3M,
                Tipo = TipoPortata.Contorno
            },
            new Portata()
            {
                Nome = "Tiramisù",
                Prezzo = 4.50M,
                Tipo = TipoPortata.Dolce
            }
        };

        private static UtenteDto[] utenti = {
            new UtenteDto()
            {
                Id = 1,
                Email = "mario.rossi@example.com",
                Nome = "Mario",
                Cognome = "Rossi",
                Password = "Password123",
                Ruolo = Ruolo.Amministratore
            },
            new UtenteDto()
            {
                Id = 2,
                Email = "luigi.bianchi@example.com",
                Nome = "Luigi",
                Cognome = "Bianchi",
                Password = "Password456",
                Ruolo = Ruolo.Cliente
            }
        };

        [SetUp]
        public async Task CreateCoursesAndMockUser()
        {
            foreach (Portata portata in portate) 
            {
                var response = await _portataController.CreatePortataAsync(new CreatePortataRequest()
                {
                    Nome = portata.Nome,
                    Prezzo = portata.Prezzo,
                    Tipo = (int) portata.Tipo
                });
                portata.Id = (int)((BaseResponse<CreatePortataResponse>)((OkObjectResult)response).Value).Result.Portata.Id;
            }

            var request = new CreateClienteRequest()
            {
                Email = "marco.caputo@popmail.com",
                Nome = "Marco",
                Cognome = "Caputo",
                Password = "Password123"
            };

            var result = await utenteController.CreateClienteAsync(request);
            var utente = ((BaseResponse<CreateClienteResponse>)((OkObjectResult)result).Value).Result.Utente;

            MockControllerWithUser(utente);
        }

        private void MockControllerWithUser(UtenteDto utente) 
        {
            var claims = new List<Claim>
            {
                new Claim("Id", utente.Id.ToString()),
                new Claim("Email", utente.Email),
                new Claim("Nome", utente.Nome),
                new Claim("Cognome", utente.Cognome),
                new Claim("Ruolo", utente.Ruolo.ToString())
            };
            var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme, nameType: "Id", roleType: "Ruolo");
            var principal = new ClaimsPrincipal(identity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };
        }

        [TearDown]
        public async Task TearDownAsync()
        {
            TestUtils.DetachAllEntities();
            foreach (Portata portata in portate)
            {
                portata.Id = 0;
            }
            await TestUtils.ClearRepositoryAsync(VoceOrdineConfiguration.TableName);
            await TestUtils.ClearRepositoryAsync(OrdineConfiguration.TableName);
            await TestUtils.ClearRepositoryAsync(PortataConfiguration.TableName);
            await TestUtils.ClearRepositoryAsync(IndirizzoConfiguration.TableName);
            await TestUtils.ClearRepositoryAsync(UtenteConfiguration.TableName);
        }

        [Test]
        public async Task ShouldCreateOrder1()
        {
            CreateOrdineRequest request = new CreateOrdineRequest()
            {
                Data = DateTime.Now,
                Indirizzo = new IndirizzoDto(indirizzi[0]),
                Voci = new List<CreateVoceOrdineRequest>()
                {
                    new CreateVoceOrdineRequest(){IdPortata = portate[0].Id, Quantita = 1},
                    new CreateVoceOrdineRequest(){IdPortata = portate[1].Id, Quantita = 1},
                }
            };

            var result = await _controller.CreateOrdineAsync(request);
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.TypeOf<BaseResponse<CreateOrdineResponse>>());

            var baseResponseValue = (BaseResponse<CreateOrdineResponse>)okResult.Value;
            Assert.True(baseResponseValue.Success);
            Assert.That(baseResponseValue.Result.Ordine.Voci.Count, Is.EqualTo(2));
            Assert.That(baseResponseValue.Result.Totale, Is.EqualTo(16M));
        }

        [Test]
        public async Task ShouldCreateOrder2()
        {
            CreateOrdineRequest request = new CreateOrdineRequest()
            {
                Data = DateTime.Now,
                Indirizzo = new IndirizzoDto(indirizzi[1]),
                Voci = new List<CreateVoceOrdineRequest>()
                {
                    new CreateVoceOrdineRequest(){IdPortata = portate[0].Id, Quantita = 2},
                    new CreateVoceOrdineRequest(){IdPortata = portate[2].Id, Quantita = 1},
                    new CreateVoceOrdineRequest(){IdPortata = portate[4].Id, Quantita = 1},
                    new CreateVoceOrdineRequest(){IdPortata = portate[5].Id, Quantita = 1}
                }
            };

            var result = await _controller.CreateOrdineAsync(request);
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.TypeOf<BaseResponse<CreateOrdineResponse>>());

            var baseResponseValue = (BaseResponse<CreateOrdineResponse>)okResult.Value;
            Assert.True(baseResponseValue.Success);
            Assert.That(baseResponseValue.Result.Ordine.Voci.Count, Is.EqualTo(4));
            Assert.That(baseResponseValue.Result.Totale, Is.EqualTo(31.9M));
        }

        [Test]
        public async Task ShouldCreateOrder3()
        {
            CreateOrdineRequest request = new CreateOrdineRequest()
            {
                Data = DateTime.Now,
                Indirizzo = new IndirizzoDto(indirizzi[0]),
                Voci = new List<CreateVoceOrdineRequest>()
                {
                    new CreateVoceOrdineRequest(){IdPortata = portate[0].Id, Quantita = 2},
                    new CreateVoceOrdineRequest(){IdPortata = portate[1].Id, Quantita = 3},
                    new CreateVoceOrdineRequest(){IdPortata = portate[2].Id, Quantita = 2},
                    new CreateVoceOrdineRequest(){IdPortata = portate[3].Id, Quantita = 5},
                    new CreateVoceOrdineRequest(){IdPortata = portate[4].Id, Quantita = 5},
                    new CreateVoceOrdineRequest(){IdPortata = portate[5].Id, Quantita = 3},
                }
            };

            var result = await _controller.CreateOrdineAsync(request);
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.TypeOf<BaseResponse<CreateOrdineResponse>>());

            var baseResponseValue = (BaseResponse<CreateOrdineResponse>)okResult.Value;
            Assert.True(baseResponseValue.Success);
            Assert.That(baseResponseValue.Result.Ordine.Voci.Count, Is.EqualTo(6));
            Assert.That(baseResponseValue.Result.Totale, Is.EqualTo(139.7M));
        }

        [Test]
        public async Task ShouldReuseAddress()
        {
            CreateOrdineRequest request1 = new CreateOrdineRequest()
            {
                Data = DateTime.Now,
                Indirizzo = new IndirizzoDto(indirizzi[0]),
                Voci = new List<CreateVoceOrdineRequest>()
                {
                    new CreateVoceOrdineRequest(){IdPortata = portate[0].Id, Quantita = 1},
                    new CreateVoceOrdineRequest(){IdPortata = portate[1].Id, Quantita = 1},
                }
            };

            CreateOrdineRequest request2 = new CreateOrdineRequest()
            {
                Data = DateTime.Now.AddDays(10),
                Indirizzo = new IndirizzoDto(indirizzi[0]),
                Voci = new List<CreateVoceOrdineRequest>()
                {
                    new CreateVoceOrdineRequest() { IdPortata = portate[0].Id, Quantita = 1 },
                    new CreateVoceOrdineRequest() { IdPortata = portate[2].Id, Quantita = 1 },
                }
            };

            await _controller.CreateOrdineAsync(request1);
            await _controller.CreateOrdineAsync(request2);

            var result = await _indirizzoRepository.OttieniTuttiAsync();

            Assert.That(result.Count, Is.EqualTo(1));

            var indirizzo = result.First();
            Assert.That(indirizzo.Via, Is.EqualTo(indirizzi[0].Via));
            Assert.That(indirizzo.NumeroCivico, Is.EqualTo(indirizzi[0].NumeroCivico));
            Assert.That(indirizzo.Citta, Is.EqualTo(indirizzi[0].Citta));
            Assert.That(indirizzo.CAP, Is.EqualTo(indirizzi[0].CAP));
        }

        [Test]
        public void ShouldNotFindCourse()
        {
            CreateOrdineRequest request = new CreateOrdineRequest()
            {
                Data = DateTime.Now,
                Indirizzo = new IndirizzoDto(indirizzi[0]),
                Voci = new List<CreateVoceOrdineRequest>()
                {
                    new CreateVoceOrdineRequest(){IdPortata = 0, Quantita = 1},
                    new CreateVoceOrdineRequest(){IdPortata = portate[1].Id, Quantita = 2},
                }
            };

            Assert.ThrowsAsync<ArgumentException>(() => _controller.CreateOrdineAsync(request));
        }

        [Test]
        public async Task ShouldGetOrders()
        {
            await TestUtils.SetUpSampleDatabase();
            MockControllerWithUser(utenti[0]);

            GetOrdiniRequest request = new GetOrdiniRequest()
            {
                Paginazione = new PaginazioneRequest()
                {
                    DimensionePagina = 10,
                    NumeroPagina = 0
                },
                DataInizio = DateTime.MinValue,
                DataFine = DateTime.MaxValue,
                IdCliente = null
            };

            var result = await _controller.GetOrdiniAsync(request);
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.TypeOf<BaseResponse<GetOrdiniResponse>>());
            var baseResponseValue = (BaseResponse<GetOrdiniResponse>)okResult.Value;
            Assert.True(baseResponseValue.Success);

            Assert.That(baseResponseValue.Result.Ordini.Count, Is.EqualTo(6));
            Assert.That(baseResponseValue.Result.Ordini[0].Data, Is.EqualTo(new DateTime(2024, 1, 1, 12, 0, 0)));
            Assert.That(baseResponseValue.Result.Ordini[0].Voci.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task ShouldGetOrdersWithPaging()
        {
            await TestUtils.SetUpSampleDatabase();
            MockControllerWithUser(utenti[0]);

            GetOrdiniRequest request = new GetOrdiniRequest()
            {
                Paginazione = new PaginazioneRequest()
                {
                    DimensionePagina = 4,
                    NumeroPagina = 0
                },
                DataInizio = DateTime.MinValue,
                DataFine = DateTime.MaxValue,
                IdCliente = null
            };

            var result = await _controller.GetOrdiniAsync(request);
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.TypeOf<BaseResponse<GetOrdiniResponse>>());
            var baseResponseValue = (BaseResponse<GetOrdiniResponse>)okResult.Value;
            Assert.True(baseResponseValue.Success);

            Assert.That(baseResponseValue.Result.Ordini.Count, Is.EqualTo(4));
            Assert.That(baseResponseValue.Result.Ordini[3].Data, Is.EqualTo(new DateTime(2024, 4, 1, 18, 30, 0)));
            Assert.That(baseResponseValue.Result.Paginazione.NumeroPagina, Is.EqualTo(0));
            Assert.That(baseResponseValue.Result.Paginazione.PagineTotali, Is.EqualTo(2));
        }

        [Test]
        public async Task ShouldFilterByDate()
        {
            await TestUtils.SetUpSampleDatabase();
            MockControllerWithUser(utenti[0]);

            GetOrdiniRequest request = new GetOrdiniRequest()
            {
                Paginazione = new PaginazioneRequest()
                {
                    DimensionePagina = 10,
                    NumeroPagina = 0
                },
                DataInizio = new DateTime(2024, 3, 1, 0, 0, 0),
                DataFine = new DateTime(2024, 6, 1, 20, 0, 0),
                IdCliente = null
            };

            var result = await _controller.GetOrdiniAsync(request);
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.TypeOf<BaseResponse<GetOrdiniResponse>>());
            var baseResponseValue = (BaseResponse<GetOrdiniResponse>)okResult.Value;
            Assert.True(baseResponseValue.Success);

            Assert.That(baseResponseValue.Result.Ordini.Count, Is.EqualTo(4));
            Assert.That(baseResponseValue.Result.Ordini[2].Data, Is.EqualTo(new DateTime(2024, 5, 1, 19, 0, 0)));
        }

        [Test]
        public async Task ShouldGetCustomerOrders()
        {
            await TestUtils.SetUpSampleDatabase();
            MockControllerWithUser(utenti[1]);

            GetOrdiniRequest request = new GetOrdiniRequest()
            {
                Paginazione = new PaginazioneRequest()
                {
                    DimensionePagina = 10,
                    NumeroPagina = 0
                },
                DataInizio = DateTime.MinValue,
                DataFine = DateTime.MaxValue,
                IdCliente = null
            };

            var result = await _controller.GetOrdiniAsync(request);
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.TypeOf<BaseResponse<GetOrdiniResponse>>());
            var baseResponseValue = (BaseResponse<GetOrdiniResponse>)okResult.Value;
            Assert.True(baseResponseValue.Success);

            Assert.That(baseResponseValue.Result.Ordini.Count, Is.EqualTo(2));
            Assert.That(baseResponseValue.Result.Ordini[0].Data, Is.EqualTo(new DateTime(2024, 2, 1, 13, 00, 0)));
            Assert.That(baseResponseValue.Result.Ordini[1].Data, Is.EqualTo(new DateTime(2024, 4, 1, 18, 30, 0)));
        }

        [Test]
        public async Task ShouldGetCustomerOrdersByDate()
        {
            await TestUtils.SetUpSampleDatabase();
            MockControllerWithUser(utenti[1]);

            GetOrdiniRequest request = new GetOrdiniRequest()
            {
                Paginazione = new PaginazioneRequest()
                {
                    DimensionePagina = 10,
                    NumeroPagina = 0
                },
                DataInizio = new DateTime(2024, 3, 1, 0, 0, 0),
                DataFine = new DateTime(2024, 5, 1, 20, 0, 0),
                IdCliente = null
            };

            var result = await _controller.GetOrdiniAsync(request);
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.TypeOf<BaseResponse<GetOrdiniResponse>>());
            var baseResponseValue = (BaseResponse<GetOrdiniResponse>)okResult.Value;
            Assert.True(baseResponseValue.Success);

            Assert.That(baseResponseValue.Result.Ordini.Count, Is.EqualTo(1));
            Assert.That(baseResponseValue.Result.Ordini[0].Data, Is.EqualTo(new DateTime(2024, 4, 1, 18, 30, 0)));
        }
    }
}
