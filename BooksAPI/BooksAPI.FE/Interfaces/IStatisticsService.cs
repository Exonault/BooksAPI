using BooksAPI.FE.Contracts.Statistics.UserManga;
using MudBlazor;

namespace BooksAPI.FE.Interfaces;

public interface IStatisticsService
{
    Task<List<UserMangaDemographicResponse>> GetDemographicStatistics(string token, string refreshToken, string userId);
}