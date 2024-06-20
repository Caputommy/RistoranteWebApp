using FluentValidation;
using Unicam.Ristorante.Application.Models.Requests;
using Unicam.Ristorante.Models.Entities;

namespace Unicam.Ristorante.Application.Validators
{
    public class CreatePortataRequestValidator : AbstractValidator<CreatePortataRequest>
    {
        public static string RequiredNameMessage = "Nome richiesto";
        public static string NegativePriceMessage = "Il prezzo non può essere negativo";
        public static string DecimalDigitsMessage = "Il prezzo può avere al più due cifre decimali";
        public static string InvalidTypeMessage = $"Il tipo deve essere compreso tra 1 e 4";

        public CreatePortataRequestValidator() 
        {
            RuleFor(p => p.Nome)
                .NotNull().WithMessage(RequiredNameMessage)
                .NotEmpty().WithMessage(RequiredNameMessage);

            RuleFor(p => p.Prezzo)
                .Custom(ValidaPrezzo);

            RuleFor(p => p.Tipo)
                .Custom(ValidaTipo);
        }

        private void ValidaPrezzo(decimal value, ValidationContext<CreatePortataRequest> context)
        {
            if (value < 0) context.AddFailure(NegativePriceMessage);
            if (((decimal)value).Scale > 2) context.AddFailure(DecimalDigitsMessage);
        }

        private void ValidaTipo(int tipo, ValidationContext<CreatePortataRequest> context)
        {
            if (!Enum.IsDefined(typeof(TipoPortata), tipo)) context.AddFailure(InvalidTypeMessage);
        }
    }
}
