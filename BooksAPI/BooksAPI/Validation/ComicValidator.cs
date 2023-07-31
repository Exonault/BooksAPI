using BooksAPI.Constants;
using BooksAPI.Entities;
using BooksAPI.Messages;
using FluentValidation;

namespace BooksAPI.Validation;

public class ComicValidator : AbstractValidator<Comic>
{
    public ComicValidator()
    {
        Include(new BookValidator());

        RuleFor(x => x.DemographicType)
            .NotEmpty()
            .WithMessage("Demographic type is required")
            .Must(x => AppConstants.DemographicTypes.Contains(x))
            .WithMessage(ComicValidationMessages.DemographicTypesMessage);

        RuleFor(x => x.ComicType)
            .NotEmpty()
            .WithMessage("Comic type is required")
            .Must(x => AppConstants.ComicTypes.Contains(x))
            .WithMessage(ComicValidationMessages.ComicTypeValidationMessage);

        RuleFor(x => x.PublishingStatus)
            .NotEmpty()
            .WithMessage("Publishing status is required")
            .Must(x => AppConstants.PublishingStatuses.Contains(x))
            .WithMessage(ComicValidationMessages.PublishingStatusValidationMessage);

        RuleFor(x => x.TotalVolumes)
            .Must(x => x >= 1)
            .WithMessage(ComicValidationMessages.TotalVolumeValidationMessage);

        RuleFor(x => x.CollectedVolumes)
            .NotEmpty()
            .WithMessage(ComicValidationMessages.CollectedVolumesValidationMessage);
    }
}