using BooksAPI.BE.Constants;
using BooksAPI.BE.Contracts.LibraryComic;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Exception;
using BooksAPI.BE.Interfaces.Repositories;
using BooksAPI.BE.Interfaces.Services;
using BooksAPI.BE.Repositories;
using BooksAPI.BE.Services;
using BooksAPI.BE.Validation;
using FluentValidation;


namespace BooksAPI.BE.Endpoints;

public static class LibraryComicEndpoints
{
    public static void MapLibraryComicEndpoints(this WebApplication app)
    {
        app.MapPost("/libraryComic/", CreateLibraryComic).RequireAuthorization(AppConstants.AdminRolePolicyName);
        
        app.MapGet("/libraryComic/{id:guid}", GetLibraryComicById);
        app.MapGet("/libraryComic/", GetAllLibraryComics);

        app.MapPut("/libraryComic/{id:guid}", UpdateLibraryComic).RequireAuthorization(AppConstants.AdminRolePolicyName);

        app.MapDelete("/libraryComic/{id:guid}", DeleteLibraryComic).RequireAuthorization(AppConstants.AdminRolePolicyName);
    }
    
    
    public static void AddLibraryComicServices(this IServiceCollection services)
    {
        services.AddScoped<ILibraryComicRepository, LibraryComicRepository>();
        services.AddScoped<ILibraryComicService, LibraryComicService>();
        services.AddScoped<IValidator<LibraryComic>, LibraryComicValidator>();
    }

    static async Task<IResult> CreateLibraryComic(ILibraryComicService service, 
        CreateLibraryComicRequest request)
    {
        try
        {
            await service.CreateLibraryComic(request);
        }
        catch (ValidationException ex)
        {
            return Results.BadRequest(ex.Message);
        }
        catch (System.Exception ex)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Results.Ok();
    }

    static async Task<IResult> GetLibraryComicById(ILibraryComicService service, Guid id)
    {
        LibraryComicResponse response;
        try
        {
            response = await service.GetLibraryComic(id);
        }
        catch (ResourceNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (System.Exception ex)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Results.Ok(response);
    }

    static async Task<IResult> GetAllLibraryComics(ILibraryComicService service)
    {
        List<LibraryComicResponse> libraryComicResponses = await service.GetAllLibraryComics();

        return Results.Ok(libraryComicResponses);

    }


    static async Task<IResult> UpdateLibraryComic(ILibraryComicService service, Guid id, 
        UpdateLibraryComicRequest request)
    {

        try
        {
            await service.UpdateLibraryComic(id, request);
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

        return Results.Ok();
    }

    static async Task<IResult> DeleteLibraryComic(ILibraryComicService service, Guid id)
    {
        try
        {
            await service.DeleteLibraryComic(id);
        }
        catch (ResourceNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (System.Exception ex)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Results.Ok();
    }
    
}