using FluentValidation;
using Unicam.Ristorante.Application.Extensions;
using Unicam.Ristorante.Application.Models.Dtos;

namespace Unicam.Ristorante.Application.Validators
{
    public class IndirizzoRequestValidator : AbstractValidator<IndirizzoDto>
    {
        public static string RequiredStreetMessage = "Via richiesta";
        public static string RequiredNumberMessage = "Numero civico richiesto";
        public static string RequiredCityMessage = "Città richiesta";
        public static string RequiredCAPMessage = "CAP richiesto";
        public static string InvalidLengthCAPMessage = "Il CAP deve essere composto da 5 cifre";

        public IndirizzoRequestValidator()
        {
            RuleFor(p => p.Via)
                .NotNull().WithMessage(RequiredStreetMessage)
                .NotEmpty().WithMessage(RequiredStreetMessage);

            RuleFor(p => p.NumeroCivico)
                .NotNull().WithMessage(RequiredNumberMessage)
                .NotEmpty().WithMessage(RequiredNumberMessage);

            RuleFor(p => p.Citta)
                .NotNull().WithMessage(RequiredCityMessage)
                .NotEmpty().WithMessage(RequiredCityMessage);

            RuleFor(p => p.CAP)
                .NotNull().WithMessage(RequiredCAPMessage)
                .NotEmpty().WithMessage(RequiredCAPMessage)
                .Length(5).WithMessage(InvalidLengthCAPMessage)
                .RegEx(@"^\d{5}$", InvalidLengthCAPMessage);
        }
    }
}
