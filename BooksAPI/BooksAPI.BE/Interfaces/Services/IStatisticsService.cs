using BooksAPI.BE.Contracts.Statistics.Order;
using BooksAPI.BE.Contracts.Statistics.UserManga;

namespace BooksAPI.BE.Interfaces.Services;

public interface IStatisticsService
{
    
    //User mangas
    Task<List<UserMangaDemographicResponse>> GetUserMangaBreakdownByDemographic(string userId);

    Task<List<UserMangaTypeResponse>> GetUserMangaBreakdownByType(string userId);

    Task<List<UserMangaPublishingStatusResponse>> GetUserMangaBreakdownByPublishingStatus(string userId);
    Task<List<UserMangaReadingStatusResponse>> GetUserMangaBreakdownByReadingStatus(string userId);

    Task<List<UserMangaCollectionStatusResponse>> GetUserMangaBreakdownByCollectionStatus(string userId);

    Task<UserMangaTotalSpendingResponse> GetUserMangaBreakdownFromTotalSpending(string userId);

    Task<GeneralStatisticsResponse> GetGeneralStatistics(string userId);
    
    //Orders
    Task<List<OrdersByYearResponse>> GetOrderBreakdownByYear(string userId);

    Task<List<OrdersForMonthByYearResponse>> GetOrderBreakdownForMonthsByYear(string userId, int year);

    Task<List<OrderByPlaceResponse>> GetOrderBreakdownByPlace(string userId);

}