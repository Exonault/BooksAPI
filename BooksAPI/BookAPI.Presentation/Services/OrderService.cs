using System.Net;
using AutoMapper;
using BookAPI.Presentation.Constants;
using BookAPI.Presentation.Contracts.Requests.Order;
using BookAPI.Presentation.Interfaces;
using BookAPI.Presentation.Models;

namespace BookAPI.Presentation.Services;

public class OrderService : IOrderService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;


    public OrderService(IHttpClientFactory clientFactory, IMapper mapper, IConfiguration configuration)
    {
        _clientFactory = clientFactory;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<HttpResponseMessage> CreateOrder(ModifyOrderModel model)
    {
        CreateOrderRequest requestContent = _mapper.Map<CreateOrderRequest>(model);

        var uri = _configuration.GetSection("ApiUri")
            .GetSection(OrderConstants.OrdersSection)
            .GetSection(OrderConstants.CreateOrderUri).Value;

        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        request.Content = JsonContent.Create(requestContent);

        var cliend = _clientFactory.CreateClient();
        try
        {
            HttpResponseMessage response = await cliend.SendAsync(request);
            return response;
        }
        catch (Exception e)
        {
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }

    public async Task<HttpResponseMessage> GetAllOrders()
    {
        var uri = _configuration.GetSection("ApiUri")
            .GetSection(OrderConstants.OrdersSection)
            .GetSection(OrderConstants.GetAllOrdersUri).Value;

        var request = new HttpRequestMessage(HttpMethod.Get, uri);

        var client = _clientFactory.CreateClient();

        try
        {
            HttpResponseMessage response = await client.SendAsync(request);
            return response;
        }
        catch (Exception e)
        {
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }

    public async Task<HttpResponseMessage> GetOrder(string id)
    {
        string uri = string.Format(_configuration.GetSection("ApiUri")
            .GetSection(OrderConstants.OrdersSection)
            .GetSection(OrderConstants.GetOrderByIdUri).Value ?? string.Empty, id);

        var request = new HttpRequestMessage(HttpMethod.Get, uri);

        var client = _clientFactory.CreateClient();

        try
        {
            HttpResponseMessage response = await client.SendAsync(request);
            return response;
        }
        catch (Exception e)
        {
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }

    public async Task<HttpResponseMessage> UpdateOrder(string id, ModifyOrderModel model)
    {
        UpdateOrderRequest requestContent = _mapper.Map<UpdateOrderRequest>(model);

        string uri = string.Format(_configuration.GetSection("ApiUri")
            .GetSection(OrderConstants.OrdersSection)
            .GetSection(OrderConstants.UpdateOrderUri).Value ?? string.Empty, id);
        var request = new HttpRequestMessage(HttpMethod.Put, uri);
        request.Content = JsonContent.Create(requestContent);

        var client = _clientFactory.CreateClient();
        try
        {
            HttpResponseMessage response = await client.SendAsync(request);
            return response;
        }
        catch (Exception e)
        {
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }

    public async Task<HttpResponseMessage> DeleteOrder(string id)
    {
        string uri = string.Format(_configuration.GetSection("ApiUri")
            .GetSection(OrderConstants.OrdersSection)
            .GetSection(OrderConstants.DeleteOrderUri).Value ?? string.Empty, id);

        var request = new HttpRequestMessage(HttpMethod.Delete, uri);

        var client = _clientFactory.CreateClient();
        try
        {
            HttpResponseMessage response = await client.SendAsync(request);
            return response;
        }
        catch (Exception e)
        {
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}