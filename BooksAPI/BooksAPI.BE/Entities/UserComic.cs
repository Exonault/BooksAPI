namespace BooksAPI.BE.Entities;

public class UserComic
{
    public Guid Id { get; set; }

    public string ReadingStatus { get; set; } = string.Empty;
    
    public int ReadVolumes { get; set; }

    public int ReadChapters { get; set; }

    public int CollectedVolumes { get; set; }
    
    public decimal Price { get; set; }

    public string CollectionStatus { get; set; } = string.Empty;

    public User User { get; set; }

    public LibraryComic LibraryComic { get; set; }
    
}