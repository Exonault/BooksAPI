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
        
        RuleFor(x => x.DemographicType)
            .NotEmpty()
            .WithMessage(LibraryComicMessages.DemographicTypeRequiredMessage)
            .Must(x => LibraryComicConstants.DemographicType.DemographicTypes.Contains(x))
            .WithMessage(LibraryComicMessages.DemographicTypeMessage);

        RuleFor(x => x.ComicType)
            .NotEmpty()
            .WithMessage(LibraryComicMessages.ComicTypeRequiredMessage)
            .Must(x => LibraryComicConstants.ComicType.ComicTypes.Contains(x))
            .WithMessage(LibraryComicMessages.ComicTypeValidationMessage);

        RuleFor(x => x.PublishingStatus)
            .NotEmpty()
            .WithMessage(LibraryComicMessages.PublishingStatusRequiredMessage)
            .Must(x => LibraryComicConstants.PublishingType.PublishingStatuses.Contains(x))
            .WithMessage(LibraryComicMessages.PublishingStatusValidationMessage);

        RuleForEach(x => x.Authors)
            .SetValidator(new AuthorValidator());
        
        RuleFor(x => new { x.TotalVolumes, x.PublishingStatus })
            .Custom((obj, context) =>
        {
            if (obj.PublishingStatus is (LibraryComicConstants.PublishingType.Finished or LibraryComicConstants.PublishingType.OnHiatus) && obj.TotalVolumes == 0)
            {
                context.AddFailure("TotalVolumes", LibraryComicMessages.TotalVolumesRequired);
            }
        });
    }
}