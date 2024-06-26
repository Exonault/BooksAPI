﻿using System.Text.Json.Serialization;

namespace BooksAPI.BE.Contracts.Statistics.UserManga;

public class MangaResponse
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("price")]
    public decimal Price { get; set; }
}