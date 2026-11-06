using FluentValidation;
using LR_3.DTO.Event;
namespace LR_3.Validators
{
    public class EventDTOValidator : AbstractValidator<CreateEventDTO>
    {
        public EventDTOValidator() { 
        RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Назва події є обов'язковою.")
                .MaximumLength(100).WithMessage("Назва не може перевищувати 100 символів.");

        RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Опис події не може бути порожнім.");

        RuleFor(x => x.startDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("Дата події не може бути в минулому!");
        }
    }

    public class UpdateEventDTOValidator : AbstractValidator<UpdateEventDTO>
    {
        public UpdateEventDTOValidator()
        {
            RuleFor(x => x.Title).MinimumLength(3).WithMessage("Назва має містити щонайменше 3 символи.")
                .MaximumLength(40).WithMessage("Назва не може перевищувати 40 символів.")
                .When(x => x.Title != null);

        }
    }
}
