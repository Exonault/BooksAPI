using BooksAPI.Contracts.Requests.Comic;
using BooksAPI.Contracts.Response.Comic;
using BooksAPI.Entities;
using BooksAPI.Exception;
using BooksAPI.Interfaces.Repositories;
using BooksAPI.Interfaces.Services;
using BooksAPI.Repositories;
using BooksAPI.Services;
using BooksAPI.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Endpoints;

public static class ComicEndpoints
{
    public static void MapComicEndpoints(this WebApplication app)
    {
        app.MapPost("/comics/", CreateComic);

        app.MapGet("/comics", GetComics);
        app.MapGet("/comics/{id}", GetComicById);

        app.MapPut("/comics/{id}", UpdateComic);

        app.MapDelete("/comics/{id}", DeleteComic);
    }

    public static void AddComicServices(this IServiceCollection services)
    {
        services.AddScoped<IComicRepository, ComicRepository>();
        services.AddScoped<IComicService, ComicService>();
        services.AddScoped<IValidator<Comic>, ComicValidator>();
    }

    internal static async Task<IResult> CreateComic(IComicService service, [FromBody] CreateComicRequest request)
    {
        CreateComicResponse response;

        try
        {
            response = await service.CreateComic(request);
        }
        catch (ValidationException ex)
        {
            return Results.BadRequest(ex.Message);
        }

        return Results.Ok(response);
    }


    internal static async Task<IResult> GetComics(IComicService service)
    {
        List<GetComicResponse> comics = await service.GetAllComics();

        return Results.Ok(comics);
    }

    internal static async Task<IResult> GetComicById(IComicService service, Guid id)
    {
        GetComicResponse response;

        try
        {
            response = await service.GetComic(id);
        }
        catch (ResourceNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }

        return Results.Ok(response);
    }


    internal static async Task<IResult> UpdateComic(IComicService service, Guid id,
        [FromBody] UpdateComicRequest request)
    {
        UpdateComicResponse response;
        try
        {
            response = await service.UpdateComic(id, request);
        }
        catch (ResourceNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (ValidationException ex1)
        {
            return Results.BadRequest(ex1.Message);
        }

        return Results.Ok(response);
    }

    internal static async Task<IResult> DeleteComic(IComicService service, Guid id)
    {
        DeleteComicResponse response;

        try
        {
            response = await service.DeleteComic(id);
        }
        catch (ResourceNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }

        return Results.Ok(response);
    }
}