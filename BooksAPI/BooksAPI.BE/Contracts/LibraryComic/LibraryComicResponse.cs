using System.Text.Json.Serialization;

namespace BooksAPI.BE.Contracts.LibraryComic;

public class LibraryComicResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
    
    [JsonPropertyName("author")]
    public string Author { get; set; } = string.Empty;
    
    [JsonPropertyName("demographicType")]
    public string DemographicType { get; set; } = string.Empty;
    
    [JsonPropertyName("comicType")]
    public string ComicType { get; set; } = string.Empty;
    
    [JsonPropertyName("publishingStatus")]
    public string PublishingStatus { get; set; } = string.Empty;
    
    [JsonPropertyName("totalVolumes")]
    public int TotalVolumes { get; set; }
    
    [JsonPropertyName("totalChapters")]
    public int TotalChapters { get; set; }
}