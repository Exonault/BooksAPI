using BooksAPI.BE.Contracts.Author;

namespace BooksAPI.BE.Contracts.LibraryManga;

public class UpdateLibraryMangaRequest
{
    public string Title { get; set; } = string.Empty;
    public string DemographicType { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string PublishingStatus { get; set; } = string.Empty;
    public int? TotalVolumes { get; set; }
    
    public string? MainImageUrl { get; set; } = string.Empty;
    public List<AuthorRequest> Authors { get; set; } = new List<AuthorRequest>();
}