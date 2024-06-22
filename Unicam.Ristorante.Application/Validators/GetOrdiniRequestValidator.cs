using FluentValidation;
using Unicam.Ristorante.Application.Models.Requests;

namespace Unicam.Ristorante.Application.Validators
{
    public class GetOrdiniRequestValidator : AbstractValidator<GetOrdiniRequest>
    {
        public static string RequiredDateMessage = "Entrambe le date di inzio e fine sono richieste";
        public static string InvalidDateMessage = "La data di inizio deve essere precedente a quella di fine";
        public static string RequiredPaginazioneMessage = "Paginazione richiesta";

        public GetOrdiniRequestValidator()
        {
            RuleFor(r => r.DataInizio)
                .NotNull().WithMessage(RequiredDateMessage)
                .NotEmpty().WithMessage(RequiredDateMessage);

            RuleFor(r => r.DataFine)
                .NotNull().WithMessage(RequiredDateMessage)
                .NotEmpty().WithMessage(RequiredDateMessage);

            RuleFor(r => new { r.DataInizio, r.DataFine })
                .Must(r => r.DataInizio < r.DataFine)
                .WithMessage(InvalidDateMessage);

            RuleFor(r => r.Paginazione)
                .NotNull().WithMessage(RequiredPaginazioneMessage)
                .SetValidator(new PaginazioneRequestValidator());
        }
    }
}
