using FluentValidation;
using System.Text.RegularExpressions;
using LR_3.Models;

namespace LR_3.Validators
{
    public class TicketOrderValidator : AbstractValidator<TicketOrder>
    {
        public TicketOrderValidator()
        {

            RuleFor(x => x.customer_email).NotEmpty().WithMessage("Email має бути обов'язково!").EmailAddress().WithMessage("Неправильний формат email! (приклад: name@gmail.com)");

            RuleFor(x => x.status).IsInEnum();

            RuleFor(x => x.customer_phone).NotEmpty().WithMessage("Номер телефону має бути обов'язково!").Must(p => string.IsNullOrWhiteSpace(p) || Regex.IsMatch(p, @"^\+?\d{10,15}$"))
            .WithMessage("Невалідний номер телефону");

            RuleFor(o => o.RequestedSeats)
                .InclusiveBetween(1, 6).WithMessage("Можна забронювати від 1 до 6 квитків за раз!");

            RuleFor(x => x.Tickets).Must(tickets => tickets == null || tickets.Count == 0).WithMessage("Квитки не створювати самостійно!");
            RuleFor(x => x.TotalAmount).Equal(0).WithMessage("Поле 'TotalAmount' розраховується. Не можна передавати його у запиті!");
            RuleFor(x => x.RefundAmount).Equal(0).WithMessage("Поле 'RefundAmount' розраховується. Не можна передавати його у запиті!");
        }
    }
    }

