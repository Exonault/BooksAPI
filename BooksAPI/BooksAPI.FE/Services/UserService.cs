using System.Text.Json;
using AutoMapper;
using BooksAPI.FE.Contracts.User;
using BooksAPI.FE.Interfaces;
using BooksAPI.FE.Model;

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


    public async Task<RegisterResponse?> Register(RegisterModel model)
    {
        RegisterRequest requestContent = _mapper.Map<RegisterRequest>(model);
        string uri = string.Format(_userUrl, "register");
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
        request.Content = JsonContent.Create(requestContent);

        HttpClient httpClient = _clientFactory.CreateClient();
        try
        {
            HttpResponseMessage responseMessage = await httpClient.SendAsync(request);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return null;
            }

            await using Stream responseStream = await responseMessage.Content.ReadAsStreamAsync();
            RegisterResponse? registerResponse =
                await JsonSerializer.DeserializeAsync<RegisterResponse>(responseStream);

            return registerResponse;
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task<LoginResponse?> Login(LoginModel model)
    {
        LoginRequest requestContent = _mapper.Map<LoginRequest>(model);
        string uri = string.Format(_userUrl, "login");

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
        request.Content = JsonContent.Create(requestContent);

        HttpClient httpClient = _clientFactory.CreateClient();

        try
        {
            HttpResponseMessage responseMessage = await httpClient.SendAsync(request);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return null;
            }

            await using Stream responseStream = await responseMessage.Content.ReadAsStreamAsync();

            //Add token to session storage
            LoginResponse? loginResponse = await JsonSerializer.DeserializeAsync<LoginResponse>(responseStream);
            return loginResponse;
        }
        catch (Exception e)
        {
            throw;
        }
    }
}