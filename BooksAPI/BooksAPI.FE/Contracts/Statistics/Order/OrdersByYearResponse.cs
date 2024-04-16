using System.Text.Json.Serialization;

namespace BooksAPI.FE.Contracts.Statistics.Order;

public class OrdersByYearResponse
{
    [JsonPropertyName("year")]
    public int Year { get; set; }

    [JsonPropertyName("items")]
    public int Items { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }
}