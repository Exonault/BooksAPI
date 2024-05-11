using BooksAPI.FE.Contracts.LibraryManga;

namespace BooksAPI.FE.Interfaces;

public interface ILibraryMangaService
{ 
    Task<IEnumerable<LibraryMangaResponse>> GetMangasForPage(int page, int entries);

    Task<LibraryMangaResponse> GetManga(int id);

    Task<IEnumerable<LibraryMangaResponse>> SearchManga(string search);
}