using System.Text.Json.Serialization;
using BooksAPI.FE.Contracts.Author;

namespace BooksAPI.FE.Contracts.LibraryComic;

public class LibraryMangaResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("authors")]
    public List<AuthorResponse> Authors { get; set; } = new List<AuthorResponse>();
    
    [JsonPropertyName("demographicType")]
    public string DemographicType { get; set; } = string.Empty;
    
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
    
    [JsonPropertyName("publishingStatus")]
    public string PublishingStatus { get; set; } = string.Empty;
    
    [JsonPropertyName("totalVolumes")]
    public int? TotalVolumes { get; set; }
    
    [JsonPropertyName("mainImageUrl")]
    public string? MainImageUrl { get; set; } = string.Empty;
}