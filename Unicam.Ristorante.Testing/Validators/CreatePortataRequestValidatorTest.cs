using Unicam.Ristorante.Application.Models.Requests;
using Unicam.Ristorante.Application.Validators;

namespace Unicam.Ristorante.Testing.Validators
{
    [TestFixture]
    internal class CreatePortataRequestValidatorTest
    {
        private CreatePortataRequestValidator _validator = new CreatePortataRequestValidator();

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
                Nome = "",
                Prezzo = 10m,
                Tipo = 2
            },
            new CreatePortataRequest()
            {
                Nome = "Tiramisù",
                Prezzo = -4.501m,
                Tipo = 4
            },
            new CreatePortataRequest()
            {
                Nome = "",
                Prezzo = 8.50m,
                Tipo = 5
            },
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
            Assert.That(result.Errors[0].PropertyName, Is.EqualTo("Nome"));
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(CreatePortataRequestValidator.RequiredNameMessage));
        }

        [Test]
        public void ShouldNotValidate2()
        {
            var result = _validator.Validate(requests[2]);

            Assert.False(result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(2));
            Assert.That(result.Errors[0].PropertyName, Is.EqualTo("Prezzo"));
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(CreatePortataRequestValidator.NegativePriceMessage));
            Assert.That(result.Errors[1].PropertyName, Is.EqualTo("Prezzo"));
            Assert.That(result.Errors[1].ErrorMessage, Is.EqualTo(CreatePortataRequestValidator.DecimalDigitsMessage));
        }

        [Test]
        public void ShouldNotValidate3()
        {
            var result = _validator.Validate(requests[3]);

            Assert.False(result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(2));
            Assert.That(result.Errors[0].PropertyName, Is.EqualTo("Nome"));
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(CreatePortataRequestValidator.RequiredNameMessage));
            Assert.That(result.Errors[1].PropertyName, Is.EqualTo("Tipo"));
            Assert.That(result.Errors[1].ErrorMessage, Is.EqualTo(CreatePortataRequestValidator.InvalidTypeMessage));
        }
    }
}
