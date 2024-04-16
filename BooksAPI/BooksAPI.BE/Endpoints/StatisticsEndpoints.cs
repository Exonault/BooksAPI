using BooksAPI.BE.Contracts.Statistics.UserManga;
using BooksAPI.BE.Interfaces.Services;
using BooksAPI.BE.Services;
using BooksAPI.BE.Util;

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

    private static async Task<IResult> GetUserMangaBreakdownByDemographic(string userId, IStatisticsService service,
        HttpContext httpContext)
    {
        try
        {
            int statusCode = UserValidationUtil.IsUserIdFromRequestValidWithAuthUser(httpContext, userId);
            switch (statusCode)
            {
                case StatusCodes.Status401Unauthorized:
                    return Results.Unauthorized();
                case StatusCodes.Status403Forbidden:
                    return Results.Forbid();
                default:
                {
                    List<UserMangaDemographicResponse> responses =
                        await service.GetUserMangaBreakdownByDemographic(userId);
                    return Results.Ok(responses);
                }
            }
        }
        catch (System.Exception e)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task<IResult> GetUserMangaBreakdownByType(string userId, IStatisticsService service,
        HttpContext httpContext)
    {
        try
        {
            int statusCode = UserValidationUtil.IsUserIdFromRequestValidWithAuthUser(httpContext, userId);
            switch (statusCode)
            {
                case StatusCodes.Status401Unauthorized:
                    return Results.Unauthorized();
                case StatusCodes.Status403Forbidden:
                    return Results.Forbid();
                default:
                {
                    List<UserMangaTypeResponse> responses =
                        await service.GetUserMangaBreakdownByType(userId);
                    return Results.Ok(responses);
                }
            }
        }
        catch (System.Exception e)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task<IResult> GetUserMangaBreakdownByPublishingStatus(string userId,
        IStatisticsService service, HttpContext httpContext)
    {
        try
        {
            int statusCode = UserValidationUtil.IsUserIdFromRequestValidWithAuthUser(httpContext, userId);
            switch (statusCode)
            {
                case StatusCodes.Status401Unauthorized:
                    return Results.Unauthorized();
                case StatusCodes.Status403Forbidden:
                    return Results.Forbid();
                default:
                {
                    List<UserMangaPublishingStatusResponse> responses =
                        await service.GetUserMangaBreakdownByPublishingStatus(userId);
                    return Results.Ok(responses);
                }
            }
        }
        catch (System.Exception e)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
        return Results.Ok();
    }

    private static async Task<IResult> GetUserMangaBreakdownByReadingStatus(string userId, IStatisticsService service,
        HttpContext httpContext)
    {
        return Results.Ok();
    }

    private static async Task<IResult> GetUserMangaBreakdownByCollectionStatus(string userId,
        IStatisticsService service, HttpContext httpContext)
    {
        return Results.Ok();
    }

    private static async Task<IResult> GetUserMangaBreakdownFromTotalSpending(string userId, IStatisticsService service,
        HttpContext httpContext)
    {
        return Results.Ok();
    }

    private static async Task<IResult> GetOrderBreakdownByYear(string userId, IStatisticsService service,
        HttpContext httpContext)
    {
        return Results.Ok();
    }

    private static async Task<IResult> GetOrderBreakdownForMonthsByYear(string userId, int year,
        IStatisticsService service, HttpContext httpContext)
    {
        return Results.Ok();
    }
}