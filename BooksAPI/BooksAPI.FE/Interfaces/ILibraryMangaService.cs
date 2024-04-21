using BooksAPI.FE.Contracts.LibraryComic;

namespace BooksAPI.FE.Interfaces;

public interface ILibraryMangaService
{
    public Task<List<LibraryMangaResponse>> GetMangasForPage(int page);
}