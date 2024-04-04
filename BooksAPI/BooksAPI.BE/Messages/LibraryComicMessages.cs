﻿using BooksAPI.BE.Constants;

namespace BooksAPI.BE.Messages;

public static class LibraryComicMessages
{
    public const string TitleValidationMessage = "Title is required.";

    public const string AuthorValidationMessage = "Author is required.";

    public const string DemographicTypeRequiredMessage = "Demographic type is required.";
    
    public static readonly string DemographicTypeMessage =
        $"Demographic type must be one of the following: {string.Join(", ", LibraryComicConstants.DemographicType.DemographicTypes)}";

    public const string ComicTypeRequiredMessage = "Comic type is required";
    
    public static readonly string ComicTypeValidationMessage =
        $"Comic type must be one of the following: " + String.Join(", ", LibraryComicConstants.ComicType.ComicTypes);

    public const string PublishingStatusRequiredMessage = "Publishing status is required.";

    public static readonly string PublishingStatusValidationMessage =
        $"Publishing status must be one of the following: {string.Join(", ", LibraryComicConstants.PublishingType.PublishingStatuses)}";

    public const string NoLibraryComicWithId = "Library comic with id doesn't exist.";

    public const string TotalVolumesRequired =
        "Total volumes are required when the publishing status is Finished or OnHiatus.";
}