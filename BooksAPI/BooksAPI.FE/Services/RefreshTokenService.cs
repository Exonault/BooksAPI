using System.Text.Json;
using BooksAPI.FE.Contracts.User;
using BooksAPI.FE.Interfaces;
using Microsoft.JSInterop;

namespace BooksAPI.FE.Services;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IJSRuntime _jsRuntime;
    private readonly IConfiguration _configuration;
    private readonly string _userUrl;

    public RefreshTokenService(IHttpClientFactory clientFactory, IJSRuntime jsRuntime, IConfiguration configuration)
    {
        _clientFactory = clientFactory;
        _jsRuntime = jsRuntime;
        _configuration = configuration;
        _userUrl = _configuration["Backend:User"]!;
    }

    public async Task<bool> RefreshToken(string token, string refreshToken)
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
                    await _jsRuntime.InvokeVoidAsync("addCookie", $"{loginResponse.Token}", $"{loginResponse.RefreshToken}");
                    return true;
                }
                await _jsRuntime.InvokeVoidAsync("deleteCookie", $"{token}", $"{refreshToken}");
                return false;
            }
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<string[]> GetTokens()
    {
        try
        {
            string[] tokens = await _jsRuntime.InvokeAsync<string[]>("getTokens");

            return tokens;
        }
        catch (Exception e)
        {
            return [];
        }
       
    }
}