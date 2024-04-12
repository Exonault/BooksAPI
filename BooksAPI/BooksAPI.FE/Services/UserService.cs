using System.Net;
using AutoMapper;
using BooksAPI.FE.Contracts.User;
using BooksAPI.FE.Data;
using BooksAPI.FE.Interfaces;

namespace BooksAPI.FE.Services;

public class UserService:IUserService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;


    public UserService(IHttpClientFactory clientFactory, IMapper mapper, IConfiguration configuration)
    {
        _clientFactory = clientFactory;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<HttpResponseMessage> Register(RegisterModel model)
    {
        RegisterRequest requestContent = _mapper.Map<RegisterRequest>(model);

        string uri = "";
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
        request.Content = JsonContent.Create(requestContent);

        HttpClient httpClient = _clientFactory.CreateClient();

        try
        {
            HttpResponseMessage response = await httpClient.SendAsync(request);
            return response;
            //TODO Return the actual content not the http response
        }
        catch (Exception e)
        {
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}