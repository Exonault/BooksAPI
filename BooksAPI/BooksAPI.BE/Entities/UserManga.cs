namespace BooksAPI.BE.Entities;

public class UserManga
{
    public Guid Id { get; set; }

    public string ReadingStatus { get; set; } = string.Empty;
    
    public int ReadVolumes { get; set; }

    public int CollectedVolumes { get; set; }
    
    public decimal Price { get; set; }

    public string CollectionStatus { get; set; } = string.Empty;

    public User User { get; set; }

    public LibraryManga LibraryManga { get; set; }
    
}