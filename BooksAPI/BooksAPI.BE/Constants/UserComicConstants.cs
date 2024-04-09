namespace BooksAPI.BE.Constants;

public class UserComicConstants
{
    public static class ReadingStatus
    {
        public const string Reading = "Reading";
        public const string Finished = "Finished";
        public const string OnHold = "OnHold";
        public const string Dropped = "Dropped";
        public const string PlanToRead = "PlanToRead";

        public static readonly IReadOnlyCollection<string> ReadingStatuses = new[]
        {
            Reading, Finished, OnHold, Dropped, PlanToRead
        };
    }
    

    public static class CollectingStatus
    {
        public const string Collected = "Collected";
        public const string InProgress = "InProgress";
        public const string PlanToCollect = "PlanToCollect";

        public static readonly IReadOnlyCollection<string> CollectingStatuses = new[]
        {
            Collected, InProgress, PlanToCollect,
        };
    }
}