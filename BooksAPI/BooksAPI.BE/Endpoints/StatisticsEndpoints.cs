using BooksAPI.BE.Interfaces.Services;
using BooksAPI.BE.Services;

namespace BooksAPI.BE.Endpoints;

public static class StatisticsEndpoints
{

    public static void MapStatisticsEndpoints(this WebApplication app)
    {
        
    }


    public static void AddStatisticsServices(this IServiceCollection services)
    {
        services.AddScoped<IStatisticsService, StatisticsService>();
    }
    
    
}