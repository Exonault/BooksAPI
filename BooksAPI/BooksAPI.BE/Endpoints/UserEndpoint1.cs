using BooksAPI.BE.Constants;
using BooksAPI.BE.Contracts.User;
using BooksAPI.BE.Exception;
using BooksAPI.BE.Interfaces.Repositories;
using BooksAPI.BE.Interfaces.Services;
using BooksAPI.BE.Repositories;
using BooksAPI.BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.BE.Endpoints;


public static class UserEndpoint1
{
    public static void MapUserEndpoints1(this WebApplication app)
    {
        app.MapPost("user/test/register/", Register)
            .AllowAnonymous()
            .Produces(StatusCodes.Status200OK, typeof(RegisterResponse), "application/json")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);
        
        app.MapPost("user/test/login/", Login)
            .AllowAnonymous()
            .Produces(StatusCodes.Status200OK, typeof(LoginResponse), "application/json")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapPost("user/test/refresh/", Refresh)
            .AllowAnonymous()
            .Produces(StatusCodes.Status200OK, typeof(LoginResponse), "application/json")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapDelete("user/test/revoke", Revoke)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }


    public static void AddUserServices1(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository1, UserRepository1>();
        services.AddScoped<IUserService1, UserService1>();
    }
    
    private static async Task<IResult> Register([FromBody]RegisterRequest request, IUserService1 service)
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

    private static async Task<IResult> Login([FromBody]LoginRequest request, IUserService1 service)
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

    private static async Task<IResult> Refresh([FromBody]RefreshRequest request, IUserService1 service)
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

    private static async Task<IResult> Revoke(IUserService1 service)
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