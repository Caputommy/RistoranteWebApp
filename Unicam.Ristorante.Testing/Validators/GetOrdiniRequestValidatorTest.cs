using Unicam.Ristorante.Application.Models.Requests;
using Unicam.Ristorante.Application.Validators;

namespace Unicam.Ristorante.Testing.Validators
{
    [TestFixture]
    internal class GetOrdiniRequestValidatorTest
    {
        private static GetOrdiniRequest[] requests =
        {
            new GetOrdiniRequest()
            {
                Paginazione = new PaginazioneRequest()
                {
                    DimensionePagina = 10,
                    NumeroPagina = 0
                },
                DataInizio = DateTime.Now.AddDays(-1),
                DataFine = DateTime.Now,
                IdCliente = 1
            },
            new GetOrdiniRequest()
            {
                Paginazione = new PaginazioneRequest()
                {
                    DimensionePagina = 10,
                    NumeroPagina = 0
                },
                DataInizio = DateTime.Now.AddDays(-1),
                DataFine = DateTime.Now,
                IdCliente = null
            },
            new GetOrdiniRequest()
            {
                Paginazione = new PaginazioneRequest()
                {
                    DimensionePagina = 10,
                    NumeroPagina = 0
                },
                DataInizio = DateTime.Now,
                DataFine = DateTime.Now.AddDays(-1),
                IdCliente = 1
            },
            new GetOrdiniRequest()
            {
                Paginazione = new PaginazioneRequest()
                {
                    DimensionePagina = -1,
                    NumeroPagina = -1
                },
                DataInizio = DateTime.Now,
                DataFine = DateTime.Now.AddDays(1),
                IdCliente = 1
            },
        };

        [Test]
        public void ShouldValidate()
        {
            var result = new GetOrdiniRequestValidator().Validate(requests[0]);

            Assert.True(result.IsValid);
        }

        [Test]
        public void ShouldNotValidate1()
        {
            var result = new GetOrdiniRequestValidator().Validate(requests[1]);

            Assert.False(result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].PropertyName, Is.EqualTo("IdCliente"));
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(GetOrdiniRequestValidator.RequiredClienteMessage));
        }

        [Test]
        public void ShouldNotValidate2()
        {
            var result = new GetOrdiniRequestValidator().Validate(requests[2]);

            Assert.False(result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(GetOrdiniRequestValidator.InvalidDateMessage));
        }

        [Test]
        public void ShouldNotValidate3()
        {
            var result = new GetOrdiniRequestValidator().Validate(requests[3]);

            Assert.False(result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(2));
            Assert.That(result.Errors[0].PropertyName, Is.EqualTo("Paginazione.DimensionePagina"));
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(PaginazioneRequestValidator.PositivePageSizeMessage));
            Assert.That(result.Errors[1].PropertyName, Is.EqualTo("Paginazione.NumeroPagina"));
            Assert.That(result.Errors[1].ErrorMessage, Is.EqualTo(PaginazioneRequestValidator.PositivePageNumMessage));
        }
    }
}
