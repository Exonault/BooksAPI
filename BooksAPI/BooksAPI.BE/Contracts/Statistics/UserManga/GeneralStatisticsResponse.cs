using System.Text.Json.Serialization;

namespace BooksAPI.BE.Contracts.Statistics.UserManga;

public class GeneralStatisticsResponse
{
    [JsonPropertyName("totalSpending")]
    public decimal TotalSpending { get; set; }

    [JsonPropertyName("totalCollectedVolumes")]
    public int TotalCollectedVolumes { get; set; }

    [JsonPropertyName("totalReadVolumes")]
    public int TotalReadVolumes { get; set; }
    
}