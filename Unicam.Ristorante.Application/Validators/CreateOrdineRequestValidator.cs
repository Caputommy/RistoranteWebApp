using FluentValidation;
using Unicam.Ristorante.Application.Models.Requests;

namespace Unicam.Ristorante.Application.Validators
{
    public class CreateOrdineRequestValidator : AbstractValidator<CreateOrdineRequest>
    {
        public static string RequiredDateMessage = "Data richiesta";
        public static string FutureDateMessage = "La data deve essere oggi o successiva ad oggi";
        public static string RequiredAddressMessage = "Indirizzo richiesto";
        public static string RequiredOrderItemsMessage = "Almeno una voce d'ordine richiesta";

        public CreateOrdineRequestValidator() 
        {
            RuleFor(p => p.Data)
                .NotNull().WithMessage(RequiredDateMessage)
                .GreaterThanOrEqualTo(DateTime.Now.Date).WithMessage(FutureDateMessage);

            RuleFor(p => p.Indirizzo)
                .NotNull().WithMessage(RequiredAddressMessage)
                .SetValidator(new IndirizzoRequestValidator());

            RuleFor(p => p.Voci)
                .NotEmpty().WithMessage(RequiredOrderItemsMessage)
                .ForEach(v => v.SetValidator(new CreateVoceOrdineRequestValidator()));

        }
    }
}
