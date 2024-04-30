using BooksAPI.FE.Contracts.LibraryManga;

namespace BooksAPI.FE.Interfaces;

public interface ILibraryMangaService
{
    public Task<IEnumerable<LibraryMangaResponse>> GetMangasForPage(int page, int entries);
}