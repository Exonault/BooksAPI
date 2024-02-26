namespace BooksAPI.BE.Constants;

public class UserComicConstants
{
    public static readonly IReadOnlyList<string> ReadingStatuses = new []
    {
        "Reading",
        "Finished",
        "OnHold",
        "Dropped",
        "PlanToRead"
    };

    public static readonly IReadOnlyList<string> CollectingStatuses = new[]
    {
        "Collected",
        "InProgress",
        "PlanToCollect"
    };
}