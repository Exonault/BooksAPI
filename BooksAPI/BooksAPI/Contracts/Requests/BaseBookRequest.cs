namespace BooksAPI.Contracts.Requests;

public class BaseBookRequest
{
    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string ReadingStatus { get; set; } = string.Empty;
}