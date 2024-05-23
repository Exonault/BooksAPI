using BooksAPI.BE.Constants;
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
            .WithMessage(OrderMessages.DescriptionRequired);

        RuleFor(x => x.Status)
            .NotEmpty()
            .WithMessage(OrderMessages.StatusRequired)
            .Must(x=> OrderConstants.Status.OrderStatuses.Contains(x))
            .WithMessage(OrderMessages.StatusValidationMessage);

        RuleFor(x => x.Amount)
            .NotEmpty()
            .WithMessage(OrderMessages.AmountRequired)
            .PrecisionScale(int.MaxValue,2, true)
            .WithMessage(OrderMessages.AmountValueMessage);

        RuleFor(x => x.Place)
            .NotEmpty()
            .WithMessage(OrderMessages.PlaceRequired);

        RuleFor(x => x.NumberOfItems)
            .NotEmpty()
            .Must(x => x > 0)
            .WithMessage(OrderMessages.NumberOfItemsRequired);

        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage(OrderMessages.DateRequired)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage(OrderMessages.DateValueMessage);
    }
}