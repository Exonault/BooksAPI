using System.Text.Json.Serialization;

namespace BooksAPI.FE.Contracts.Statistics.UserManga;

public class UserMangaTotalSpendingResponse
{
    [JsonPropertyName("totalSpending")]
    public decimal TotalSpending { get; set; }

    [JsonPropertyName("mangas")]
    public List<MangaResponse> Mangas { get; set; } = new ();
    
}