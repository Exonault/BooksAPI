using BooksAPI.Constants;

namespace BooksAPI.Messages;

public static class ComicValidationMessages
{
    public const string TotalVolumeValidationMessage = "Total volumes is required and it must be at least 1";

    public const string CollectedVolumesValidationMessage = "Collected volumes is required";

    public const string CollectedVolumesLessThanTotalVolumes =
        "Collected volumes must be less than or equal to the total volumes";

    public static readonly string DemographicTypesMessage =
        $"Demographic type must be one of the following: {string.Join(", ", AppConstants.DemographicTypes)}";

    public static readonly string ComicTypeValidationMessage =
        $"Comic type must be one of the following: " + String.Join(", ", AppConstants.ComicTypes);

    public static readonly string PublishingStatusValidationMessage =
        $"Publishing status must be one of the following: {string.Join(", ", AppConstants.PublishingStatuses)}";
}