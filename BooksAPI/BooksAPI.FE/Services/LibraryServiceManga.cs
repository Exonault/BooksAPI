using System.Text.Json;
using AutoMapper;
using BooksAPI.FE.Contracts.LibraryManga;
using BooksAPI.FE.Interfaces;

namespace BooksAPI.FE.Services;

public class LibraryMangaService : ILibraryMangaService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public LibraryMangaService(IHttpClientFactory clientFactory, IMapper mapper, IConfiguration configuration)
    {
        _clientFactory = clientFactory;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<IEnumerable<LibraryMangaResponse>> GetMangasForPage(int page, int entries)
    {
        string url = string.Format(_configuration["Backend:LibraryMangas:GetLibraryMangasForPage"]!, page, entries);
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

        HttpClient httpClient = _clientFactory.CreateClient();

        HttpResponseMessage responseMessage = await httpClient.SendAsync(request);
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception();
        }

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

    public async Task<LibraryMangaResponse> GetManga(int id)
    {
        string url = string.Format(_configuration["Backend:LibraryMangas:GetLibraryManga"]!, id);

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

        HttpClient httpClient = _clientFactory.CreateClient();

        HttpResponseMessage responseMessage = await httpClient.SendAsync(request);
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception();
        }

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

    public async Task<IEnumerable<LibraryMangaResponse>> SearchManga(string search)
    {
        string url = string.Format(_configuration["Backend:LibraryMangas:SearchLibraryManga"]!, search);

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

        HttpClient httpClient = _clientFactory.CreateClient();

        HttpResponseMessage responseMessage = await httpClient.SendAsync(request);
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception();
        }

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
}