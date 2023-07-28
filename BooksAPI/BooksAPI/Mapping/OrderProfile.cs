using AutoMapper;
using BooksAPI.Contracts.Requests.Order;
using BooksAPI.Contracts.Response.Order;
using BooksAPI.Entities;

namespace BooksAPI.Mapping;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<CreateOrderRequest, Order>();
        CreateMap<Order, CreateOrderResponse>();

        CreateMap<Order, GetOrderResponse>();

        CreateMap<UpdateOrderRequest, Order>();
        CreateMap<Order, UpdateOrderResponse>();

        CreateMap<Order, DeleteOrderResponse>();
    }
}