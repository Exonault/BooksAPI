using BooksAPI.Entities;
using FluentValidation;

namespace BooksAPI.Validation;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("Date is required");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required");
        
        RuleFor(x => x.Place)
            .NotEmpty()
            .WithMessage("Place is required");

        RuleFor(x => x.Amount)
            .NotEmpty()
            .WithMessage("Amount is required");

        RuleFor(x => x.NumberOfItems)
            .NotEmpty()
            .WithMessage("Number of items is required");
    }
}