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
using Microsoft.AspNetCore.Mvc;


namespace BooksAPI.BE.Endpoints;

public static class LibraryComicEndpoints
{
    public static void MapLibraryComicEndpoints(this WebApplication app)
    {
        app.MapPost("/libraryComic/", CreateLibraryComic)
            .RequireAuthorization(AppConstants.PolicyNames.AdminRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapGet("/libraryComic/{id:guid}", GetLibraryComicById)
            .AllowAnonymous()
            .Produces(StatusCodes.Status200OK, typeof(LibraryComicResponse), "application/json")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);


        app.MapGet("/libraryComics/", GetAllLibraryComics)
            .AllowAnonymous()
            .Produces(StatusCodes.Status200OK, typeof(List<LibraryComicResponse>), "application/json")
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapPut("/libraryComic/", UpdateLibraryComic)
            .RequireAuthorization(AppConstants.PolicyNames.AdminRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapDelete("/libraryComic/", DeleteLibraryComic)
            .RequireAuthorization(AppConstants.PolicyNames.AdminRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }


    public static void AddLibraryComicServices(this IServiceCollection services)
    {
        services.AddScoped<ILibraryComicRepository, LibraryComicRepository>();
        services.AddScoped<ILibraryComicService, LibraryComicService>();
        services.AddScoped<IValidator<LibraryComic>, LibraryComicValidator>();
    }

    private static async Task<IResult> CreateLibraryComic([FromBody] CreateLibraryComicRequest request,
        ILibraryComicService service)
    {
        try
        {
            await service.CreateLibraryComic(request);
            return Results.Ok();
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

    private static async Task<IResult> GetLibraryComicById([FromRoute] Guid id, ILibraryComicService service)
    {
        try
        {
            LibraryComicResponse response = await service.GetLibraryComic(id);
            return Results.Ok(response);
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

    private static async Task<IResult> GetAllLibraryComics(ILibraryComicService service)
    {
        List<LibraryComicResponse> libraryComicResponses = await service.GetAllLibraryComics();

        return Results.Ok(libraryComicResponses);
    }


    private static async Task<IResult> UpdateLibraryComic([FromQuery] Guid id,
        [FromBody] UpdateLibraryComicRequest request, ILibraryComicService service)
    {
        try
        {
            await service.UpdateLibraryComic(id, request);
            return Results.Ok();
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

    private static async Task<IResult> DeleteLibraryComic([FromQuery] Guid id, ILibraryComicService service)
    {
        try
        {
            await service.DeleteLibraryComic(id);
            return Results.Ok();
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