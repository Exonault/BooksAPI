using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using AutoMapper;
using BooksAPI.FE.Constants;
using BooksAPI.FE.Contracts.Order;
using BooksAPI.FE.Interfaces;
using BooksAPI.FE.Model;

namespace BooksAPI.FE.Services;

public class OrderService : IOrderService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IMapper _mapper;

    public OrderService(IConfiguration configuration, IHttpClientFactory clientFactory,
        IRefreshTokenService refreshTokenService, IMapper mapper)
    {
        _configuration = configuration;
        _clientFactory = clientFactory;
        _refreshTokenService = refreshTokenService;
        _mapper = mapper;
    }

    public async Task<List<OrderResponse>> GetUserOrders(string token, string refreshToken, string userId)
    {
        string url = string.Format(_configuration["Backend:Orders:GetUserOrders"]!, userId);

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpClient httpClient = _clientFactory.CreateClient();

        HttpResponseMessage responseMessage = await httpClient.SendAsync(request);

        if (!responseMessage.IsSuccessStatusCode)
        {
            if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                responseMessage = await RefreshRequest(token, refreshToken, request, httpClient);
            }
        }

        if (responseMessage.IsSuccessStatusCode)
        {
            await using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync())
            {
                List<OrderResponse>? response =
                    await JsonSerializer.DeserializeAsync<List<OrderResponse>>(responseStream);

                if (response is null)
                {
                    throw new Exception();
                }

                return response;
            }
        }

        throw new Exception();
    }

    public async Task<OrderResponse> GetOrder(int id, string token, string refreshToken, string userId)
    {
        string url = string.Format(_configuration["Backend:Orders:GetOrder"]!, id);

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

        HttpClient httpClient = _clientFactory.CreateClient();

        HttpResponseMessage responseMessage = await httpClient.SendAsync(request);
        if (!responseMessage.IsSuccessStatusCode)
        {
            if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                responseMessage = await RefreshRequest(token, refreshToken, request, httpClient);
            }
            else throw new Exception();
        }


        if (responseMessage.IsSuccessStatusCode)
        {
            await using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync())
            {
                OrderResponse? response =
                    await JsonSerializer.DeserializeAsync<OrderResponse>(responseStream);

                if (response is null)
                {
                    throw new Exception();
                }

                return response;
            }
        }

        throw new Exception();
    }

    public async Task<OrderModel> GetOrderModel(int id, string token, string refreshToken, string userId)
    {
        OrderResponse order = await GetOrder(id, token, refreshToken, userId);

        OrderModel model = _mapper.Map<OrderModel>(order);

        return model;
    }

    public async Task<bool> CreateOrder(OrderModel model, string token, string refreshToken, string userId)
    {
        string url = _configuration["Backend:Orders:CreateOrder"]!;

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        CreateOrderRequest requestContent = _mapper.Map<CreateOrderRequest>(model);
        
        requestContent.UserId = userId;

        request.Content = JsonContent.Create(requestContent);

        HttpClient httpClient = _clientFactory.CreateClient();

        HttpResponseMessage responseMessage = await httpClient.SendAsync(request);
        if (!responseMessage.IsSuccessStatusCode)
        {
            if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                responseMessage = await RefreshRequest(token, refreshToken, request, httpClient);
            }
            else throw new Exception();
        }

        if (responseMessage.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> UpdateOrder(int id, OrderModel model, string token, string refreshToken, string userId)
    {
        string url = string.Format(_configuration["Backend:Orders:UpdateOrder"]!, id);

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        UpdateOrderRequest requestContent = _mapper.Map<UpdateOrderRequest>(model);

        requestContent.UserId = userId;

        // return false;

        request.Content = JsonContent.Create(requestContent);

        HttpClient httpClient = _clientFactory.CreateClient();

        HttpResponseMessage responseMessage = await httpClient.SendAsync(request);
        if (!responseMessage.IsSuccessStatusCode)
        {
            if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                responseMessage = await RefreshRequest(token, refreshToken, request, httpClient);
            }
            else throw new Exception();
        }

        if (responseMessage.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> DeleteOrder(int id, string token, string refreshToken, string userId)
    {
        string url = string.Format(_configuration["Backend:Orders:DeleteOrder"]!, id, userId);

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpClient httpClient = _clientFactory.CreateClient();

        HttpResponseMessage responseMessage = await httpClient.SendAsync(request);
        if (!responseMessage.IsSuccessStatusCode)
        {
            if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                responseMessage = await RefreshRequest(token, refreshToken, request, httpClient);
            }
            else throw new Exception();
        }

        if (responseMessage.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }

    private async Task<HttpResponseMessage> RefreshRequest(string token, string refreshToken,
        HttpRequestMessage request, HttpClient httpClient)
    {
        HttpResponseMessage responseMessage;
        bool isRefreshSuccessful = await _refreshTokenService.RefreshToken(token, refreshToken);

        if (isRefreshSuccessful)
        {
            string[] tokens = await _refreshTokenService.GetTokens();

            token = tokens[0];

            HttpRequestMessage refreshedRequest = new HttpRequestMessage(request.Method, request.RequestUri);
            refreshedRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            responseMessage = await httpClient.SendAsync(refreshedRequest);
        }
        else
        {
            throw new InvalidOperationException();
        }

        return responseMessage;
    }
}