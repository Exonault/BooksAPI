using BooksAPI.BE.Contracts.LibraryComic;

namespace BooksAPI.BE.Interfaces.Services;

public interface ILibraryComicService
{
    public Task CreateLibraryComic(CreateLibraryComicRequest request);

    public Task<LibraryComicResponse> GetLibraryComic(Guid id);

    public Task<List<LibraryComicResponse>> GetAllLibraryComics();

    public Task UpdateLibraryComic(Guid id, UpdateLibraryComicRequest request);
    
    public Task DeleteLibraryComic(Guid id);
}