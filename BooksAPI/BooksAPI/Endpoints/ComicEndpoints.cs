using BooksAPI.Constants;
using BooksAPI.Contracts.Requests.Comic;
using BooksAPI.Contracts.Response.Comic;
using BooksAPI.Entities;
using BooksAPI.Exception;
using BooksAPI.Interfaces.Repositories;
using BooksAPI.Interfaces.Services;
using BooksAPI.Messages;
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
        app.MapGet("/comics/{id:guid}", GetComicById);
        app.MapGet("/comics/readingStatus/{readingStatus}", GetComicsByReadingStatus);
        app.MapGet("/comics/demographic/{demographic}", GetComicsByDemographic);
        app.MapGet("/comics/publishingStatus/{publishingStatus}", GetComicsByPublishingStatus);
        app.MapGet("/comics/comicType/{comicType}", GetComicsByComicType);

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

    internal static async Task<IResult> GetComicsByReadingStatus(IComicService service, string readingStatus)
    {
        if (!AppConstants.ReadingStatuses.Contains(readingStatus))
        {
            return Results.BadRequest(BookValidationMessages.ReadingStatusMessageMessage);
        }

        List<GetComicResponse> allComicsByReadingStatus = await service.GetAllComicsByReadingStatus(readingStatus);

        return Results.Ok(allComicsByReadingStatus);
    }

    internal static async Task<IResult> GetComicsByDemographic(IComicService service, string demographic)
    {
        if (!AppConstants.DemographicTypes.Contains(demographic))
        {
            return Results.BadRequest(ComicValidationMessages.DemographicTypesMessage);
        }

        List<GetComicResponse> allComicsByDemographic = await service.GetAllComicsByDemographic(demographic);

        return Results.Ok(allComicsByDemographic);
    }

    internal static async Task<IResult> GetComicsByPublishingStatus(IComicService service, string publishingStatus)
    {
        if (!AppConstants.PublishingStatuses.Contains(publishingStatus))
        {
            return Results.BadRequest(ComicValidationMessages.PublishingStatusValidationMessage);
        }

        List<GetComicResponse> allComicsByPublishingStatus =
            await service.GetAllComicsByPublishingStatus(publishingStatus);

        return Results.Ok(allComicsByPublishingStatus);
    }

    internal static async Task<IResult> GetComicsByComicType(IComicService service, string comicType)
    {
        if (!AppConstants.ComicTypes.Contains(comicType))
        {
            return Results.BadRequest(ComicValidationMessages.ComicTypeValidationMessage);
        }

        List<GetComicResponse> allComicsByComicType = await service.GetAllComicsByComicType(comicType);

        return Results.Ok(allComicsByComicType);
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