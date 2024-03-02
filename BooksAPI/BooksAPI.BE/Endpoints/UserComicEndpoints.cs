using System.Security.Claims;
using BooksAPI.BE.Constants;
using BooksAPI.BE.Contracts.UserComic;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Exception;
using BooksAPI.BE.Interfaces.Repositories;
using BooksAPI.BE.Interfaces.Services;
using BooksAPI.BE.Repositories;
using BooksAPI.BE.Services;
using BooksAPI.BE.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.BE.Endpoints;

public static class UserComicEndpoints
{
    public static void MapUserComicEndpoints(this WebApplication app)
    {
        app.MapPost("/userComic", CreateUserComic).RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName);

        app.MapGet("/userComic/{id:guid}", GetUserComicById)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName);
        app.MapGet("/userComic/", GetAllUserComics).RequireAuthorization(AppConstants.PolicyNames.AdminRolePolicyName);
        app.MapGet("/userComics/", GetAllUserComicsByUserId)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName);

        app.MapPut("/userComic/", UpdateUserComic).RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName);

        app.MapDelete("/userComic/", DeleteUserComic).RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName);
    }

    public static void AddUserComicServices(this IServiceCollection services)
    {
        services.AddScoped<IUserComicRepository, UserComicRepository>();
        services.AddScoped<IUserComicService, UserComicService>();
        services.AddScoped<IValidator<UserComic>, UserComicValidator>();
        services.AddHttpContextAccessor();
    }

    static async Task<IResult> CreateUserComic(IUserComicService service, [FromBody] CreateUserComicRequest request)
    {
        try
        {
            await service.CreateUserComic(request);
            return Results.Ok();
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

    static async Task<IResult> GetUserComicById(IUserComicService service, [FromRoute] Guid id)
    {
        try
        {
            UserComicResponse userComicResponse = await service.GetUserComic(id);
            return Results.Ok(userComicResponse);
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

    static async Task<IResult> GetAllUserComicsByUserId(IUserComicService service, [FromQuery] string userId)
    {
        try
        {
            List<UserComicResponse> allUserComicsByUserId = await service.GetAllUserComicsByUserId(userId);
            return Results.Ok(allUserComicsByUserId);
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

    static async Task<IResult> UpdateUserComic(IUserComicService service, [FromQuery] Guid id,
        [FromBody] UpdateUserComicRequest request)
    {
        try
        {
            await service.UpdateUserComic(id, request);
            return Results.Ok();
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

    static async Task<IResult> DeleteUserComic([FromQuery] Guid id, [FromQuery] string userId,
        IUserComicService service, HttpContext httpContext)
    {
        try
        {
            var userIdFromAuth = GetUserIdFromAuth(httpContext);

            if (userIdFromAuth == null)
            {
                return Results.Unauthorized();
            }

            if (userId != userIdFromAuth)
            {
                return Results.Forbid();
            }

            await service.DeleteUserComic(id, userId);
            return Results.Ok();
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

    private static string? GetUserIdFromAuth(HttpContext httpContext)
    {
        ClaimsPrincipal user = httpContext.User;
        string? userIdFromAuth = null;
        foreach (Claim userClaim in user.Claims)
        {
            if (userClaim.Type == ClaimTypes.NameIdentifier)
            {
                userIdFromAuth = userClaim.Value;
            }
        }

        return userIdFromAuth;
    }
}