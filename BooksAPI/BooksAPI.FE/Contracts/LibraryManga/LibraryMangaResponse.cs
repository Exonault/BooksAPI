﻿using System.Text.Json.Serialization;
using BooksAPI.FE.Contracts.Author;

namespace BooksAPI.FE.Contracts.LibraryManga;

public class LibraryMangaResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("titleRomaji")]
    public string TitleRomaji { get; set; } = string.Empty;
    
    [JsonPropertyName("titleEnglish")]
    public string? TitleEnglish { get; set; } = string.Empty; 
    
    [JsonPropertyName("titleJapanese")]
    public string TitleJapanese { get; set; } = string.Empty; 

    [JsonPropertyName("authors")]
    public List<AuthorResponse> Authors { get; set; } = new List<AuthorResponse>();
    
    [JsonPropertyName("demographicType")]
    public string DemographicType { get; set; } = string.Empty;
    
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
    
    [JsonPropertyName("publishingStatus")]
    public string PublishingStatus { get; set; } = string.Empty;
    
    [JsonPropertyName("synopsis")]
    public string Synopsis { get; set; }  = string.Empty;
    
    [JsonPropertyName("totalVolumes")]
    public int? TotalVolumes { get; set; }
    
    [JsonPropertyName("mainImageUrl")]
    public string? MainImageUrl { get; set; } = string.Empty;
}