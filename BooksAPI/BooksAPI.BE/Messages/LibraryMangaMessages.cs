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
    
    public static readonly string ComicTypeValidationMessage =
        $"Comic type must be one of the following: " + String.Join(", ", LibraryMangaConstants.Type.ComicTypes);

    public const string PublishingStatusRequired = "Publishing status is required.";

    public static readonly string PublishingStatusValidationMessage =
        $"Publishing status must be one of the following: {string.Join(", ", LibraryMangaConstants.PublishingType.PublishingStatuses)}";

    public const string NoLibraryComicWithId = "Library manga with id doesn't exist.";

    public const string TotalVolumesValidationMessage =
        "Total volumes are required when the publishing status is Finished or OnHiatus.";

    public const string SynopsisRequired = "Synopsis is required";
}