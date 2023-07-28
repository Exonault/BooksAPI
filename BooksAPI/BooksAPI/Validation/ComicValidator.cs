using BooksAPI.Constants;
using BooksAPI.Entities;
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
            .WithMessage("Demographic type must be one of the following: " +
                         String.Join(",", AppConstants.DemographicTypes));
        
        RuleFor(x => x.ComicType) 
            .NotEmpty()
            .WithMessage("Comic type is required")
            .Must(x => AppConstants.ComicTypes.Contains(x))
            .WithMessage("Comic type must be one of the following: " +
                         String.Join(",", AppConstants.ComicTypes));
        
        RuleFor(x => x.PublishingStatus) 
            .NotEmpty()
            .WithMessage("Publishing status is required")
            .Must(x => AppConstants.PublishingStatuses.Contains(x))
            .WithMessage("Publishing status must be one of the following: " +
                         String.Join(",", AppConstants.PublishingStatuses));

        RuleFor(x => x.TotalVolumes)
            .NotEmpty()
            .WithMessage("Total volumes is required");
        
        RuleFor(x => x.CollectedVolumes)
            .NotEmpty()
            .WithMessage("Collected volumes is required");


    }
}