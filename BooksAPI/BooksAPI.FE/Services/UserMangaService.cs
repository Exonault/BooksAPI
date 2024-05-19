using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using AutoMapper;
using BooksAPI.FE.Contracts.UserManga;
using BooksAPI.FE.Interfaces;
using BooksAPI.FE.Model;

namespace BooksAPI.FE.Services;

public class UserMangaService : IUserMangaService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IMapper _mapper;

    public UserMangaService(IConfiguration configuration, IHttpClientFactory clientFactory,
        IRefreshTokenService refreshTokenService, IMapper mapper)
    {
        _configuration = configuration;
        _clientFactory = clientFactory;
        _refreshTokenService = refreshTokenService;
        _mapper = mapper;
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

    public async Task<UserMangaModel> GetUserMangaModel(int id, string token, string refreshToken, string userId)
    {
        UserMangaResponse userManga = await GetUserManga(id, token, refreshToken, userId);

        UserMangaModel model = _mapper.Map<UserMangaModel>(userManga);

        model.LibraryMangaId = userManga.LibraryMangaResponse.Id;

        return model;
    }

    public async Task<UserMangaResponse> GetUserManga(int id, string token, string refreshToken, string userId)
    {
        string url = string.Format(_configuration["Backend:UserMangas:GetUserMangaById"]!, id);

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
            UserMangaResponse? response =
                await JsonSerializer.DeserializeAsync<UserMangaResponse>(responseStream);

            if (response is null)
            {
                throw new Exception();
            }

            return response;
        }
    }

    public async Task<bool> CreateUserManga(UserMangaModel model, string token, string refreshToken, string userId)
    {
        string url = _configuration["Backend:UserMangas:GetUserMangas"]!;

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        CreateUserMangaRequest requestContent = _mapper.Map<CreateUserMangaRequest>(model);

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

    public async Task<bool> UpdateUserManga(int id, UserMangaModel model, string token, string refreshToken,
        string userId)
    {
        string url = string.Format(_configuration["Backend:UserMangas:UpdateUserManga"]!, id);

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, url);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        UpdateUserMangaRequest requestContent = _mapper.Map<UpdateUserMangaRequest>(model);

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

    public async Task<bool> DeleteUserManga(int id, string token, string refreshToken, string userId)
    {
        string url = string.Format(_configuration["Backend:UserMangas:DeleteUserManga"]!, id, userId);

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