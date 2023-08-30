using System.Text.Json.Serialization;

namespace BookAPI.Presentation.Contracts.Response.Order;

public class GetOrderResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
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
}