using BooksAPI.Contracts.Requests.Comic;
using BooksAPI.Contracts.Response.Comic;

namespace BooksAPI.Interfaces.Services;

public interface IComicService
{
    public Task<CreateComicResponse> CreateComic(CreateComicRequest request);

    public Task<GetComicResponse> GetComic(Guid id);

    public Task<List<GetComicResponse>> GetAllComics();

    public Task<UpdateComicResponse> UpdateComic(Guid id, UpdateComicRequest request);

    public Task<DeleteComicResponse> DeleteComic(Guid id);
}