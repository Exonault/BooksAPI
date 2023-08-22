using BooksAPI.Contracts.Requests.Comic;
using BooksAPI.Contracts.Requests.Order;
using BooksAPI.Contracts.Response.Order;
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

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this WebApplication app)
    {
        app.MapPost("/orders", CreateOrder);

        app.MapGet("/orders", GetOrders);
        app.MapGet("/orders/{id}", GetOrderById);
        app.MapGet("/orders/place/{place}", GetOrdersByPlace);

        app.MapPut("/orders/{id}", UpdateOrder);

        app.MapDelete("/orders/{id}", DeleteOrder);
    }

    public static void AddOrderServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IValidator<Order>, OrderValidator>();
    }

    internal static async Task<IResult> CreateOrder(IOrderService service, [FromBody] CreateOrderRequest request)
    {
        try
        {
            await service.CreateOrder(request);
        }
        catch (ValidationException ex)
        {
            return Results.BadRequest(ex.Message);
        }

        return Results.Ok();
    }

    internal static async Task<IResult> GetOrders(IOrderService service)
    {
        List<GetOrderResponse> orders = await service.GetAllOrders();

        return Results.Ok(orders);
    }

    internal static async Task<IResult> GetOrderById(IOrderService service, Guid id)
    {
        GetOrderResponse response;
        try
        {
            response = await service.GetOrder(id);
        }
        catch (ResourceNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }

        return Results.Ok(response);
    }

    internal static async Task<IResult> GetOrdersByPlace(IOrderService service, string place)
    {
        List<GetOrderResponse> allOrdersFromPlace = await service.GetAllOrdersFromPlace(place);

        return Results.Ok(allOrdersFromPlace);
    }

    internal static async Task<IResult> UpdateOrder(IOrderService service, Guid id,
        [FromBody] UpdateOrderRequest request)
    {
        try
        {
            await service.UpdateOrder(id, request);
        }
        catch (ResourceNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (ValidationException ex1)
        {
            return Results.BadRequest(ex1.Message);
        }

        return Results.Ok();
    }

    internal static async Task<IResult> DeleteOrder(IOrderService service, Guid id)
    {
        try
        {
            await service.DeleteOrder(id);
        }
        catch (ResourceNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }

        return Results.Ok();
    }
}