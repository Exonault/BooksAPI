using System.Net;
using AutoMapper;
using BookAPI.Presentation.Contracts.Requests.Comic;
using BookAPI.Presentation.Interfaces;
using BookAPI.Presentation.Models;

namespace BookAPI.Presentation.Services;

public class ComicService : IComicsService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public ComicService(IHttpClientFactory clientFactory, IMapper mapper, IConfiguration configuration)
    {
        _clientFactory = clientFactory;
        _mapper = mapper;
        _configuration = configuration;
    }


    public async Task<HttpResponseMessage> CreateComic(ModifyComicsModel model)
    {
        CreateComicRequest requestContent = _mapper.Map<CreateComicRequest>(model);

        var request = new HttpRequestMessage(HttpMethod.Post, _configuration["ApiUri:Comics:CreateComic"]);
        request.Content = JsonContent.Create(requestContent);

        var client = _clientFactory.CreateClient();

        try
        {
            HttpResponseMessage response = await client.SendAsync(request);
            return response;
        }
        catch (Exception e)
        {
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }

    public async Task<HttpResponseMessage> GetAllComics()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, _configuration["ApiUri:Comics:GetAllComicsUri"]);

        var client = _clientFactory.CreateClient();

        try
        {
            HttpResponseMessage response = await client.SendAsync(request);
            return response;
        }
        catch (Exception e)
        {
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }

    public async Task<HttpResponseMessage> GetComic(string id)
    {
        string uri = string.Format(_configuration["ApiUri:Comics:GetComicByIdUri"] ?? string.Empty, id);

        var request = new HttpRequestMessage(HttpMethod.Get, uri);

        var client = _clientFactory.CreateClient();

        try
        {
            HttpResponseMessage response = await client.SendAsync(request);
            return response;
        }
        catch (Exception e)
        {
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }

    public async Task<HttpResponseMessage> UpdateComic(string id, ModifyComicsModel model)
    {
        UpdateComicRequest requestContent = _mapper.Map<UpdateComicRequest>(model);

        string uri = string.Format(_configuration["ApiUri:Comics:UpdateComic"] ?? string.Empty, id);

        var request = new HttpRequestMessage(HttpMethod.Put, uri);
        request.Content = JsonContent.Create(requestContent);

        var client = _clientFactory.CreateClient();
        try
        {
            HttpResponseMessage response = await client.SendAsync(request);
            return response;
        }
        catch (Exception e)
        {
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }

    public async Task<HttpResponseMessage> DeleteComic(string id)
    {
        string uri = string.Format(_configuration["ApiUri:Comics:DeleteComic"] ?? string.Empty, id);

        var request = new HttpRequestMessage(HttpMethod.Delete, uri);

        var client = _clientFactory.CreateClient();
        try
        {
            HttpResponseMessage response = await client.SendAsync(request);
            return response;
        }
        catch (Exception e)
        {
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}