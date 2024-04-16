using System.Text.Json.Serialization;

namespace BooksAPI.FE.Contracts.Statistics.UserManga;

public class UserMangaDemographicResponse
{
    [JsonPropertyName("demographicType")]
    public string DemographicType { get; set; } = string.Empty;
    
    [JsonPropertyName("count")]
    public int Count { get; set; }
}