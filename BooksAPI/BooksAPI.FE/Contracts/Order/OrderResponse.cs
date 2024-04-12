﻿using System.Text.Json.Serialization;

namespace BooksAPI.FE.Contracts.Order;

public class OrderResponse
{
    [JsonPropertyName("date")]
    public DateOnly Date { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
    
    [JsonPropertyName("place")]
    public string Place { get; set; } = string.Empty;
    
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
    
    [JsonPropertyName("numberOfItems")]
    public int NumberOfItems { get; set; }

    [JsonPropertyName("userId")]
    public string UserId { get; set; } = string.Empty;
}