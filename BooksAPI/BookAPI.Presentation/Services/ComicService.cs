using System.Net;
using AutoMapper;
using BookAPI.Presentation.Constants;
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

        var uri = _configuration.GetSection("ApiUri")
            .GetSection("Comics")
            .GetSection(ComicsConstants.CreateComicUri).Value;
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
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
        var uri = _configuration.GetSection("ApiUri")
            .GetSection("Comics")
            .GetSection(ComicsConstants.GetAllComicsUri).Value;

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

    public async Task<HttpResponseMessage> GetComic(string id)
    {
        string uri = string.Format(_configuration.GetSection("ApiUri")
            .GetSection("Comics")
            .GetSection(ComicsConstants.GetComicByIdUri).Value ?? string.Empty, id);

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

        string uri = string.Format(_configuration.GetSection("ApiUri")
            .GetSection("Comics")
            .GetSection(ComicsConstants.UpdateComicUri).Value ?? string.Empty, id);

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
        string uri = string.Format(_configuration.GetSection("ApiUri")
            .GetSection("Comics")
            .GetSection(ComicsConstants.DeleteComicUri).Value ?? string.Empty, id);

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