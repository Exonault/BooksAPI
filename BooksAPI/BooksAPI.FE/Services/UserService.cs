using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using AutoMapper;
using BooksAPI.FE.Contracts.User;
using BooksAPI.FE.Interfaces;
using BooksAPI.FE.Model;
using Microsoft.JSInterop;

namespace BooksAPI.FE.Services;

public class UserService : IUserService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly string _userUrl;

    public UserService(IHttpClientFactory clientFactory, IMapper mapper, IConfiguration configuration)
    {
        _clientFactory = clientFactory;
        _mapper = mapper;
        _configuration = configuration;
        _userUrl = _configuration["Backend:User"]!;
    }


    public async Task<RegisterResponse?> Register(RegisterModel model, bool isAdmin)
    {
        RegisterRequest requestContent = _mapper.Map<RegisterRequest>(model);
        requestContent.Admin = isAdmin;
        string uri = string.Format(_userUrl, "register");
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
        request.Content = JsonContent.Create(requestContent);

        HttpClient httpClient = _clientFactory.CreateClient();

        HttpResponseMessage responseMessage = await httpClient.SendAsync(request);
        if (!responseMessage.IsSuccessStatusCode)
        {
            return null;
        }

        await using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync())
        {
            RegisterResponse? registerResponse =
                await JsonSerializer.DeserializeAsync<RegisterResponse>(responseStream);

            return registerResponse;
        }
    }

    public async Task<LoginResponse?> Login(LoginModel model)
    {
        LoginRequest requestContent = _mapper.Map<LoginRequest>(model);
        string uri = string.Format(_userUrl, "login");

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
        request.Content = JsonContent.Create(requestContent);

        HttpClient httpClient = _clientFactory.CreateClient();

        HttpResponseMessage responseMessage = await httpClient.SendAsync(request);

        if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new ArgumentException();
        }

        if (responseMessage.StatusCode == HttpStatusCode.NotFound)
        {
            throw new InvalidOperationException();
        }

        if (!responseMessage.IsSuccessStatusCode)
        {
            return null;
        }

        await using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync())
        {
            LoginResponse? loginResponse = await JsonSerializer.DeserializeAsync<LoginResponse>(responseStream);
            return loginResponse;
        }
    }

    public async Task Logout(string token)
    {
        HttpClient httpClient = _clientFactory.CreateClient();

        string uri = string.Format(_userUrl, "revoke");
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, uri);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        try
        {
            HttpResponseMessage responseMessage = await httpClient.SendAsync(request);
        }
        catch (Exception e)
        {
            // return null;
        }
    }
}