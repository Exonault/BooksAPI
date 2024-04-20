using BooksAPI.BE.Constants;
using BooksAPI.BE.Contracts.Statistics.Order;
using BooksAPI.BE.Contracts.Statistics.UserManga;
using BooksAPI.BE.Exception;
using BooksAPI.BE.Interfaces.Services;
using BooksAPI.BE.Services;
using BooksAPI.BE.Util;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.BE.Endpoints;

public static class StatisticsEndpoints
{
    public static void MapStatisticsEndpoints(this WebApplication app)
    {
        app.MapGet("statistic/userManga/demographic/{userId}", GetUserMangaBreakdownByDemographic)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapGet("statistic/userManga/type/{userId}", GetUserMangaBreakdownByType)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapGet("statistic/userManga/publishingStatus/{userId}", GetUserMangaBreakdownByPublishingStatus)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapGet("statistic/userManga/readingStatus/{userId}", GetUserMangaBreakdownByReadingStatus)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapGet("statistic/userManga/collectionStatus/{userId}", GetUserMangaBreakdownByCollectionStatus)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapGet("statistic/userManga/totalSpending/{userId}", GetUserMangaBreakdownFromTotalSpending)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapGet("statistic/order/year/{userId}", GetOrderBreakdownByYear)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapGet("statistic/order/monthsByYear/{userId}", GetOrderBreakdownForMonthsByYear)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }


    public static void AddStatisticsServices(this IServiceCollection services)
    {
        services.AddScoped<IStatisticsService, StatisticsService>();
    }

    private static async Task<IResult> GetUserMangaBreakdownByDemographic([FromRoute] string userId,
        IStatisticsService service,
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
        catch (UserNotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
        catch (System.Exception e)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task<IResult> GetUserMangaBreakdownByType([FromRoute] string userId,
        IStatisticsService service,
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
        catch (UserNotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
        catch (System.Exception e)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task<IResult> GetUserMangaBreakdownByPublishingStatus([FromRoute] string userId,
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
        catch (UserNotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
        catch (System.Exception e)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task<IResult> GetUserMangaBreakdownByReadingStatus([FromRoute] string userId,
        IStatisticsService service,
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
                    List<UserMangaReadingStatusResponse> responses =
                        await service.GetUserMangaBreakdownByReadingStatus(userId);
                    return Results.Ok(responses);
                }
            }
        }
        catch (UserNotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
        catch (System.Exception e)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task<IResult> GetUserMangaBreakdownByCollectionStatus([FromRoute] string userId,
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
                    List<UserMangaCollectionStatusResponse> responses =
                        await service.GetUserMangaBreakdownByCollectionStatus(userId);
                    return Results.Ok(responses);
                }
            }
        }
        catch (UserNotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
        catch (System.Exception e)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task<IResult> GetUserMangaBreakdownFromTotalSpending([FromRoute] string userId,
        IStatisticsService service,
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
                    UserMangaTotalSpendingResponse response =
                        await service.GetUserMangaBreakdownFromTotalSpending(userId);
                    return Results.Ok(response);
                }
            }
        }
        catch (UserNotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
        catch (System.Exception e)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task<IResult> GetOrderBreakdownByYear([FromRoute] string userId, IStatisticsService service,
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
                    List<OrdersByYearResponse> responses =
                        await service.GetOrderBreakdownByYear(userId);
                    return Results.Ok(responses);
                }
            }
        }
        catch (UserNotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
        catch (System.Exception e)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task<IResult> GetOrderBreakdownForMonthsByYear([FromRoute] string userId, [FromQuery] int year,
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
                    List<OrdersForMonthByYearResponse> responses =
                        await service.GetOrderBreakdownForMonthsByYear(userId, year);
                    return Results.Ok(responses);
                }
            }
        }
        catch (UserNotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
        catch (System.Exception e)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}