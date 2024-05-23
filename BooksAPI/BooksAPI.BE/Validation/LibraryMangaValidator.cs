using BooksAPI.BE.Constants;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Messages;
using FluentValidation;

namespace BooksAPI.BE.Validation;

public class LibraryMangaValidator : AbstractValidator<LibraryManga>
{
    public LibraryMangaValidator()
    {
        RuleFor(x => x.TitleRomaji)
            .NotEmpty()
            .WithMessage(LibraryMangaMessages.TitleRomajiRequired);
        
        RuleFor(x => x.TitleJapanese)
            .NotEmpty()
            .WithMessage(LibraryMangaMessages.TitleJapaneseRequired);
        
        RuleFor(x => x.DemographicType)
            .NotEmpty()
            .WithMessage(LibraryMangaMessages.DemographicTypeRequired)
            .Must(x => LibraryMangaConstants.DemographicType.DemographicTypes.Contains(x))
            .WithMessage(LibraryMangaMessages.DemographicTypeMessage);

        RuleFor(x => x.Type)
            .NotEmpty()
            .WithMessage(LibraryMangaMessages.TypeRequiredMessage)
            .Must(x => LibraryMangaConstants.Type.ComicTypes.Contains(x))
            .WithMessage(LibraryMangaMessages.ComicTypeValidationMessage);

        RuleFor(x => x.PublishingStatus)
            .NotEmpty()
            .WithMessage(LibraryMangaMessages.PublishingStatusRequired)
            .Must(x => LibraryMangaConstants.PublishingType.PublishingStatuses.Contains(x))
            .WithMessage(LibraryMangaMessages.PublishingStatusValidationMessage);

        RuleFor(x => x.Synopsis)
            .NotEmpty()
            .WithMessage(LibraryMangaMessages.SynopsisRequired);
        
        RuleForEach(x => x.Authors)
            .SetValidator(new AuthorValidator());
        
        RuleFor(x => new { x.TotalVolumes, x.PublishingStatus })
            .Custom((obj, context) =>
        {
            if (obj.PublishingStatus is (LibraryMangaConstants.PublishingType.Finished or LibraryMangaConstants.PublishingType.OnHiatus) && obj.TotalVolumes == 0)
            {
                context.AddFailure("TotalVolumes", LibraryMangaMessages.TotalVolumesValidationMessage);
            }
        });
    }
}