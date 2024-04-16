using System.Text.Json.Serialization;

namespace BooksAPI.FE.Contracts.Statistics.UserManga;

public class UserMangaPublishingStatusResponse
{
    [JsonPropertyName("publishingStatus")]
    public string PublishingStatus { get; set; } = string.Empty;
    
    [JsonPropertyName("count")]
    public int Count { get; set; }
    
}