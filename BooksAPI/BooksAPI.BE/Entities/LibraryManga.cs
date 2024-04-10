namespace BooksAPI.BE.Entities;

public class LibraryManga
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DemographicType { get; set; } = string.Empty;
    public string ComicType { get; set; } = string.Empty;
    public string PublishingStatus { get; set; } = string.Empty;
    public int? TotalVolumes { get; set; }
    public List<UserManga> UserMangas { get; set; }
    
    public List<Author> Authors { get; set; }
}