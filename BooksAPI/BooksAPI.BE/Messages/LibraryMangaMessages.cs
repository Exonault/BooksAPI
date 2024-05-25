using BooksAPI.BE.Constants;

namespace BooksAPI.BE.Messages;

public static class LibraryMangaMessages
{
    public const string TitleRomajiRequired = "Romaji title is required.";
    public const string TitleEnglishRequired = "English title is required.";
    public const string TitleJapaneseRequired = "Japanese title is required.";

    public const string AuthorValidationMessage = "Author is required.";

    public const string DemographicTypeRequired = "Demographic type is required.";
    
    public static readonly string DemographicTypeMessage =
        $"Demographic type must be one of the following: {string.Join(", ", LibraryMangaConstants.DemographicType.DemographicTypes)}";

    public const string TypeRequiredMessage = "Type is required";
    
    public static readonly string TypeValidationMessage =
        $"Type must be one of the following: " + String.Join(", ", LibraryMangaConstants.Type.Types);

    public const string PublishingStatusRequired = "Publishing status is required.";

    public static readonly string PublishingStatusValidationMessage =
        $"Publishing status must be one of the following: {string.Join(", ", LibraryMangaConstants.PublishingType.PublishingStatuses)}";

    public const string NoLibraryComicWithId = "Library manga with id doesn't exist.";

    public const string TotalVolumesValidationMessage =
        "Total volumes are required when the publishing status is Finished or OnHiatus.";

    public const string TotalVolumesOneShotValidationMessage = "Total volumes must be 1 when the type is OneShot";

    public const string PublishingStatusOneShotValidationMessage =
        "Publishing status must be Finished if the type is OneShot";

    public const string SynopsisRequired = "Synopsis is required";
}