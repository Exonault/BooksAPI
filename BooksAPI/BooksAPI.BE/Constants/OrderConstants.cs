namespace BooksAPI.BE.Constants;

public static class OrderConstants
{
    public static class Status
    {
        public const string Created = "Created";

        public const string OnTheWay = "OnTheWay";

        public const string Delivered = "Delivered";

        public static readonly IReadOnlyList<string> OrderStatuses = new[]
        {
            Created,
            OnTheWay,
            Delivered
        };
    }
}