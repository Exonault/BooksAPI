using BooksAPI.BE.Contracts.LibraryManga;

namespace BooksAPI.BE.Interfaces.Services;

public interface ILibraryMangaService
{
    Task CreateLibraryManga(CreateLibraryMangaRequest request);

    Task<LibraryMangaResponse> GetLibraryManga(int id);

    Task<List<LibraryMangaResponse>> GetAllLibraryMangas();

    Task<List<LibraryMangaResponse>> SearchByTitle(string title);

    Task<List<LibraryMangaForPageResponse>> GetLibraryMangasForPage(int pageIndex, int pageEntriesCount);

    Task UpdateLibraryManga(int id, UpdateLibraryMangaRequest request);

    Task DeleteLibraryManga(int id);
}