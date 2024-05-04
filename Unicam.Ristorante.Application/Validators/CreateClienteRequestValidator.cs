using FluentValidation;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Unicam.Ristorante.Application.Models.Requests;

namespace Unicam.Ristorante.Application.Validators
{
    public class CreateClienteRequestValidator : AbstractValidator<CreateClienteRequest>
    {
        public static string RequiredEmailMessage = "Email richiesta";
        public static string InvalidEmailMessage = "L'indirizzo email non è valido";
        public static string RequiredPasswordMessage = "Password richiesta";
        public static string ShortPasswordMessage = "La password deve essere di almeno 8 caratteri";

        public CreateClienteRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(RequiredEmailMessage)
                .EmailAddress().WithMessage(InvalidEmailMessage);
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(RequiredPasswordMessage)
                .MinimumLength(8).WithMessage(ShortPasswordMessage);
                //TODO: Add regex rule for password
        }
    }
}
