using BooksAPI.BE.Constants;
using BooksAPI.BE.Contracts.Order;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Exception;
using BooksAPI.BE.Interfaces.Repositories;
using BooksAPI.BE.Interfaces.Services;
using BooksAPI.BE.Repositories;
using BooksAPI.BE.Services;
using BooksAPI.BE.Util;
using BooksAPI.BE.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.BE.Endpoints;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this WebApplication app)
    {
        app.MapPost("/order", CreateOrder)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithSummary("Create an order")
            .WithOpenApi();

        app.MapGet("/order/{id:int}", GetOrderById)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .CacheOutput(x => x.Expire(TimeSpan.FromMinutes(5)))
            .Produces(StatusCodes.Status200OK, typeof(OrderResponse), "application/json")
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithSummary("Retrieve an order by id")
            .WithOpenApi();;

        app.MapGet("/order/", GetAllOrdersByUserId)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .Produces(StatusCodes.Status200OK, typeof(List<OrderResponse>), "application/json")
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithSummary("Retrieve all orders related to a user")
            .WithOpenApi();;

        app.MapGet("/orders/all", GetAllOrders)
            .RequireAuthorization(AppConstants.PolicyNames.AdminRolePolicyName)
            .Produces(StatusCodes.Status200OK, typeof(List<OrderResponse>), "application/json")
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithSummary("Retrieve ALL orders")
            .WithOpenApi();;

        app.MapPut("/order/{id:int}", UpdateOrder)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithSummary("Update an order")
            .WithOpenApi();;

        app.MapDelete("/order/{id:int}", DeleteOrder)
            .RequireAuthorization(AppConstants.PolicyNames.UserRolePolicyName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }

    public static void AddOrderServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderService, OrderServices>();
        services.AddScoped<IValidator<Order>, OrderValidator>();
    }

    static async Task<IResult> CreateOrder([FromBody] CreateOrderRequest request,
        IOrderService service, HttpContext httpContext)
    {
        try
        {
            int statusCode = UserValidationUtil.IsUserIdFromRequestValidWithAuthUser(httpContext, request.UserId);

            switch (statusCode)
            {
                case StatusCodes.Status401Unauthorized:
                    return Results.Unauthorized();
                case StatusCodes.Status403Forbidden:
                    return Results.Forbid();
                default:
                {
                    await service.CreateOrder(request);
                    return Results.Ok();
                }
            }
        }
        catch (UserNotFoundException ex)
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

    static async Task<IResult> GetOrderById([FromRoute] int id, IOrderService service, HttpContext httpContext)
    {
        try
        {
            OrderResponse response = await service.GetOrder(id);
            int statusCode = UserValidationUtil.IsUserIdFromRequestValidWithAuthUser(httpContext, response.UserId);

            switch (statusCode)
            {
                case StatusCodes.Status401Unauthorized:
                    return Results.Unauthorized();
                case StatusCodes.Status403Forbidden:
                    return Results.Forbid();
                default:
                {
                    return Results.Ok(response);
                }
            }
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

    static async Task<IResult> GetAllOrders(IOrderService service)
    {
        try
        {
            List<OrderResponse> orderResponses = await service.GetAllOrders();
            return Results.Ok(orderResponses);
        }
        catch (System.Exception ex)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    static async Task<IResult> GetAllOrdersByUserId([FromQuery] string userId,
        IOrderService service, HttpContext httpContext)
    {
        try
        {
            int statusCode = UserValidationUtil.IsUserIdFromRequestValidWithAuthUser(httpContext, userId);

            switch (statusCode)
            {
                case StatusCodes.Status401Unauthorized:
                    return Results.Unauthorized();
                case StatusCodes.Status403Forbidden:
                    return Results.Forbid();
                default:
                {
                    List<OrderResponse> response = await service.GetAllOrdersByUserId(userId);
                    return Results.Ok(response);
                }
            }
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

    static async Task<IResult> UpdateOrder([FromRoute] int id, [FromBody] UpdateOrderRequest request,
        IOrderService service, HttpContext httpContext)
    {
        try
        {
            int statusCode = UserValidationUtil.IsUserIdFromRequestValidWithAuthUser(httpContext, request.UserId);

            switch (statusCode)
            {
                case StatusCodes.Status401Unauthorized:
                    return Results.Unauthorized();
                case StatusCodes.Status403Forbidden:
                    return Results.Forbid();
                default:
                {
                    await service.UpdateOrder(id, request);
                    return Results.Ok();
                }
            }
        }
        catch (ResourceNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (UserNotFoundException ex)
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


    static async Task<IResult> DeleteOrder([FromRoute] int id, [FromQuery] string userId,
        IOrderService service, HttpContext httpContext)
    {
        try
        {
            int statusCode = UserValidationUtil.IsUserIdFromRequestValidWithAuthUser(httpContext, userId);
            switch (statusCode)
            {
                case StatusCodes.Status401Unauthorized:
                    return Results.Unauthorized();
                case StatusCodes.Status403Forbidden:
                    return Results.Forbid();
                default:
                {
                    await service.DeleteOrder(id, userId);
                    return Results.Ok();
                }
            }
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(ex.Message);
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