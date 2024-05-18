using BooksAPI.BE.Constants;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Messages;
using FluentValidation;

namespace BooksAPI.BE.Validation;

public class UserMangaValidator : AbstractValidator<UserManga>
{
    public UserMangaValidator()
    {
        RuleFor(x => x.ReadingStatus)
            .NotEmpty()
            .WithMessage(UserMangaMessages.ReadingStatusRequiredMessage)
            .Must(x => UserMangaConstants.ReadingStatus.ReadingStatuses.Contains(x))
            .WithMessage(UserMangaMessages.ReadingStatusValidationMessage);

        RuleFor(x => new { x.ReadVolumes, x.CollectedVolumes })
            .Must(x => x.ReadVolumes >= 0)
            .WithMessage(UserMangaMessages.ReadVolumesRequiredMessage)
            .Must(x => x.ReadVolumes <= x.CollectedVolumes)
            .WithMessage(UserMangaMessages.ReadVolumesLowerThanTotalVolumes);

        RuleFor(x => new { x.CollectionStatus, x.LibraryManga.PublishingStatus })
            .NotEmpty()
            .WithMessage(UserMangaMessages.CollectionStatusRequiredMessage)
            .Must(x => UserMangaConstants.CollectingStatus.CollectingStatuses.Contains(x.CollectionStatus))
            .WithMessage(UserMangaMessages.CollectionStatusValidationMessage);

        RuleFor(x => x.PricePerVolume)
            .GreaterThanOrEqualTo(0)
            .WithMessage(UserMangaMessages.PricePerVolumeValidationMessage);

        RuleFor(x => x.CollectedVolumes)
            .GreaterThanOrEqualTo(0)
            .WithMessage(UserMangaMessages.CollectedVolumesRequiredMessage);

        RuleFor(x => new { x.CollectionStatus, x.LibraryManga.PublishingStatus })
            .Custom((obj, context) =>
            {
                if (obj.CollectionStatus == UserMangaConstants.CollectingStatus.Collected &&
                    obj.PublishingStatus == LibraryMangaConstants.PublishingType.Publishing)
                {
                    context.AddFailure("CollectionStatus", UserMangaMessages.CollectionStatusMessage);
                }
            });

        RuleFor(x => x.LibraryManga)
            .SetValidator(new LibraryMangaValidator());
    }
}