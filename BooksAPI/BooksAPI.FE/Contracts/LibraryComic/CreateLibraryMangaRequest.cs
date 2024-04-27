using BooksAPI.FE.Contracts.Author;

namespace BooksAPI.FE.Contracts.LibraryComic;

public class CreateLibraryMangaRequest
{
    public string Title { get; set; } = string.Empty;
    public string DemographicType { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string PublishingStatus { get; set; } = string.Empty;
    public string? MainImageUrl { get; set; } = string.Empty;
    public int? TotalVolumes { get; set; }
    public List<AuthorRequest> Authors { get; set; } = new List<AuthorRequest>();
}