using Unicam.Ristorante.Application.Models.Requests;
using Unicam.Ristorante.Application.Validators;

namespace Unicam.Ristorante.Testing.Validators
{
    public class CreateClienteRequestValidatorTest
    {
        private CreateClienteRequestValidator _validator = new CreateClienteRequestValidator();

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
                Email = "mario@rossi@popmail.com",
                Password = "PassWord123"
            },
            new CreateClienteRequest()
            {
                Email = "luigi.bianchi@hotmail.it",
                Password = "Pass1"
            },
            new CreateClienteRequest()
            {
                Email = "luigi.bianchi@hotmail.it",
                Password = "password"
            },
            new CreateClienteRequest()
            {
                Email = "luigi@bianchi@hotmail.it",
                Password = ""
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
            Assert.That(result.Errors[0].PropertyName, Is.EqualTo("Email"));
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(CreateClienteRequestValidator.InvalidEmailMessage));
        }

        [Test]
        public void ShouldNotValidate2()
        {
            var result = _validator.Validate(requests[2]);

            Assert.False(result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].PropertyName, Is.EqualTo("Password"));
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(CreateClienteRequestValidator.ShortPasswordMessage));
        }

        [Test]
        public void ShouldNotValidate3()
        {
            var result = _validator.Validate(requests[3]);

            Assert.False(result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].PropertyName, Is.EqualTo("Password"));
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(CreateClienteRequestValidator.InvalidPasswordMessage));
        }

        [Test]
        public void ShouldNotValidate4()
        {
            var result = _validator.Validate(requests[4]);

            Assert.False(result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(4));
            Assert.That(result.Errors[0].PropertyName, Is.EqualTo("Email"));
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(CreateClienteRequestValidator.InvalidEmailMessage));
            Assert.That(result.Errors[1].PropertyName, Is.EqualTo("Password"));
            Assert.That(result.Errors[1].ErrorMessage, Is.EqualTo(CreateClienteRequestValidator.RequiredPasswordMessage));
            Assert.That(result.Errors[2].PropertyName, Is.EqualTo("Password"));
            Assert.That(result.Errors[2].ErrorMessage, Is.EqualTo(CreateClienteRequestValidator.ShortPasswordMessage));
            Assert.That(result.Errors[3].PropertyName, Is.EqualTo("Password"));
            Assert.That(result.Errors[3].ErrorMessage, Is.EqualTo(CreateClienteRequestValidator.InvalidPasswordMessage));
        }
    }
}
