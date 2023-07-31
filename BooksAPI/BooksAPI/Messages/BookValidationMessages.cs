using BooksAPI.Constants;

namespace BooksAPI.Messages;

public static class BookValidationMessages
{
    public const string AuthorValidationMessage = "Author name is required";

    public const string TitleValidationMessage = "Title is required";

    public const string PriceValidationMessage = "Price is required";
    
    public static readonly string ReadingStatusMessageMessage =
        $"Status must be one of the following: {string.Join(", ", AppConstants.ReadingStatuses)}";

}