﻿using BooksAPI.BE.Constants;
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
            .RequireAuthorization(AppConstants.AdminRolePolicyName);

        app.MapGet("/libraryComic/{id:guid}", GetLibraryComicById);
        app.MapGet("/libraryComics/", GetAllLibraryComics);

        app.MapPut("/libraryComic/", UpdateLibraryComic)
            .RequireAuthorization(AppConstants.AdminRolePolicyName);

        app.MapDelete("/libraryComic/", DeleteLibraryComic)
            .RequireAuthorization(AppConstants.AdminRolePolicyName);
    }


    public static void AddLibraryComicServices(this IServiceCollection services)
    {
        services.AddScoped<ILibraryComicRepository, LibraryComicRepository>();
        services.AddScoped<ILibraryComicService, LibraryComicService>();
        services.AddScoped<IValidator<LibraryComic>, LibraryComicValidator>();
    }

    static async Task<IResult> CreateLibraryComic(ILibraryComicService service,
        [FromBody]CreateLibraryComicRequest request)
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

    static async Task<IResult> GetLibraryComicById(ILibraryComicService service, [FromRoute]Guid id)
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

    static async Task<IResult> GetAllLibraryComics(ILibraryComicService service)
    {
        List<LibraryComicResponse> libraryComicResponses = await service.GetAllLibraryComics();

        return Results.Ok(libraryComicResponses);
    }


    static async Task<IResult> UpdateLibraryComic(ILibraryComicService service, [FromQuery] Guid id,
        [FromBody] UpdateLibraryComicRequest request)
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

    static async Task<IResult> DeleteLibraryComic(ILibraryComicService service, [FromQuery]Guid id)
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