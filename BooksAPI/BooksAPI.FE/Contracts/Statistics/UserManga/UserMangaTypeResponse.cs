using System.Text.Json.Serialization;

namespace BooksAPI.FE.Contracts.Statistics.UserManga;

public class UserMangaTypeResponse
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("count")]
    public int Count { get; set; }
}