using System.Text.Json.Serialization;

namespace BooksAPI.FE.Contracts.Statistics.UserManga;

public class MangaResponse
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("price")]
    public decimal Price { get; set; }
}