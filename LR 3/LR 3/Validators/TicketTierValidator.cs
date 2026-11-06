using FluentValidation;
using LR_3.Models;

namespace LR_3.Validators
{
    public class TicketTierValidator : AbstractValidator<TicketTier>
    {
        public TicketTierValidator()
        {
           
            RuleFor(x => x.EventId)
                .NotNull().WithMessage("Поле 'EventId' має бути вказано.");

            RuleFor(x => x.Category).IsInEnum();

         
        }
    }
}
