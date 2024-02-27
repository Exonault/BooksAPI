using BooksAPI.BE.Contracts.User;
using BooksAPI.BE.Exception;
using BooksAPI.BE.Interfaces.Repositories;
using BooksAPI.BE.Interfaces.Services;
using BooksAPI.BE.Repositories;
using BooksAPI.BE.Services;

namespace BooksAPI.BE.Endpoints;

using static UserResponses;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapPost("user/register/", RegisterUser);
        app.MapPost("user/login/", Login);
    }


    public static void AddUserServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
    }

    static async Task<IResult> RegisterUser(IUserService service, RegisterRequest request)
    {
        try
        {
            RegisterResponse response = await service.RegisterAccount(request);
            return Results.Ok(response);
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

    static async Task<IResult> Login(IUserService service, LoginRequest request)
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
}