using BooksAPI.BE.Entities;
using BooksAPI.BE.Messages;
using FluentValidation;

namespace BooksAPI.BE.Validation;

public class OrderValidator:AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(OrderMessages.DescriptionMessage);

        RuleFor(x => x.Amount)
            .NotEmpty()
            .WithMessage(OrderMessages.AmountMessage)
            .PrecisionScale(int.MaxValue,2, true)
            .WithMessage(OrderMessages.AmountValueMessage);

        RuleFor(x => x.Place)
            .NotEmpty()
            .WithMessage(OrderMessages.PlaceMessage);

        RuleFor(x => x.NumberOfItems)
            .NotEmpty()
            .Must(x => x > 0)
            .WithMessage(OrderMessages.NumberOfItemsMessage);

        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage(OrderMessages.DateMessage)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage(OrderMessages.DateValueMessage);
    }
}