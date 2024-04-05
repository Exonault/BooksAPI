using AutoMapper;
using BooksAPI.BE.Contracts.Order;
using BooksAPI.BE.Entities;

namespace BooksAPI.BE.Mapping;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<CreateOrderRequest, Order>(); // Doesn't map User

        CreateMap<Order, OrderResponse>(); 

        CreateMap<UpdateOrderRequest, Order>(); // Doesn't map User
    }
}