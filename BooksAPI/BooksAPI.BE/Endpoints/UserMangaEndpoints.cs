using BooksAPI.BE.Constants;
using BooksAPI.BE.Contracts.UserManga;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Exception;
using BooksAPI.BE.Interfaces.Repositories;
using BooksAPI.BE.Interfaces.Services;
using BooksAPI.BE.Repositories;
using BooksAPI.BE.Services;
using BooksAPI.BE.Util;
using BooksAPI.BE.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.BE.Endpoints;

public static class UserMangaEndpoints
{
    public static void MapUserMangaEndpoints(this WebApplication app)
    {
        app.MapPost("/userManga", CreateUserManga)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapGet("/userManga/{id:int}", GetUserMangaById)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .CacheOutput(x => x.Expire(TimeSpan.FromMinutes(5)).Tag(CacheConstants.UserMangaWithIdTag))
            .Produces(StatusCodes.Status200OK, typeof(UserMangaResponse), "application/json")
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapGet("/userManga/", GetAllUserMangasByUserId)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .Produces(StatusCodes.Status200OK, typeof(List<UserMangaResponse>), "application/json")
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapGet("/userMangas/all", GetAllUserManga)
            .RequireAuthorization(AppConstants.PolicyNames.AdminRolePolicyName)
            .Produces(StatusCodes.Status200OK, typeof(List<UserMangaResponse>), "application/json")
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapPut("/userManga/{id:int}", UpdateUserManga)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapDelete("/userManga/{id:int}", DeleteUserManga)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError);
    }

    public static void AddUserMangaServices(this IServiceCollection services)
    {
        services.AddScoped<IUserMangaRepository, UserMangaRepository>();
        services.AddScoped<IUserMangaService, UserMangaService>();
        services.AddScoped<IValidator<UserManga>, UserMangaValidator>();
    }

    private static async Task<IResult> CreateUserManga([FromBody] CreateUserMangaRequest request,
        IUserMangaService service, HttpContext httpContext)
    {
        try
        {
            int statusCode = UserValidationUtil.IsUserIdFromRequestValidWithAuthUser(httpContext, request.UserId);

            switch (statusCode)
            {
                case StatusCodes.Status401Unauthorized:
                    return Results.Unauthorized();
                case StatusCodes.Status403Forbidden:
                    return Results.Forbid();
                default:
                {
                    await service.CreateUserManga(request);
                    return Results.Ok();
                }
            }
        }
        catch (UserNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (ResourceNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (ValidationException ex)
        {
            return Results.BadRequest(ex.Message);
        }
        catch (System.Exception ex)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task<IResult> GetUserMangaById([FromRoute] int id, IUserMangaService service,
        HttpContext httpContext)
    {
        try
        {
            UserMangaResponse response = await service.GetUserManga(id);
            int statusCode = UserValidationUtil.IsUserIdFromRequestValidWithAuthUser(httpContext, response.UserId);

            switch (statusCode)
            {
                case StatusCodes.Status401Unauthorized:
                    return Results.Unauthorized();
                case StatusCodes.Status403Forbidden:
                    return Results.Forbid();
                default:
                {
                    return Results.Ok(response);
                }
            }
        }
        catch (ResourceNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (System.Exception ex)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    static async Task<IResult> GetAllUserManga(IUserMangaService service)
    {
        try
        {
            List<UserMangaResponse> userComicResponses = await service.GetAllUserMangas();
            return Results.Ok(userComicResponses);
        }
        catch (System.Exception ex)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    static async Task<IResult> GetAllUserMangasByUserId([FromQuery] string userId, IUserMangaService service,
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
                    List<UserMangaResponse> allUserComicsByUserId = await service.GetAllUserMangasByUserId(userId);
                    return Results.Ok(allUserComicsByUserId);
                }
            }
        }
        catch (UserNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (System.Exception ex)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    static async Task<IResult> UpdateUserManga([FromRoute] int id, [FromBody] UpdateUserMangaRequest request,
        IUserMangaService service, HttpContext httpContext)
    {
        try
        {
            int statusCode = UserValidationUtil.IsUserIdFromRequestValidWithAuthUser(httpContext, request.UserId);

            switch (statusCode)
            {
                case StatusCodes.Status401Unauthorized:
                    return Results.Unauthorized();
                case StatusCodes.Status403Forbidden:
                    return Results.Forbid();
                default:
                {
                    await service.UpdateUserManga(id, request);
                    return Results.Ok();
                }
            }
        }
        catch (UserNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (ResourceNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (ValidationException ex)
        {
            return Results.BadRequest(ex.Message);
        }
        catch (System.Exception ex)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    static async Task<IResult> DeleteUserManga([FromRoute] int id, [FromQuery] string userId,
        IUserMangaService service, HttpContext httpContext)
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
                    await service.DeleteUserManga(id, userId);
                    return Results.Ok();
                }
            }
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(ex.Message);
        }
        catch (UserNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (ResourceNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (System.Exception ex)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}