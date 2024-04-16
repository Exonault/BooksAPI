﻿using System.Text.Json.Serialization;

namespace BooksAPI.BE.Contracts.Statistics.UserManga;

public class UserMangaTotalSpendingResponse
{
    [JsonPropertyName("totalSpending")]
    public decimal TotalSpending { get; set; }

    [JsonPropertyName("mangas")]
    public List<MangaResponse> Mangas { get; set; } = new ();
    
}