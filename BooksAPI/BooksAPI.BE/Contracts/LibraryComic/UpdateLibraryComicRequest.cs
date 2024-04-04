namespace BooksAPI.BE.Contracts.LibraryComic;

public class UpdateLibraryComicRequest
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string DemographicType { get; set; } = string.Empty;
    public string ComicType { get; set; } = string.Empty;
    public string PublishingStatus { get; set; } = string.Empty;
    public int TotalVolumes { get; set; }
}