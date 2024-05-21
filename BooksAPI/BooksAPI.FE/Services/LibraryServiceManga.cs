using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using AutoMapper;
using BooksAPI.FE.Contracts.Author;
using BooksAPI.FE.Contracts.LibraryManga;
using BooksAPI.FE.Interfaces;
using BooksAPI.FE.Model;

namespace BooksAPI.FE.Services;

public class LibraryMangaService : ILibraryMangaService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IRefreshTokenService _refreshTokenService;

    public LibraryMangaService(IHttpClientFactory clientFactory, IMapper mapper, IConfiguration configuration,
        IRefreshTokenService refreshTokenService)
    {
        _clientFactory = clientFactory;
        _mapper = mapper;
        _configuration = configuration;
        _refreshTokenService = refreshTokenService;
    }

    public async Task<List<LibraryMangaResponse>> GetMangasForPage(int page, int entries)
    {
        string url = string.Format(_configuration["Backend:LibraryMangas:GetLibraryMangasForPage"]!, page, entries);
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

        HttpClient httpClient = _clientFactory.CreateClient();

        HttpResponseMessage responseMessage = await httpClient.SendAsync(request);
        if (responseMessage.IsSuccessStatusCode)
        {
            await using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync())
            {
                List<LibraryMangaResponse>? response =
                    await JsonSerializer.DeserializeAsync<List<LibraryMangaResponse>>(responseStream);

                if (response is null)
                {
                    throw new Exception();
                }

                return response;
            }
        }
        else
        {
            throw new Exception();
        }
    }

    public async Task<LibraryMangaModel> GetMangaModel(int id)
    {
        LibraryMangaResponse? response = await GetManga(id);

        LibraryMangaModel model = _mapper.Map<LibraryMangaModel>(response);

        return model;
    }

    public async Task<LibraryMangaResponse> GetManga(int id)
    {
        string url = string.Format(_configuration["Backend:LibraryMangas:GetLibraryManga"]!, id);

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

        HttpClient httpClient = _clientFactory.CreateClient();

        HttpResponseMessage responseMessage = await httpClient.SendAsync(request);
        if (responseMessage.IsSuccessStatusCode)
        {
            await using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync())
            {
                LibraryMangaResponse? response =
                    await JsonSerializer.DeserializeAsync<LibraryMangaResponse>(responseStream);

                if (response is null)
                {
                    throw new Exception();
                }

                return response;
            }
        }
        else
        {
            throw new Exception();
        }
    }

    public async Task<List<LibraryMangaResponse>> SearchManga(string search)
    {
        string url = string.Format(_configuration["Backend:LibraryMangas:SearchLibraryManga"]!, search);

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

        HttpClient httpClient = _clientFactory.CreateClient();

        HttpResponseMessage responseMessage = await httpClient.SendAsync(request);
        if (responseMessage.IsSuccessStatusCode)
        {
            await using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync())
            {
                List<LibraryMangaResponse>? response =
                    await JsonSerializer.DeserializeAsync<List<LibraryMangaResponse>>(responseStream);

                if (response is null)
                {
                    throw new Exception();
                }

                return response;
            }
        }
        else
        {
            throw new Exception();
        }
    }

    public async Task<bool> CreateManga(LibraryMangaModel model, string token, string refreshToken)
    {
        string url = _configuration["Backend:LibraryMangas:CreateLibraryManga"]!;

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        CreateLibraryMangaRequest requestContent = _mapper.Map<CreateLibraryMangaRequest>(model);
        
        request.Content = JsonContent.Create(requestContent);

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
            return true;
        }

        return false;
    }

    public async Task<bool> UpdateManga(int id, LibraryMangaModel model, string token, string refreshToken)
    {
        string url = string.Format(_configuration["Backend:LibraryMangas:UpdateLibraryManga"]!, id);

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        UpdateLibraryMangaRequest requestContent = _mapper.Map<UpdateLibraryMangaRequest>(model);

        request.Content = JsonContent.Create(requestContent);

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
            return true;
        }

        return false;
    }

    public async Task<bool> DeleteManga(int id, string token, string refreshToken)
    {
        string url = string.Format(_configuration["Backend:LibraryMangas:DeleteLibraryManga"]!, id);

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