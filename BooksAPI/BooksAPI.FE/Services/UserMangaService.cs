using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using BooksAPI.FE.Contracts.UserManga;
using BooksAPI.FE.Interfaces;
using BooksAPI.FE.Model;

namespace BooksAPI.FE.Services;

public class UserMangaService : IUserMangaService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IRefreshTokenService _refreshTokenService;

    public UserMangaService(IConfiguration configuration, IHttpClientFactory clientFactory,
        IRefreshTokenService refreshTokenService)
    {
        _configuration = configuration;
        _clientFactory = clientFactory;
        _refreshTokenService = refreshTokenService;
    }

    public async Task<IEnumerable<UserMangaResponse>> GetUserMangas(string token, string refreshToken, string userId)
    {
        string url = string.Format(_configuration["Backend:UserMangas:GetUserMangas"]!, userId);
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
            List<UserMangaResponse>? response =
                await JsonSerializer.DeserializeAsync<List<UserMangaResponse>>(responseStream);

            if (response is null)
            {
                throw new Exception();
            }

            return response;
        }
    }

    public Task<UserMangaResponse> GetUserManga(string token, string refreshToken, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateUserManga(UserMangaModel model, string token, string refreshToken, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateUserManga(UserMangaModel model, string token, string refreshToken, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUserManga(UserMangaModel model, string token, string refreshToken, string userId)
    {
        throw new NotImplementedException();
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