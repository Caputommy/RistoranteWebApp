using FluentValidation;
using Unicam.Ristorante.Application.Models.Requests;

namespace Unicam.Ristorante.Application.Validators
{
    public class PaginazioneRequestValidator : AbstractValidator<PaginazioneRequest>
    {
        public static string RequiredPageSizeMessage = "Dimensione della pagina richiesta";
        public static string RequiredPageNumMessage = "Dimensione della pagina richiesta";
        public static string PositivePageSizeMessage = "La dimensione della pagina deve essere maggiore di 0";
        public static string PositivePageNumMessage = "Il numero di pagina deve essere maggiore o uguale a 0";

        public PaginazioneRequestValidator()
        {
            RuleFor(p => p.DimensionePagina)
                .NotNull().WithMessage(RequiredPageSizeMessage)
                .GreaterThan(0).WithMessage(PositivePageSizeMessage);

            RuleFor(p => p.NumeroPagina)
                .NotNull().WithMessage(RequiredPageNumMessage)
                .GreaterThanOrEqualTo(0).WithMessage(PositivePageNumMessage);
        }
    }
}
