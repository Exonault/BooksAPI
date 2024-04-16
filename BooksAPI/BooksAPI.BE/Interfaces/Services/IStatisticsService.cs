﻿using BooksAPI.BE.Contracts.Statistics.Order;
using BooksAPI.BE.Contracts.Statistics.UserManga;

namespace BooksAPI.BE.Interfaces.Services;

public interface IStatisticsService
{
    
    //User mangas
    Task<List<UserMangaDemographicResponse>> GetUserMangaBreakdownByDemographic(string userId);

    Task<List<UserMangaTypeResponse>> GetUserMangaBreakdownByType(string userId);

    Task<List<UserMangaPublishingStatusResponse>> GetUserMangaBreakdownByPublishingStatus(string userId);
    Task<List<UserMangaReadingStatusReponse>> GetUserMangaBreakdownByReadingStatus(string userId);

    Task<List<UserMangaCollectionStatusResponse>> GetUserMangaBreakdownByCollectionStatus(string userId);

    Task<UserMangaTotalSpendingResponse> GetUserMangaBreakdownFromTotalSpending(string userId);
    
    
    
    //Orders
    Task<List<OrdersByYearResponse>> GetOrderBreakdownByYear(string userId);

    Task<List<OrdersForMonthByYearResponse>> GetOrderBreakdownForMonthsByYear(string userId, int year);
}