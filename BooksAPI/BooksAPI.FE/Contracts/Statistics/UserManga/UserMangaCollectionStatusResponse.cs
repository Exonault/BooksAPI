using System.Text.Json.Serialization;

namespace BooksAPI.FE.Contracts.Statistics.UserManga;

public class UserMangaCollectionStatusResponse
{
    [JsonPropertyName("collectionStatus")]
    public string CollectionStatus { get; set; } = string.Empty;

    [JsonPropertyName("count")]
    public int Count { get; set; }
}