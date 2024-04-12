using System.Text.Json.Serialization;
using BooksAPI.FE.Contracts.Author;

namespace BooksAPI.FE.Contracts.LibraryComic;

public class LibraryMangaResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("authors")]
    public List<AuthorResponse> Authors { get; set; } = new List<AuthorResponse>();
    
    [JsonPropertyName("demographicType")]
    public string DemographicType { get; set; } = string.Empty;
    
    [JsonPropertyName("comicType")]
    public string ComicType { get; set; } = string.Empty;
    
    [JsonPropertyName("publishingStatus")]
    public string PublishingStatus { get; set; } = string.Empty;
    
    [JsonPropertyName("totalVolumes")]
    public int? TotalVolumes { get; set; }
}