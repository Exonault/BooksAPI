using AutoMapper;
using BookAPI.Presentation.Contracts.Requests.Order;
using BookAPI.Presentation.Contracts.Response.Order;
using BookAPI.Presentation.Models;

namespace BookAPI.Presentation.Mapping;

public class OrderProfile:Profile
{
    public OrderProfile()
    {
        CreateMap<GetOrderResponse, OrderListElementModel>();

        CreateMap<GetOrderResponse, ModifyOrderModel>();
        CreateMap<ModifyOrderModel, CreateOrderRequest>();

        CreateMap<ModifyOrderModel, UpdateOrderRequest>();
    }

}