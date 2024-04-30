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
    private readonly IJSRuntime _jsRuntime;

    public UserService(IHttpClientFactory clientFactory, IMapper mapper, IConfiguration configuration,
        IJSRuntime jsRuntime)
    {
        _clientFactory = clientFactory;
        _mapper = mapper;
        _configuration = configuration;
        _jsRuntime = jsRuntime;
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

            await using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync())
            {
                RegisterResponse? registerResponse =
                    await JsonSerializer.DeserializeAsync<RegisterResponse>(responseStream);

                return registerResponse;
            }
        }
        catch (Exception e)
        {
            return null;
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

            await using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync())
            {
                LoginResponse? loginResponse = await JsonSerializer.DeserializeAsync<LoginResponse>(responseStream);
                return loginResponse;
            }
        }
        catch (Exception e)
        {
            return null;
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

    public async Task<bool> Refresh(string token, string refreshToken)
    {
        RefreshRequest requestContent = new RefreshRequest
        {
            AccessToken = token,
            RefreshToken = refreshToken
        };

        string uri = string.Format(_userUrl, "refresh");

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
        request.Content = JsonContent.Create(requestContent);

        HttpClient httpClient = _clientFactory.CreateClient();

        try
        {
            HttpResponseMessage responseMessage = await httpClient.SendAsync(request);

            if (!responseMessage.IsSuccessStatusCode)
            {
                return false;
            }

            await using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync())
            {
                LoginResponse? loginResponse = await JsonSerializer.DeserializeAsync<LoginResponse>(responseStream);
                if (loginResponse is not null)
                {
                    await _jsRuntime.InvokeVoidAsync("addCookie", $"{loginResponse.Token}", $"{loginResponse.Token}");
                    return true;
                }
                else
                {
                    await _jsRuntime.InvokeVoidAsync("deleteCookie", $"{token}",
                        $"{refreshToken}");
                    return false;
                }
            }
        }
        catch (Exception e)
        {
            return false;
        }
    }
}