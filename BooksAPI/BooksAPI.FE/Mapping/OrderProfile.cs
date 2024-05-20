using AutoMapper;
using BooksAPI.FE.Contracts.Order;
using BooksAPI.FE.Model;

namespace BooksAPI.FE.Mapping;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderModel, CreateOrderRequest>()
            .ForMember(request => request.Date,
                options =>
                    options.MapFrom(model => model.Date.HasValue
                            ? DateOnly.FromDateTime(model.Date.Value)
                            : DateOnly.FromDateTime(DateTime.Today)));

        CreateMap<OrderModel, UpdateOrderRequest>()
            .ForMember(request => request.Date,
                options =>
                    options.MapFrom(model => model.Date.HasValue
                        ? DateOnly.FromDateTime(model.Date.Value)
                        : DateOnly.FromDateTime(DateTime.Today)));

        CreateMap<OrderResponse, OrderModel>()
            .ForMember(model => model.Date,
                options =>
                    options.MapFrom(response => response.Date.ToDateTime(new TimeOnly(0, 0))));
    }
}