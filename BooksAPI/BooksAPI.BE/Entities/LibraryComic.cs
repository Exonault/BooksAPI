namespace BooksAPI.BE.Entities;

public class LibraryComic
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string DemographicType { get; set; } = string.Empty;
    public string ComicType { get; set; } = string.Empty;
    
    public string PublishingStatus { get; set; } = string.Empty;
    public int TotalVolumes { get; set; }
    public int TotalChapters { get; set; }

    public List<UserComic> UserComics { get; set; }
}