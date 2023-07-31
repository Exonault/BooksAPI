using BooksAPI.Contracts.Requests.Comic;
using BooksAPI.Contracts.Response.Comic;

namespace BooksAPI.Interfaces.Services;

public interface IComicService
{
    public Task<CreateComicResponse> CreateComic(CreateComicRequest request);

    public Task<GetComicResponse> GetComic(Guid id);

    public Task<List<GetComicResponse>> GetAllComics();

    public Task<List<GetComicResponse>> GetAllComicsByReadingStatus(string readingStatus);

    public Task<List<GetComicResponse>> GetAllComicsByDemographic(string demographic);

    public Task<List<GetComicResponse>> GetAllComicsByPublishingStatus(string publishingStatus);

    public Task<List<GetComicResponse>> GetAllComicsByComicType(string comicType);

    public Task<UpdateComicResponse> UpdateComic(Guid id, UpdateComicRequest request);

    public Task<DeleteComicResponse> DeleteComic(Guid id);
}