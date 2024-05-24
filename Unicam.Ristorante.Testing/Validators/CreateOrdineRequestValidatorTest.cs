using Unicam.Ristorante.Application.Models.Dtos;
using Unicam.Ristorante.Application.Models.Requests;
using Unicam.Ristorante.Application.Validators;
using static Unicam.Ristorante.Application.Models.Requests.CreateOrdineRequest;

namespace Unicam.Ristorante.Testing.Validators
{
    [TestFixture]
    internal class CreateOrdineRequestValidatorTest
    {
        private CreateOrdineRequestValidator _validator = new CreateOrdineRequestValidator();

        private static Indirizzo[] indirizzi = [
            new Indirizzo()
            {
                Via = "Via Roma",
                NumeroCivico = "1",
                Citta = "Ancona",
                CAP = "60125"
            },
            new Indirizzo()
            {
                Via = "Via Milano",
                NumeroCivico = "",
                Citta = "Ancona",
                CAP = "60125"
            }
        ];

        private static CreateOrdineRequest[] requests =
        {
            new CreateOrdineRequest()
            {   
                Data = DateTime.Now.AddDays(1),
                Indirizzo = new IndirizzoDto(indirizzi[0]),
                Voci = new List<CreateVoceOrdineRequest>()
                {
                    new CreateVoceOrdineRequest()
                    {
                        IdPortata = 1,
                        Quantita = 2
                    },
                    new CreateVoceOrdineRequest()
                    {
                        IdPortata = 2,
                        Quantita = 1
                    }
                }
            },
            new CreateOrdineRequest()
            {
                Data = DateTime.Now.AddDays(-1),
                Indirizzo = new IndirizzoDto(indirizzi[0]),
                Voci = new List<CreateVoceOrdineRequest>()
                {
                    new CreateVoceOrdineRequest()
                    {
                        IdPortata = 1,
                        Quantita = 2
                    },
                    new CreateVoceOrdineRequest()
                    {
                        IdPortata = 2,
                        Quantita = 1
                    }
                }
            },
            new CreateOrdineRequest()
            {
                Data = DateTime.Now.AddDays(10),
                Indirizzo = new IndirizzoDto(indirizzi[1]),
                Voci = new List<CreateVoceOrdineRequest>()
                {
                    new CreateVoceOrdineRequest()
                    {
                        IdPortata = 1,
                        Quantita = 2
                    },
                    new CreateVoceOrdineRequest()
                    {
                        IdPortata = 2,
                        Quantita = 1
                    }
                }
            },
            new CreateOrdineRequest()
            {
                Data = DateTime.Now.AddDays(10),
                Indirizzo = new IndirizzoDto(indirizzi[0]),
                Voci = new List<CreateVoceOrdineRequest>()
                {
                    new CreateVoceOrdineRequest()
                    {
                        IdPortata = 1,
                        Quantita = -1
                    },
                    new CreateVoceOrdineRequest()
                    {
                        IdPortata = 2,
                        Quantita = 0
                    }
                }
            }
        };

        [Test]
        public void ShouldValidate()
        {
            var result = _validator.Validate(requests[0]);

            Assert.True(result.IsValid);
        }

        [Test]
        public void ShouldNotValidate1()
        {
            var result = _validator.Validate(requests[1]);

            Assert.False(result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].PropertyName, Is.EqualTo("Data"));
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(CreateOrdineRequestValidator.FutureDateMessage));
        }

        [Test]
        public void ShouldNotValidate2()
        {
            var result = _validator.Validate(requests[2]);

            Assert.False(result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].PropertyName, Is.EqualTo("Indirizzo.NumeroCivico"));
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(IndirizzoRequestValidator.RequiredNumberMessage));
        }

        [Test]
        public void ShouldNotValidate3()
        {
            var result = _validator.Validate(requests[3]);

            Assert.False(result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(2));
            Assert.That(result.Errors[0].PropertyName, Is.EqualTo("Voci[0].Quantita"));
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(CreateVoceOrdineRequestValidator.PositiveQuantityMessage));
            Assert.That(result.Errors[1].PropertyName, Is.EqualTo("Voci[1].Quantita"));
            Assert.That(result.Errors[1].ErrorMessage, Is.EqualTo(CreateVoceOrdineRequestValidator.PositiveQuantityMessage));
        }
    }
}
