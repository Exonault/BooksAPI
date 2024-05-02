using System.Text.Json.Serialization;

namespace BooksAPI.FE.Contracts.Statistics.Order;

public class OrderByPlaceResponse
{
    [JsonPropertyName("place")]
    public string Place { get; set; }

    [JsonPropertyName("totalOrders")]
    public int TotalOrders { get; set; }
    
    [JsonPropertyName("totalValueOfOrders")]
    public decimal TotalValueOfOrders { get; set; }
    
}