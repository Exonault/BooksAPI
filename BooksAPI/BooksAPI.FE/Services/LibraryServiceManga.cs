using AutoMapper;
using BooksAPI.FE.Contracts.LibraryComic;
using BooksAPI.FE.Interfaces;

namespace BooksAPI.FE.Services;

public class LibraryMangaService:ILibraryMangaService
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

    public Task<List<LibraryMangaResponse>> GetMangasForPage(int page)
    {
        string url = _configuration["Backend:LibraryManga:GetLibraryMangasForList"]!;
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
        
        
        
        return null;
    }
}