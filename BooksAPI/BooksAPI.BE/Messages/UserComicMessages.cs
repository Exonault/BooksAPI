using BooksAPI.BE.Constants;

namespace BooksAPI.BE.Messages;

public static class UserComicMessages
{
    public const string ReadVolumesRequiredMessage = "Read volumes is required and must be at least 1.";

    public const string ReadVolumesLowerThanTotalVolumes =
        "Read volumes must be less than or equal to the Total volumes";

    public const string ReadChaptersValidationMessage = "Read chapters is required and must be at least 1.";

    public const string ReadChaptersLowerThanTotalChapters =
        "Read chapters must be less than or equal to the Total chapters.";

    public const string ReadingStatusRequiredMessage = "Reading status is required.";

    public static readonly string ReadingStatusValidationMessage =
        $"Reading status must be one of the following: " + String.Join(", ", UserComicConstants.ReadingStatuses);

    public const string CollectionStatusRequiredMessage = "Collection status is required.";

    public static readonly string CollectionStatusValidationMessage =
        $"Collection status must be one of the following: " + String.Join(", ", UserComicConstants.CollectingStatuses);

    public const string CollectedVolumesRequiredMessage = "Collected volumes is requred";

    public const string CollectedVolumesLowerThanTotalVolumes =
        "Collected volumes must be less than or equal to the Total volumes";

    public const string PriceValidationMessage = "Price is required and must be greater than 0";

    public const string NoUserComicWithId = "No user comic with id";

    public const string DeleteImpossible = "You can't delete this user comic";

}