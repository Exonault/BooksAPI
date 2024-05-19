namespace BooksAPI.BE.Exception;

public class UserMangaAlreadyExistsException:System.Exception
{
    public UserMangaAlreadyExistsException()
    {
    }

    public UserMangaAlreadyExistsException(string? message) : base(message)
    {
    }

    public UserMangaAlreadyExistsException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}