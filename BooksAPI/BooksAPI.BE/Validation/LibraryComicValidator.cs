using BooksAPI.BE.Constants;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Messages;
using FluentValidation;

namespace BooksAPI.BE.Validation;

public class LibraryComicValidator : AbstractValidator<LibraryComic>
{
    public LibraryComicValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage(LibraryComicMessages.TitleValidationMessage);

        RuleFor(x => x.Author)
            .NotEmpty()
            .WithMessage(LibraryComicMessages.AuthorValidationMessage);
        
        RuleFor(x => x.DemographicType)
            .NotEmpty()
            .WithMessage(LibraryComicMessages.DemographicTypeRequiredMessage)
            .Must(x => LibraryComicConstants.DemographicTypes.Contains(x))
            .WithMessage(LibraryComicMessages.DemographicTypeMessage);

        RuleFor(x => x.ComicType)
            .NotEmpty()
            .WithMessage(LibraryComicMessages.ComicTypeRequiredMessage)
            .Must(x => LibraryComicConstants.ComicTypes.Contains(x))
            .WithMessage(LibraryComicMessages.ComicTypeValidationMessage);

        RuleFor(x => x.PublishingStatus)
            .NotEmpty()
            .WithMessage(LibraryComicMessages.PublishingStatusRequiredMessage)
            .Must(x => LibraryComicConstants.PublishingStatuses.Contains(x))
            .WithMessage(LibraryComicMessages.PublishingStatusValidationMessage);

        RuleFor(x => x.TotalVolumes)
            .Must(x => x >= 1)
            .WithMessage(LibraryComicMessages.TotalVolumesValidationMessage);

        RuleFor(x => x.TotalChapters)
            .Must(x => x >= 1)
            .WithMessage(LibraryComicMessages.TotalChaptersValidationMessage);
    }
}