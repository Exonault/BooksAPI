using BooksAPI.FE.Contracts.Author;

namespace BooksAPI.FE.Contracts.LibraryManga;

public class CreateLibraryMangaRequest
{
    public string TitleRomaji { get; set; } = string.Empty;
    public string? TitleEnglish { get; set; } = string.Empty;
    public string TitleJapanese { get; set; } = string.Empty;
    public string DemographicType { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string PublishingStatus { get; set; } = string.Empty;
    public string Synopsis { get; set; }  = string.Empty;
    public string? MainImageUrl { get; set; } = string.Empty;
    public int? TotalVolumes { get; set; }
    public List<AuthorRequest> Authors { get; set; } = new List<AuthorRequest>();
}