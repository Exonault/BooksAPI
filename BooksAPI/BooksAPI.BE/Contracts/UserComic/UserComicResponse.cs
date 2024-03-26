using System.Text.Json.Serialization;
using BooksAPI.BE.Contracts.LibraryComic;

namespace BooksAPI.BE.Contracts.UserComic;

public class UserComicResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("readingStatus")]
    public string ReadingStatus { get; set; } = string.Empty;
    
    [JsonPropertyName("readVolumes")]
    public int ReadVolumes { get; set; }

    [JsonPropertyName("readChapters")]
    public int ReadChapters { get; set; }

    [JsonPropertyName("collectedVolumes")]
    public int CollectedVolumes { get; set; }
    
    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("collectionStatus")]
    public string CollectionStatus { get; set; } = string.Empty;

    [JsonPropertyName("userId")] 
    public string UserId { get; set; } = string.Empty;
    
    [JsonPropertyName("libraryComicInformation")]
    public LibraryComicResponse LibraryComicResponse { get; set; }
}