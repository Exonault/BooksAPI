using BooksAPI.BE.Constants;
using BooksAPI.BE.Contracts.User;
using BooksAPI.BE.Exception;
using BooksAPI.BE.Interfaces.Repositories;
using BooksAPI.BE.Interfaces.Services;
using BooksAPI.BE.Repositories;
using BooksAPI.BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.BE.Endpoints;


public static class UserEndpoint
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapPost("user/register/", Register)
            .AllowAnonymous()
            .Produces(StatusCodes.Status200OK, typeof(RegisterResponse), "application/json")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);
        
        app.MapPost("user/login/", Login)
            .AllowAnonymous()
            .Produces(StatusCodes.Status200OK, typeof(LoginResponse), "application/json")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapPost("user/refresh/", Refresh)
            .AllowAnonymous()
            .Produces(StatusCodes.Status200OK, typeof(LoginResponse), "application/json")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapDelete("user/revoke", Revoke)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }


    public static void AddUserServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
    }
    
    private static async Task<IResult> Register([FromBody]RegisterRequest request, IUserService service)
    {
        try
        {
            RegisterResponse response = await service.RegisterAccount(request);
            return Results.Ok(response);
        }
        catch (InvalidEmailPasswordException ex)
        {
            return Results.BadRequest(ex.Message);
        }
        catch (UserAlreadyRegisteredException ex)
        {
            return Results.Conflict(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(ex.Message);
        }
        catch (System.Exception ex)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task<IResult> Login([FromBody]LoginRequest request, IUserService service)
    {
        try
        {
            LoginResponse response = await service.LoginAccount(request);
            return Results.Ok(response);
        }
        catch (InvalidEmailPasswordException ex)
        {
            return Results.BadRequest(ex.Message);
        }
        catch (UserNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(ex.Message);
        }
        catch (System.Exception ex)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task<IResult> Refresh([FromBody]RefreshRequest request, IUserService service)
    {
        try
        {
            LoginResponse response = await service.Refresh(request);
            return Results.Ok(response);
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

    private static async Task<IResult> Revoke(IUserService service)
    {
        try
        {
            await service.Revoke();
            return Results.Ok();
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
    
}