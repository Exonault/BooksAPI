using BooksAPI.Entities;
using BooksAPI.Messages;
using FluentValidation;

namespace BooksAPI.Validation;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage(OrderValidationMessages.DateValidationMessage);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(OrderValidationMessages.DescriptionValidationMessage);

        RuleFor(x => x.Place)
            .NotEmpty()
            .WithMessage(OrderValidationMessages.PlaceValidationMessage);

        RuleFor(x => x.Amount)
            .NotEmpty()
            .WithMessage(OrderValidationMessages.AmountValidationMessage);

        RuleFor(x => x.NumberOfItems)
            .NotEmpty()
            .WithMessage(OrderValidationMessages.NumberOfItemsValidationMessage);
    }
}