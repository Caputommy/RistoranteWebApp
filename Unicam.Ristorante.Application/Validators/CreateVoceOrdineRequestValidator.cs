using FluentValidation;
using static Unicam.Ristorante.Application.Models.Requests.CreateOrdineRequest;

namespace Unicam.Ristorante.Application.Validators
{
    public class CreateVoceOrdineRequestValidator : AbstractValidator<CreateVoceOrdineRequest>
    {
        public static string RequiredCourseIdMessage = "Id portata richiesto";
        public static string RequiredQuantityMessage = "Quantità richiesta";
        public static string PositiveQuantityMessage = "La quantità deve essere maggiore di 0";
        public CreateVoceOrdineRequestValidator()
        {
            RuleFor(p => p.IdPortata)
                .NotNull().WithMessage(RequiredCourseIdMessage);

            RuleFor(p => p.Quantita)
                .NotNull().WithMessage(RequiredQuantityMessage)
                .GreaterThan(0).WithMessage(PositiveQuantityMessage);
        }
    }
}
