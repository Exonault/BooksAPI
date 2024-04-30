using System.Text.Json.Serialization;
using BooksAPI.FE.Contracts.LibraryManga;

namespace BooksAPI.FE.Contracts.UserManga;

public class UserMangaResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("readingStatus")]
    public string ReadingStatus { get; set; } = string.Empty;
    
    [JsonPropertyName("readVolumes")]
    public int ReadVolumes { get; set; }

    [JsonPropertyName("collectedVolumes")]
    public int CollectedVolumes { get; set; }
    
    [JsonPropertyName("pricePerVolume")]
    public decimal PricePerVolume { get; set; }

    [JsonPropertyName("collectionStatus")]
    public string CollectionStatus { get; set; } = string.Empty;

    [JsonPropertyName("userId")] 
    public string UserId { get; set; } = string.Empty;
    
    [JsonPropertyName("libraryComicInformation")]
    public LibraryMangaResponse LibraryMangaResponse { get; set; }
}