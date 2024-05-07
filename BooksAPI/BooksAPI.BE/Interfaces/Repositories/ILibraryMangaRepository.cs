using BooksAPI.BE.Entities;

namespace BooksAPI.BE.Interfaces.Repositories;

public interface ILibraryMangaRepository
{
    Task CreateLibraryManga(LibraryManga libraryManga);

    Task<LibraryManga?> GetLibraryMangaById(int id);

    Task<List<LibraryManga>> GetAllLibraryMangas();

    Task<List<LibraryManga>> SearchByTitle(string searchTitle);

    Task<List<LibraryManga>> GetLibraryMangasForPage(int pageIndex, int pageEntriesCount);

    Task UpdateLibraryManga(LibraryManga libraryManga);

    Task DeleteLibraryManga(LibraryManga libraryManga);
}