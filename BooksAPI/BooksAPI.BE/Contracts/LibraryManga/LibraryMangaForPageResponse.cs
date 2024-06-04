using System.Text.Json.Serialization;
using BooksAPI.BE.Contracts.Author;

namespace BooksAPI.BE.Contracts.LibraryManga;

public class LibraryMangaForPageResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("titleRomaji")]
    public string TitleRomaji { get; set; } = string.Empty;
    
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("authors")]
    public List<AuthorResponse> Authors { get; set; } = new List<AuthorResponse>();
    
    [JsonPropertyName("mainImageUrl")]
    public string? MainImageUrl { get; set; } = string.Empty;
    
    
}