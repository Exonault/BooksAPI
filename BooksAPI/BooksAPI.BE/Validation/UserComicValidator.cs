using BooksAPI.BE.Constants;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Messages;
using FluentValidation;

namespace BooksAPI.BE.Validation;

public class UserComicValidator : AbstractValidator<UserComic>
{
    public UserComicValidator()
    {
        RuleFor(x => x.ReadingStatus)
            .NotEmpty()
            .WithMessage(UserComicMessages.ReadingStatusRequiredMessage)
            .Must(x => UserComicConstants.ReadingStatuses.Contains(x))
            .WithMessage(UserComicMessages.ReadingStatusValidationMessage);

        RuleFor(x => new { x.ReadVolumes, x.LibraryComic.TotalVolumes })
            .Must(x => x.ReadVolumes >= 1)
            .WithMessage(UserComicMessages.ReadVolumesRequiredMessage)
            .Must(x => x.ReadVolumes <= x.TotalVolumes)
            .WithMessage(UserComicMessages.ReadVolumesLowerThanTotalVolumes);

        RuleFor(x => new { x.ReadChapters, x.LibraryComic.TotalChapters })
            .Must(x => x.ReadChapters >= 1)
            .WithMessage(UserComicMessages.ReadChaptersValidationMessage)
            .Must(x => x.ReadChapters <= x.TotalChapters)
            .WithMessage(UserComicMessages.ReadChaptersLowerThanTotalChapters);

        RuleFor(x => x.CollectionStatus)
            .NotEmpty()
            .WithMessage(UserComicMessages.CollectionStatusRequiredMessage)
            .Must(x => UserComicConstants.CollectingStatuses.Contains(x))
            .WithMessage(UserComicMessages.CollectionStatusValidationMessage);

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage(UserComicMessages.PriceValidationMessage);

        RuleFor(x => new { x.CollectedVolumes, x.LibraryComic.TotalVolumes })
            .NotEmpty()
            .WithMessage(UserComicMessages.CollectedVolumesRequiredMessage)
            .Must(x => x.CollectedVolumes <= x.TotalVolumes)
            .WithMessage(UserComicMessages.CollectedVolumesLowerThanTotalVolumes);


        RuleFor(x => x.LibraryComic)
            .SetValidator(new LibraryComicValidator());
    }
}