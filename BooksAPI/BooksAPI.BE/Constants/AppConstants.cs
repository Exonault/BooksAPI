namespace BooksAPI.BE.Constants;

public static class AppConstants
{
    public static readonly IReadOnlyList<string> ReadingStatuses = new []
    {
        "Reading",
        "Finished",
        "OnHold",
        "Dropped",
        "PlanToRead"
    };

    public static readonly IReadOnlyList<string> DemographicTypes = new[]
    {
        "Shonen",
        "Seinen",
        "Shojo",
        "Josei",
        "Kodomomuke"
    };

    public static readonly IReadOnlyList<string> ComicTypes = new[]
    {
        "Comic",
        "Manga",
        "LightNovel",
        "OneShot"
    };

    public static readonly IReadOnlyList<string> PublishingStatuses = new[]
    {
        "Publishing",
        "Finished",
        "OnHiatus"
    };

}