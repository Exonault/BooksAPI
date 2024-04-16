namespace BooksAPI.BE.Contracts.Statistics.UserManga;

public class UserMangaTotalSpendingResponse
{
    public decimal TotalSpending { get; set; }

    public List<MangaResponse> Mangas { get; set; }
    
}