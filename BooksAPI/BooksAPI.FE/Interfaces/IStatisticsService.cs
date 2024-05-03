using BooksAPI.FE.Contracts.Statistics.Order;
using BooksAPI.FE.Contracts.Statistics.UserManga;

namespace BooksAPI.FE.Interfaces;

public interface IStatisticsService
{
    
    //User mangas
    Task<List<UserMangaDemographicResponse>> GetDemographicStatistics(string token, string refreshToken, string userId);

    Task<List<UserMangaTypeResponse>> GetTypeStatistics(string token, string refreshToken, string userId);

    Task<List<UserMangaCollectionStatusResponse>> GetCollectionStatusStatistics(string token, string refreshToken,
        string userId);

    Task<List<UserMangaPublishingStatusResponse>> GetPublishingStatusStatistics(string token, string refreshToken,
        string userId);

    Task<List<UserMangaReadingStatusResponse>> GetReadingStatusStatistics(string token, string refreshToken,
        string userId);

    Task<UserMangaTotalSpendingResponse> GetTotalSpendingStatistics(string token, string refreshToken,
        string userId);

    Task<GeneralStatisticsResponse> GetGeneralStatisticsResponse(string token, string refreshToken, string userId);

    
    //Orders
    Task<List<OrderByPlaceResponse>> GetOrderByPlaceStatistics(string token, string refreshToken, string userId);

    Task<List<OrdersByYearResponse>> GetOrderByYear(string token, string refreshToken, string userId);

    Task<List<OrdersForMonthByYearResponse>> GetOrderForMonthByYearResponse(string token, string refreshToken,
        string userId, int year);
}