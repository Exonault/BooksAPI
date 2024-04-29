using System.Net;
using System.Net.Http.Headers;
using BooksAPI.FE.Interfaces;

namespace BooksAPI.FE.Handlers;

public class AuthenticationHandler : DelegatingHandler
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;
    private bool _refreshing;
    
    public AuthenticationHandler(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var jwt = "";//await _authenticationService.GetJwtAsync();
        var isToServer = request.RequestUri?.AbsoluteUri.StartsWith(_configuration["ServerUrl"] ?? "") ?? false;

        if (isToServer && !string.IsNullOrEmpty(jwt))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        var response =  await base.SendAsync(request, cancellationToken);

        if (!_refreshing && !string.IsNullOrEmpty(jwt) && response.StatusCode == HttpStatusCode.Unauthorized)
        {
            try
            {
                _refreshing = true;

                // if (await _authenticationService.RefreshAsync())
                // {
                //     jwt = await _authenticationService.GetJwtAsync();
                //
                //     if (isToServer && !string.IsNullOrEmpty(jwt))
                //         request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                //
                //     response = await base.SendAsync(request, cancellationToken);
                // }
            }
            finally
            {
                _refreshing = false;
            }
        }

        return response;
    }
}