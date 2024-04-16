using System.Text.Json.Serialization;

namespace BooksAPI.FE.Contracts.Statistics.UserManga;

public class UserMangaReadingStatusResponse
{
    [JsonPropertyName("readingStatus")]
    public string ReadingStatus { get; set; } = string.Empty;

    [JsonPropertyName("count")]
    public int Count { get; set; }
}