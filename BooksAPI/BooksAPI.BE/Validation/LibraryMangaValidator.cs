using BooksAPI.BE.Constants;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Messages;
using FluentValidation;

namespace BooksAPI.BE.Validation;

public class LibraryMangaValidator : AbstractValidator<LibraryManga>
{
    public LibraryMangaValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage(LibraryMangaMessages.TitleValidationMessage);
        
        RuleFor(x => x.DemographicType)
            .NotEmpty()
            .WithMessage(LibraryMangaMessages.DemographicTypeRequiredMessage)
            .Must(x => LibraryMangaConstants.DemographicType.DemographicTypes.Contains(x))
            .WithMessage(LibraryMangaMessages.DemographicTypeMessage);

        RuleFor(x => x.Type)
            .NotEmpty()
            .WithMessage(LibraryMangaMessages.TypeRequiredMessage)
            .Must(x => LibraryMangaConstants.ComicType.ComicTypes.Contains(x))
            .WithMessage(LibraryMangaMessages.ComicTypeValidationMessage);

        RuleFor(x => x.PublishingStatus)
            .NotEmpty()
            .WithMessage(LibraryMangaMessages.PublishingStatusRequiredMessage)
            .Must(x => LibraryMangaConstants.PublishingType.PublishingStatuses.Contains(x))
            .WithMessage(LibraryMangaMessages.PublishingStatusValidationMessage);

        RuleForEach(x => x.Authors)
            .SetValidator(new AuthorValidator());
        
        RuleFor(x => new { x.TotalVolumes, x.PublishingStatus })
            .Custom((obj, context) =>
        {
            if (obj.PublishingStatus is (LibraryMangaConstants.PublishingType.Finished or LibraryMangaConstants.PublishingType.OnHiatus) && obj.TotalVolumes == 0)
            {
                context.AddFailure("TotalVolumes", LibraryMangaMessages.TotalVolumesRequired);
            }
        });
    }
}