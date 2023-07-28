using BooksAPI.Contracts.Requests.Comic;
using BooksAPI.Contracts.Response.Comic;
using BooksAPI.Interfaces.Services;

namespace BooksAPI.Services;

public class ComicService:IComicService
{
    public Task<CreateComicResponse> CreateComic(CreateComicRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<GetComicResponse> GetComic(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<GetComicResponse>> GetAllComics()
    {
        throw new NotImplementedException();
    }

    public Task<UpdateComicResponse> UpdateComic(Guid id, UpdateComicRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<DeleteComicResponse> DeleteComic(Guid id)
    {
        throw new NotImplementedException();
    }
}