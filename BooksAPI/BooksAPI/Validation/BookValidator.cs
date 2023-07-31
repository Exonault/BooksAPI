using BooksAPI.Constants;
using BooksAPI.Entities;
using BooksAPI.Messages;
using FluentValidation;


namespace BooksAPI.Validation;

public class BookValidator : AbstractValidator<Book>
{
    public BookValidator()
    {
        RuleFor(x => x.Author)
            .NotEmpty()
            .WithMessage(BookValidationMessages.AuthorValidationMessage);

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage(BookValidationMessages.TitleValidationMessage);

        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage(BookValidationMessages.PriceValidationMessage);

        RuleFor(x => x.ReadingStatus)
            .Must(x => AppConstants.ReadingStatuses.Contains(x))
            .WithMessage(BookValidationMessages.ReadingStatusMessageMessage);
    }
}