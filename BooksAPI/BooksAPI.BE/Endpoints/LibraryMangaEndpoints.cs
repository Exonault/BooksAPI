using BooksAPI.BE.Constants;
using BooksAPI.BE.Contracts.LibraryManga;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Exception;
using BooksAPI.BE.Interfaces.Repositories;
using BooksAPI.BE.Interfaces.Services;
using BooksAPI.BE.Repositories;
using BooksAPI.BE.Services;
using BooksAPI.BE.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;


namespace BooksAPI.BE.Endpoints;

public static class LibraryMangaEndpoints
{
    public static void MapLibraryMangaEndpoints(this WebApplication app)
    {
        app.MapPost("/libraryManga/", CreateLibraryManga)
            .RequireAuthorization(AppConstants.PolicyNames.AdminRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapGet("/libraryManga/{id:int}", GetLibraryMangaById)
            .AllowAnonymous()
            .CacheOutput(x => x.Expire(TimeSpan.FromMinutes(10))
                .Tag(CacheConstants.LibraryMangaWithIdTag))
            .Produces(StatusCodes.Status200OK, typeof(LibraryMangaResponse), "application/json")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapGet("/libraryMangas/", GetLibraryMangasForPage)
            .AllowAnonymous()
            .CacheOutput(x => x.Expire(TimeSpan.FromMinutes(15))
                .Tag(CacheConstants.LibraryMangaForPageTag))
            .Produces(StatusCodes.Status200OK, typeof(List<LibraryMangaForPageResponse>), "application/json")
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapGet("libraryManga/search", SearchByTitle)
            .AllowAnonymous()
            .CacheOutput(x => x.Expire(TimeSpan.FromMinutes(10))
                .Tag(CacheConstants.LibraryMangaSearchTag))
            .Produces(StatusCodes.Status200OK, typeof(List<LibraryMangaResponse>), "application/json")
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapGet("/libraryMangas/all", GetAllLibraryMangas)
            .RequireAuthorization(AppConstants.PolicyNames.AdminRolePolicyName)
            .CacheOutput(x => x.Expire(TimeSpan.FromMinutes(15))
                .Tag(CacheConstants.AllLibraryMangasTag))
            .Produces(StatusCodes.Status200OK, typeof(List<LibraryMangaResponse>), "application/json")
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapPut("/libraryManga/{id:int}", UpdateLibraryMangas)
            .RequireAuthorization(AppConstants.PolicyNames.AdminRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapDelete("/libraryManga/{id:int}", DeleteLibraryMangas)
            .RequireAuthorization(AppConstants.PolicyNames.AdminRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }


    public static void AddLibraryMangaServices(this IServiceCollection services)
    {
        services.AddScoped<ILibraryMangaRepository, LibraryMangaRepository>();
        services.AddScoped<ILibraryMangaService, LibraryMangaService>();
        services.AddScoped<IValidator<LibraryManga>, LibraryMangaValidator>();

        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IValidator<Author>, AuthorValidator>();
    }

    private static async Task<IResult> CreateLibraryManga([FromBody] CreateLibraryMangaRequest request,
        ILibraryMangaService service, IOutputCacheStore cacheStore)
    {
        try
        {
            await service.CreateLibraryManga(request);
            await cacheStore.EvictByTagAsync(CacheConstants.LibraryMangaForPageTag, CancellationToken.None);
            await cacheStore.EvictByTagAsync(CacheConstants.AllLibraryMangasTag, CancellationToken.None);
            await cacheStore.EvictByTagAsync(CacheConstants.LibraryMangaSearchTag, CancellationToken.None);
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

    private static async Task<IResult> GetLibraryMangaById([FromRoute] int id, ILibraryMangaService service)
    {
        try
        {
            LibraryMangaResponse response = await service.GetLibraryManga(id);
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

    private static async Task<IResult> GetAllLibraryMangas(ILibraryMangaService service)
    {
        List<LibraryMangaResponse> libraryMangaResponses = await service.GetAllLibraryMangas();

        return Results.Ok(libraryMangaResponses);
    }

    private static async Task<IResult> SearchByTitle([FromQuery] string title, ILibraryMangaService service)
    {
        List<LibraryMangaResponse> libraryMangaResponses = await service.SearchByTitle(title);
        return Results.Ok(libraryMangaResponses);
    }

    private static async Task<IResult> GetLibraryMangasForPage([FromQuery] int pageIndex,
        [FromQuery] int pageEntriesCount, ILibraryMangaService service)
    {
        List<LibraryMangaForPageResponse> libraryMangaResponses =
            await service.GetLibraryMangasForPage(pageIndex, pageEntriesCount);

        return Results.Ok(libraryMangaResponses);
    }

    private static async Task<IResult> UpdateLibraryMangas([FromRoute] int id,
        [FromBody] UpdateLibraryMangaRequest request, ILibraryMangaService service, IOutputCacheStore cacheStore)
    {
        try
        {
            await service.UpdateLibraryManga(id, request);
            await cacheStore.EvictByTagAsync(CacheConstants.LibraryMangaForPageTag, CancellationToken.None);
            await cacheStore.EvictByTagAsync(CacheConstants.AllLibraryMangasTag, CancellationToken.None);
            await cacheStore.EvictByTagAsync(CacheConstants.LibraryMangaWithIdTag, CancellationToken.None);
            await cacheStore.EvictByTagAsync(CacheConstants.LibraryMangaSearchTag, CancellationToken.None);
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

    private static async Task<IResult> DeleteLibraryMangas([FromRoute] int id, ILibraryMangaService service,
        IOutputCacheStore cacheStore)
    {
        try
        {
            await service.DeleteLibraryManga(id);
            await cacheStore.EvictByTagAsync(CacheConstants.LibraryMangaForPageTag, CancellationToken.None);
            await cacheStore.EvictByTagAsync(CacheConstants.AllLibraryMangasTag, CancellationToken.None);
            await cacheStore.EvictByTagAsync(CacheConstants.LibraryMangaSearchTag, CancellationToken.None);
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