using System.Text.Json.Serialization;
using BooksAPI.BE.Contracts.Author;

namespace BooksAPI.BE.Contracts.LibraryComic;

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
    
    [JsonPropertyName("Type")]
    public string Type { get; set; } = string.Empty;
    
    [JsonPropertyName("publishingStatus")]
    public string PublishingStatus { get; set; } = string.Empty;
    
    [JsonPropertyName("totalVolumes")]
    public int? TotalVolumes { get; set; }
}