using BooksAPI.Constants;
using BooksAPI.Entities;
using FluentValidation;


namespace BooksAPI.Validation;

public class BookValidator : AbstractValidator<Book>
{
    public BookValidator()
    {
        RuleFor(x => x.Author)
            .NotEmpty()
            .WithMessage("Author name is required");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required");

        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage("Price is required");

        RuleFor(x => x.ReadingStatus)
            .Must(x => AppConstants.ReadingStatuses.Contains(x))
            .WithMessage("Status must be one of the following: " + String.Join(", ", AppConstants.ReadingStatuses));
    }
}