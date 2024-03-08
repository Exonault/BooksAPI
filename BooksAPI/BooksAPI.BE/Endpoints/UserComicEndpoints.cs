using BooksAPI.BE.Constants;
using BooksAPI.BE.Contracts.UserComic;
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

public static class UserComicEndpoints
{
    public static void MapUserComicEndpoints(this WebApplication app)
    {
        app.MapPost("/userComic", CreateUserComic)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapGet("/userComic/{id:guid}", GetUserComicById)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .Produces(StatusCodes.Status200OK, typeof(UserComicResponse), "application/json")
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapGet("/userComics/", GetAllUserComics)
            .RequireAuthorization(AppConstants.PolicyNames.AdminRolePolicyName)
            .Produces(StatusCodes.Status200OK, typeof(List<UserComicResponse>), "application/json")
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapGet("/userComic/", GetAllUserComicsByUserId)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .Produces(StatusCodes.Status200OK, typeof(List<UserComicResponse>), "application/json")
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapPut("/userComic/{id:guid}", UpdateUserComic)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapDelete("/userComic/{id:guid}", DeleteUserComic)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError);
    }

    public static void AddUserComicServices(this IServiceCollection services)
    {
        services.AddScoped<IUserComicRepository, UserComicRepository>();
        services.AddScoped<IUserComicService, UserComicService>();
        services.AddScoped<IValidator<UserComic>, UserComicValidator>();
    }

    private static async Task<IResult> CreateUserComic([FromBody] CreateUserComicRequest request,
        IUserComicService service, HttpContext httpContext)
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
                    await service.CreateUserComic(request);
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

    private static async Task<IResult> GetUserComicById([FromRoute] Guid id, IUserComicService service,
        HttpContext httpContext)
    {
        try
        {
            UserComicResponse response = await service.GetUserComic(id);
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

    static async Task<IResult> GetAllUserComics(IUserComicService service)
    {
        try
        {
            List<UserComicResponse> userComicResponses = await service.GetAllUserComics();
            return Results.Ok(userComicResponses);
        }
        catch (System.Exception ex)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    static async Task<IResult> GetAllUserComicsByUserId([FromQuery] string userId, IUserComicService service,
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
                    List<UserComicResponse> allUserComicsByUserId = await service.GetAllUserComicsByUserId(userId);
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

    static async Task<IResult> UpdateUserComic([FromRoute] Guid id, [FromBody] UpdateUserComicRequest request,
        IUserComicService service, HttpContext httpContext)
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
                    await service.UpdateUserComic(id, request);
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

    static async Task<IResult> DeleteUserComic([FromRoute] Guid id, [FromQuery] string userId,
        IUserComicService service, HttpContext httpContext)
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
                    await service.DeleteUserComic(id, userId);
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