using System.Text.Json.Serialization;

namespace BookAPI.Presentation.Data;

public class ComicResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("author")]
    public string Author { get; set; } = string.Empty;

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("readingStatus")]
    public string ReadingStatus { get; set; } = string.Empty;

    [JsonPropertyName("demographicType")]
    public string DemographicType { get; set; } = string.Empty;

    [JsonPropertyName("comicType")]
    public string ComicType { get; set; } = string.Empty;

    [JsonPropertyName("publishingStatus")]
    public string PublishingStatus { get; set; } = string.Empty;

    [JsonPropertyName("totalVolumes")]
    public int TotalVolumes { get; set; }

    [JsonPropertyName("collectedVolumes")]
    public int CollectedVolumes { get; set; }
}