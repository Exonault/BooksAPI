using BooksAPI.BE.Entities;

namespace BooksAPI.BE.Interfaces.Repositories;

public interface ILibraryComicRepository
{
    public Task CreateLibraryComic(LibraryComic libraryComic);

    public Task<LibraryComic?> GetLibraryComicById(Guid id);

    public Task<List<LibraryComic>> GetAllLibraryComics();
    
    public Task UpdateLibraryComic(LibraryComic libraryComic);

    public Task DeleteLibraryComic(LibraryComic libraryComic);
}