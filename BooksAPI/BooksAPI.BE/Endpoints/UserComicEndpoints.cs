using BooksAPI.BE.Contracts.UserComic;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Exception;
using BooksAPI.BE.Interfaces.Repositories;
using BooksAPI.BE.Interfaces.Services;
using BooksAPI.BE.Repositories;
using BooksAPI.BE.Services;
using BooksAPI.BE.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BooksAPI.BE.Endpoints;

public static class UserComicEndpoints
{
    public static void MapUserComicEndpoints(this WebApplication app)
    {
        app.MapPost("/userComic", CreateUserComic);

        app.MapGet("/userComic/{id:guid}", GetUserComicById);
        app.MapGet("/userComic", GetAllUserComics);
        app.MapGet("/userComic/{userId}", GetAllUserComicsByUserId);

        app.MapPut("/userComic/{id:guid}", UpdateUserComic);

        app.MapDelete("/userComic/", DeleteUserComic);
    }

    public static void AddUserComicServices(this IServiceCollection services)
    {
        services.AddScoped<IUserComicRepository, UserComicRepository>();
        services.AddScoped<IUserComicService, UserComicService>();
        services.AddScoped<IValidator<UserComic>, UserComicValidator>();
    }

    static async Task<IResult> CreateUserComic(IUserComicService service, CreateUserComicRequest request)
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

    static async Task<IResult> GetUserComicById(IUserComicService service, Guid id)
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

    static async Task<IResult> GetAllUserComicsByUserId(IUserComicService service, string userId)
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

    static async Task<IResult> UpdateUserComic(IUserComicService service, Guid id, UpdateUserComicRequest request)
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

    static async Task<IResult> DeleteUserComic(IUserComicService service, Guid id, DeleteUserComicRequest request)
    {
        try
        {
            await service.DeleteUserComic(request);
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
        catch (System.Exception ex)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}