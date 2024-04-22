namespace BooksAPI.BE.Entities;

public class UserManga
{
    public int Id { get; set; }

    public string ReadingStatus { get; set; } = string.Empty;
    
    public int ReadVolumes { get; set; }

    public int CollectedVolumes { get; set; }
    
    public decimal PricePerVolume { get; set; }

    public string CollectionStatus { get; set; } = string.Empty;

    public User User { get; set; }

    public LibraryManga LibraryManga { get; set; }
    
}