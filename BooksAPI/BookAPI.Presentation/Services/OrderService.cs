using System.Net;
using AutoMapper;
using BookAPI.Presentation.Constants;
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

    public Task<HttpResponseMessage> CreateOrder(ModifyOrderModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<HttpResponseMessage> GetAllOrders()
    {
        var uri = _configuration.GetSection("ApiUri")
            .GetSection("Orders")
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

    public Task<HttpResponseMessage> GetOrder(string id)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> UpdateOrder(string id, ModifyOrderModel model)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> DeleteOrder(string id)
    {
        throw new NotImplementedException();
    }
}