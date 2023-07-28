namespace BooksAPI.Entities;

public class Book
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string ReadingStatus { get; set; } = string.Empty;

    public Order Order { get; set; } = default!;
}