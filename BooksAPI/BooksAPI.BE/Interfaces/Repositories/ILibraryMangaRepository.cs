using BooksAPI.BE.Entities;

namespace BooksAPI.BE.Interfaces.Repositories;

public interface ILibraryMangaRepository
{
    public Task CreateLibraryManga(LibraryManga libraryManga);

    public Task<LibraryManga?> GetLibraryMangaById(Guid id);

    public Task<List<LibraryManga>> GetAllLibraryMangas();
    
    public Task UpdateLibraryManga(LibraryManga libraryManga);

    public Task DeleteLibraryManga(LibraryManga libraryManga);
}