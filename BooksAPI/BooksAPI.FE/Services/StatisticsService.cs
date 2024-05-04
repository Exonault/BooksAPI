using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using BooksAPI.FE.Contracts.Statistics.Order;
using BooksAPI.FE.Contracts.Statistics.UserManga;
using BooksAPI.FE.Interfaces;

namespace BooksAPI.FE.Services;

public class StatisticsService : IStatisticsService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IRefreshTokenService _refreshTokenService;

    public StatisticsService(IConfiguration configuration, IHttpClientFactory clientFactory,
        IRefreshTokenService refreshTokenService)
    {
        _configuration = configuration;
        _clientFactory = clientFactory;
        _refreshTokenService = refreshTokenService;
    }

    public async Task<List<UserMangaDemographicResponse>> GetDemographicStatistics(string token, string refreshToken,
        string userId)
    {
        string url = string.Format(_configuration["Backend:Statistics:GetUserDemographicStatistics"]!, userId);

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
            else throw new Exception();
        }

        await using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync())
        {
            List<UserMangaDemographicResponse>? response =
                await JsonSerializer.DeserializeAsync<List<UserMangaDemographicResponse>>(responseStream);

            if (response is null)
            {
                throw new Exception();
            }

            return response;
        }
    }

    public async Task<List<UserMangaTypeResponse>> GetTypeStatistics(string token, string refreshToken, string userId)
    {
        string url = string.Format(_configuration["Backend:Statistics:GetUserTypeStatistics"]!, userId);

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
            else throw new Exception();
        }

        await using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync())
        {
            List<UserMangaTypeResponse>? response =
                await JsonSerializer.DeserializeAsync<List<UserMangaTypeResponse>>(responseStream);

            if (response is null)
            {
                throw new Exception();
            }

            return response;
        }
    }

    public async Task<List<UserMangaCollectionStatusResponse>> GetCollectionStatusStatistics(string token,
        string refreshToken, string userId)
    {
        string url = string.Format(_configuration["Backend:Statistics:GetUserCollectionStatusStatistics"]!, userId);

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
            else throw new Exception();
        }

        await using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync())
        {
            List<UserMangaCollectionStatusResponse>? response =
                await JsonSerializer.DeserializeAsync<List<UserMangaCollectionStatusResponse>>(responseStream);

            if (response is null)
            {
                throw new Exception();
            }

            return response;
        }
    }

    public async Task<List<UserMangaPublishingStatusResponse>> GetPublishingStatusStatistics(string token,
        string refreshToken, string userId)
    {
        string url = string.Format(_configuration["Backend:Statistics:GetUserPublishingStatusStatistics"]!, userId);

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
            else throw new Exception();
        }

        await using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync())
        {
            List<UserMangaPublishingStatusResponse>? response =
                await JsonSerializer.DeserializeAsync<List<UserMangaPublishingStatusResponse>>(responseStream);

            if (response is null)
            {
                throw new Exception();
            }

            return response;
        }
    }

    public async Task<List<UserMangaReadingStatusResponse>> GetReadingStatusStatistics(string token,
        string refreshToken, string userId)
    {
        string url = string.Format(_configuration["Backend:Statistics:GetUserReadingStatusStatistics"]!, userId);

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
            else throw new Exception();
        }

        await using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync())
        {
            List<UserMangaReadingStatusResponse>? response =
                await JsonSerializer.DeserializeAsync<List<UserMangaReadingStatusResponse>>(responseStream);

            if (response is null)
            {
                throw new Exception();
            }

            return response;
        }
    }

    public async Task<UserMangaTotalSpendingResponse> GetTotalSpendingStatistics(string token, string refreshToken,
        string userId)
    {
        string url = string.Format(_configuration["Backend:Statistics:GetUserTotalSpendingStatistics"]!, userId);

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
            else throw new Exception();
        }

        await using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync())
        {
            UserMangaTotalSpendingResponse? response =
                await JsonSerializer.DeserializeAsync<UserMangaTotalSpendingResponse>(responseStream);

            if (response is null)
            {
                throw new Exception();
            }

            return response;
        }
    }

    public async Task<GeneralStatisticsResponse> GetGeneralStatisticsResponse(string token, string refreshToken,
        string userId)
    {
        string url = string.Format(_configuration["Backend:Statistics:GetUserGeneralStatistics"]!, userId);

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
            else throw new Exception();
        }

        await using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync())
        {
            GeneralStatisticsResponse? response =
                await JsonSerializer.DeserializeAsync<GeneralStatisticsResponse>(responseStream);

            if (response is null)
            {
                throw new Exception();
            }

            return response;
        }
    }

    public async Task<List<OrderByPlaceResponse>> GetOrderByPlaceStatistics(string token, string refreshToken, string userId)
    {
        string url = string.Format(_configuration["Backend:Statistics:GetOrderByPlaceStatistics"]!, userId);

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
            else throw new Exception();
        }

        await using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync())
        {
            List<OrderByPlaceResponse>? response =
                await JsonSerializer.DeserializeAsync<List<OrderByPlaceResponse>>(responseStream);

            if (response is null)
            {
                throw new Exception();
            }

            return response;
        }
    }

    public async Task<List<OrdersByYearResponse>> GetOrderByYear(string token, string refreshToken, string userId)
    {
        string url = string.Format(_configuration["Backend:Statistics:GetOrderByYearStatistics"]!, userId);

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
            else throw new Exception();
        }

        await using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync())
        {
            List<OrdersByYearResponse>? response =
                await JsonSerializer.DeserializeAsync<List<OrdersByYearResponse>>(responseStream);

            if (response is null)
            {
                throw new Exception();
            }

            return response;
        }
    }

    public async Task<List<OrdersForMonthByYearResponse>> GetOrderForMonthByYearResponse(string token, string refreshToken,
        string userId, int year)
    {
        string url = string.Format(_configuration["Backend:Statistics:GetOrderForMonthsByYearStatistics"]!, userId);

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
            else throw new Exception();
        }

        await using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync())
        {
            List<OrdersForMonthByYearResponse>? response =
                await JsonSerializer.DeserializeAsync<List<OrdersForMonthByYearResponse>>(responseStream);

            if (response is null)
            {
                throw new Exception();
            }

            return response;
        }
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
            // refreshToken = tokens[1];
            
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